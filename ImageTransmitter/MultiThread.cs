using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageTransmitter
{
    class ThreadRoutine
    {
        public int command = -1;

        System.Threading.EventWaitHandle m_ewhStart = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset);
        System.Threading.EventWaitHandle m_ewhStop = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.AutoReset);

        ImageMatrix m_matrix = null;
        ImageMap m_map1 = null;
        ImageMap m_map2 = null;
        ImageMap m_resultImageMap = null;
        byte[] m_A = null;

        int m_NStep, m_dn, m_cell;

        public void StartSet()
        {
            m_ewhStart.Set();
        }

        public void StopWainForOne()
        {
            m_ewhStop.WaitOne();
        }

        public void SetFinalTransmition(ImageMatrix matrix, ImageMap map1, ImageMap map2, ImageMap resultImageMap)
        {
            m_matrix = matrix;
            m_map1 = map1;
            m_map2 = map2;
            m_resultImageMap = resultImageMap;
        }

        public void Blind(int NStep, int dn, int cell, ImageMatrix matrix)
        {
            m_matrix = matrix;
            m_NStep = NStep;
            m_dn = dn;
            m_cell = cell;
        }

        public void Archimedes(int NStep, ImageMatrix matrix, byte[] A, int cell)
        {
            m_matrix = matrix;
            m_NStep = NStep;
            m_A = A;
            m_cell = cell;
        }

        public int startIndex { get; set; }
        public int stopIndex { get; set; }

        public void DoWork()
        {
            while (true)
            {
                m_ewhStart.WaitOne();

                if (command == 1) // final mixing!
                {
                    for (int x = startIndex; x <= stopIndex; x++)
                    {
                        for (int y = 0; y < m_matrix.height; y++)
                        {
                            double t = m_matrix.GetValue(x, y);

                            for (int offset = 0; offset < 3; offset++)
                            {
                                double v2 = m_map2.GetValue(x, y, offset);
                                double v1 = m_map1.GetValue(x, y, offset);
                                double v = (v2 - v1) * t + v1;

                                m_resultImageMap.SetValue(x, y, offset, Convert.ToByte(v));
                            }
                        }
                    }
                }
                else if (command == 2)
                {
                    for (int x = startIndex; x <= stopIndex; x++)
                    {
                        for (int y = 0; y < m_NStep; y++)
                        {
                            for (int j = 0; j < m_cell; j++)
                            {
                                if (y * m_cell + j < m_matrix.height)
                                {
                                    if (j < m_dn)
                                    {
                                        m_matrix.SetValueOne(x, y * m_cell + j);
                                    }
                                }
                            }
                        }
                    }

                }
                else if (command == 3)
                {
                    for (int y = startIndex; y <= stopIndex ; y++)
                    {
                        for (int x = 0; x < m_NStep; x++)
                        {
                            for (int i = 0; i < m_cell; i++)
                            {
                                if (x * m_cell + i < m_matrix.width)
                                {
                                    if (i < m_dn)
                                    {
                                        m_matrix.SetValueOne(x * m_cell + i, y);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (command == 4)
                {
                    for (int y = startIndex; y <= stopIndex; y++)
                    {
                        for (int x = 0; x < m_NStep; x++)
                        {
                            for (int j = 0; j < m_cell; j++)
                            {
                                for (int i = 0; i < m_cell; i++)
                                {
                                    if (x * m_cell + i < m_matrix.width && y * m_cell + j < m_matrix.height)
                                    {
                                        if (i + j < m_dn)
                                        {
                                            m_matrix.SetValueOne(x * m_cell + i, y * m_cell + j);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (command == 5)
                {
                    for (int x = startIndex; x <= stopIndex; x++)
                    {
                        for (int y = 0; y < m_NStep; y++)
                        {
                            for (int i = 0; i < m_cell; i++)
                            {
                                for (int j = 0; j < m_cell; j++)
                                {
                                    int xCenter = x * m_cell + i;
                                    int yCenter = y * m_cell + j;

                                    if (xCenter < m_matrix.width && yCenter < m_matrix.height)
                                    {
                                        m_matrix.SetValue(xCenter, yCenter, m_A[j * m_cell + i]);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    m_ewhStop.Set();
                    break;
                }

                m_ewhStop.Set();
            }
        }
    }
}
