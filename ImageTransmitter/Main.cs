using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ImageTransmitter
{
    public class PDImageTransmitter : IDisposable
    {
        #region === members ===

        ImageMap m_imageMapStart = null;
        ImageMap m_imageMapStop = null;

        Bitmap m_bitmap = null;
        int m_transmiteEffect;
        int m_count = 0;
        ImageMatrix m_matrix = null;
        ImageMap m_imageMap = null;

        #endregion

        public PDImageTransmitter(Bitmap bitmap1, Bitmap bitmap2, int transmitterEffect, int stepsCount)
        {
            SetBitmaps(bitmap1, bitmap2, stepsCount);
            Initialize(bitmap1.Width, bitmap1.Height, transmitterEffect, stepsCount);
            StartThreads();
        }

        #region === threads ===

        int m_processCount = Environment.ProcessorCount;

        System.Threading.Thread[] m_thread = null;
        ThreadRoutine[] m_threadRoutine = null;

        void StartThreads()
        {
            m_thread = new System.Threading.Thread[m_processCount];
            m_threadRoutine = new ThreadRoutine[m_processCount];

            for (int i = 0; i < m_processCount; i++)
            {
                m_threadRoutine[i] = new ThreadRoutine
                {
                    command = 0
                };

                System.Threading.ThreadStart threadDelegate = new System.Threading.ThreadStart(m_threadRoutine[i].DoWork);
                m_thread[i] = new System.Threading.Thread(threadDelegate);

                m_thread[i].Start();
            }
        }

        void StopThreads()
        {
            for (int i = 0; i < m_processCount; i++)
            {
                m_threadRoutine[i].command = -1;
                m_threadRoutine[i].StartSet();
            }
        }

        #endregion

        #region === private ===

        public void Initialize(int width, int height, int transmiteEffect, int count)
        {
            m_bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            m_transmiteEffect = transmiteEffect;
            m_count = count;
            m_matrix = new ImageMatrix(width, height);
            m_imageMap = new ImageMap(m_bitmap);
        }

        void PrepareImageMatrix(int iteration)
        {
            m_matrix.ZeroValues();

            do
            {
                if (iteration <= 0)
                    break;

                if (iteration >= m_count - 1)
                {
                    System.Threading.Tasks.Parallel.For(0, m_matrix.size, i =>
                    {
                        m_matrix.SetValueOne(i);
                    });

                    break;
                }

                if (m_transmiteEffect == 0)
                {
                    double t = Convert.ToDouble(iteration) / Convert.ToDouble(m_count);

                    System.Threading.Tasks.Parallel.For(0, m_matrix.size, i =>
                    {
                        m_matrix.SetValue(i, t);
                    });

                    break;
                }

                if (m_transmiteEffect == 1)
                {
                    const int c_cell = 24;
                    double dn = Convert.ToDouble(c_cell) / Convert.ToDouble(m_count - 1) * iteration;
                    int NStep = m_matrix.height / c_cell + 1;
                    int XStep = m_matrix.width / m_processCount;
                    int i;

                    // start mixing
                    for (i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].Blind(NStep, Convert.ToInt32(dn), c_cell, m_matrix);
                        m_threadRoutine[i].startIndex = i * XStep;
                        m_threadRoutine[i].stopIndex = (i < m_processCount - 1) ? (i + 1) * XStep : m_matrix.width - 1;
                        m_threadRoutine[i].command = 2;
                        m_threadRoutine[i].StartSet();
                    }

                    // waiting for each thread complete its task
                    for (i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].StopWainForOne();
                    }

                    break;
                }

                if (m_transmiteEffect == 2)
                {
                    const int c_cell = 24;
                    double dn = Convert.ToDouble(c_cell) / Convert.ToDouble(m_count - 1) * iteration;
                    int NStep = m_matrix.width / c_cell + 1;
                    int YStep = m_matrix.height / m_processCount;
                    int i;

                    // start mixing
                    for (i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].Blind(NStep, Convert.ToInt32(dn), c_cell, m_matrix);
                        m_threadRoutine[i].startIndex = i * YStep;
                        m_threadRoutine[i].stopIndex = (i < m_processCount - 1) ? (i + 1) * YStep : m_matrix.height - 1;
                        m_threadRoutine[i].command = 3;
                        m_threadRoutine[i].StartSet();
                    }

                    // waiting for each thread complete its task
                    for (i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].StopWainForOne();
                    }

                    break;
                }

                if (m_transmiteEffect == 3)
                {
                    const int c_cell = 24;
                    double dn = Convert.ToDouble(c_cell + c_cell) / Convert.ToDouble(m_count - 1) * iteration;
                    int XStep = m_matrix.width / c_cell + 1;
                    int YStep = m_matrix.height / c_cell + 1;
                    int i;

                    // start mixing
                    for (i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].Blind(XStep, Convert.ToInt32(dn), c_cell, m_matrix);
                        m_threadRoutine[i].startIndex = i * YStep / m_processCount;
                        m_threadRoutine[i].stopIndex = (i + 1) * YStep / m_processCount;
                        m_threadRoutine[i].command = 4;
                        m_threadRoutine[i].StartSet();
                    }

                    // waiting for each thread complete its task
                    for (i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].StopWainForOne();
                    }

                    break;
                }

                if (m_transmiteEffect == 4)
                {
                    const int c_cell = 25;

                    double len = Convert.ToDouble(c_cell * c_cell) / Convert.ToDouble(m_count - 1) * iteration;
                    int nLen = Convert.ToInt32(len); ;
                    int XStep = m_matrix.width / c_cell + 1;
                    int YStep = m_matrix.height / c_cell + 1;
                    byte[] A = new byte[c_cell * c_cell];
                    Array.Clear(A, 0, A.Length);

                    int step = 0;
                    int direction = 1;

                    int xCenter = c_cell / 2;
                    int yCenter = c_cell / 2;

                    A[yCenter * c_cell + xCenter] = 1;
                    int p = 1;

                    for (int i = 0; true; i++)
                    {
                        if (i % 2 == 0)
                            step++;
                        else
                            direction = -direction;

                        for (int j = 0; j < step; j++)
                        {
                            if (i % 2 == 0) // along X
                                xCenter += direction;
                            else // along Y
                                yCenter += direction;

                            if (p < nLen)
                            {
                                A[yCenter * c_cell + xCenter] = 1;
                                p++;
                            }
                            else
                                break;
                        }

                        if (p >= nLen)
                            break;
                    }

                    // start mixing
                    for (int i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].Archimedes(YStep, m_matrix, A, c_cell);
                        m_threadRoutine[i].startIndex = i * XStep / m_processCount;
                        m_threadRoutine[i].stopIndex = (i + 1) * XStep / m_processCount;
                        m_threadRoutine[i].command = 5;
                        m_threadRoutine[i].StartSet();
                    }

                    // waiting for each thread complete its task
                    for (int i = 0; i < m_processCount; i++)
                    {
                        m_threadRoutine[i].StopWainForOne();
                    }

                    break;
                }

            } while (false);
        }

        Bitmap GetBitmap(ImageMap map1, ImageMap map2, int iteration)
        {
            PrepareImageMatrix(iteration);

            int i;
            int XStep = m_matrix.width / m_processCount;

            // start final mixing
            for (i = 0; i < m_processCount; i++)
            {
                m_threadRoutine[i].SetFinalTransmition(m_matrix, map1.Clone(), map2.Clone(), m_imageMap);
                m_threadRoutine[i].startIndex = i * XStep;
                m_threadRoutine[i].stopIndex = (i < m_processCount - 1) ? (i + 1) * XStep : m_matrix.width - 1;
                m_threadRoutine[i].command = 1;
                m_threadRoutine[i].StartSet();
            }

            // waiting for each thread complete its task
            for (i = 0; i < m_processCount; i++)
            {
                m_threadRoutine[i].StopWainForOne();
            }

            Rectangle rect = new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = m_bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int bytes = Math.Abs(bmpData.Stride) * m_bitmap.Height;

            System.Runtime.InteropServices.Marshal.Copy(m_imageMap.Values, 0, bmpData.Scan0, bytes);
            m_bitmap.UnlockBits(bmpData);

            return m_bitmap;
        }

        #endregion

        #region === public ===

        public void SetBitmaps(Bitmap bitmap1, Bitmap bitmap2, int stepsCount)
        {
            if (bitmap1.Width != bitmap2.Width || bitmap1.Height != bitmap2.Height)
                return;

            m_imageMapStart = new ImageMap(bitmap1);
            m_imageMapStop = new ImageMap(bitmap2);
        }

        public Bitmap GetTransmiteBitmap(int iteration)
        {
            return GetBitmap(m_imageMapStart, m_imageMapStop, iteration);
        }

        public bool isValid
        {
            get
            {
                if (m_imageMapStart == null || m_imageMapStop == null)
                    return false;

                return m_imageMapStart.isValid && m_imageMapStop.isValid;
            }
        }
        
        static public IEnumerable<string> effectsCollection
        {
            get
            {
                List<string> collection = new List<string>();

                collection.Add("SmoothTransparent");
                collection.Add("BlindHorizontal");
                collection.Add("BlindVertical");
                collection.Add("BlindDiagonal");
                collection.Add("Archimedes");

                return collection;
            }
        }

        public int transmitterEffect
        {
            get { return m_transmiteEffect; }
            set { m_transmiteEffect = value; }
        }

        #endregion

        #region === IDisposable Support ===

        private bool m_disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposedValue)
            {
                if (disposing)
                {
                    StopThreads();
                }

                m_disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }    
}
