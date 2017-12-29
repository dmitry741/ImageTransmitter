using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageTransmitter
{
    class ImageMatrix
    {
        double[] m_matrix = null;
        int m_width, m_height;

        public ImageMatrix(int width, int height)
        {
            m_width = width;
            m_height = height;
            m_matrix = new double[width * height];
        }

        public ImageMatrix()
        {
            m_width = 0;
            m_height = 0;
            m_matrix = null;
        }

        public ImageMatrix Clone()
        {
            ImageMatrix im = new ImageMatrix
            {
                m_width = m_width,
                m_height = m_height
            };

            if (m_matrix != null)
            {
                im.m_matrix = new double[width * height];
                Array.Copy(m_matrix, im.m_matrix, m_matrix.Length);
            }

            return im;
        }

        public int width => m_width;
        public int height => m_height;

        public int size => width * height;

        public double GetValue(int x, int y)
        {
            return m_matrix[y * m_width + x];
        }

        public void SetValue(int x, int y, double v)
        {
            m_matrix[y * m_width + x] = v;
        }

        public void SetValueOne(int x, int y)
        {
            m_matrix[y * m_width + x] = 1;
        }

        public void SetValue(int i, double v)
        {
            m_matrix[i] = v;
        }

        public void SetValueOne(int i)
        {
            m_matrix[i] = 1;
        }

        public void ZeroValues()
        {
            Array.Clear(m_matrix, 0, size);
        }
    }
}
