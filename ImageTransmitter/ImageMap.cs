using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageTransmitter
{
   class ImageMap
    {
        byte[] m_rgbValues = null;
        int m_stride = 0;

        public ImageMap(Bitmap bitmap)
        {
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            System.Drawing.Imaging.BitmapData bmpData = bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            m_stride = bmpData.Stride;
            int bytes = Math.Abs(m_stride) * bitmap.Height;
            m_rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, m_rgbValues, 0, bytes);
            bitmap.UnlockBits(bmpData);
        }

        public ImageMap()
        {
            m_rgbValues = null;
            m_stride = 0;
        }

        #region === public ===

        public ImageMap Clone()
        {
            ImageMap im = new ImageMap
            {
                m_stride = m_stride
            };

            if (m_rgbValues != null)
            {
                im.m_rgbValues = new byte[m_rgbValues.Length];
                Array.Copy(m_rgbValues, im.m_rgbValues, m_rgbValues.Length);
            }

            return im;
        }

        public bool isValid => m_rgbValues.Length > 0 && m_stride != 0;

        public byte GetValue(int x, int y, int offset)
        {
            return m_rgbValues[y * m_stride + x * 3 + offset];
        }

        public void SetValue(int x, int y, int offset, byte v)
        {
            m_rgbValues[y * m_stride + x * 3 + offset] = v;
        }

        public byte[] Values => m_rgbValues;

        #endregion
    }
}
