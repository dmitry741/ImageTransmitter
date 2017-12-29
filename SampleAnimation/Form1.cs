using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageTransmitter;

namespace SampleAnimation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap m_bitmap = null;
        const int c_count = 32;

        void Render(Bitmap b)
        {
            if (m_bitmap == null)
                return;

            Graphics g = Graphics.FromImage(m_bitmap);
            g.Clear(Color.Black);

            g.DrawImage(b, 0, 0, b.Width, b.Height);

            pictureBox1.Image = m_bitmap;
        }

        void ShowTransmitter(int te)
        {
            using (PDImageTransmitter it = new PDImageTransmitter(Properties.Resources.coffee1, Properties.Resources.coffee2, te, c_count))
            {
                for (int i = 0; i < c_count; i++)
                {
                    Bitmap bitmap = it.GetTransmiteBitmap(i);
                    Render(bitmap);
                    pictureBox1.Refresh();
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render(Properties.Resources.coffee2);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            cmbTransmitterEffect.Items.AddRange(PDImageTransmitter.effectsCollection.ToArray());
            cmbTransmitterEffect.SelectedIndex = 0;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            int index = cmbTransmitterEffect.SelectedIndex;

            if (index < 0)
                return;

            ShowTransmitter(index);
        }
    }
}
