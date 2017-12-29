using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleSteps
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Bitmap m_bitmap = null;
        const int c_count = 24;
        int m_frame = 0;
        ImageTransmitter.PDImageTransmitter m_it = null;

        void Render(Bitmap b)
        {
            if (m_bitmap == null)
                return;

            Graphics g = Graphics.FromImage(m_bitmap);
            g.Clear(Color.Black);

            g.DrawImage(b, 0, 0, b.Width, b.Height);

            pictureBox1.Image = m_bitmap;
        }

        void Start()
        {
            m_frame = 0;
            Render(m_it.GetTransmiteBitmap(m_frame));
            lblFrame.Text = string.Format("{0} of {1}", m_frame + 1, c_count);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            m_it = new ImageTransmitter.PDImageTransmitter(Properties.Resources.coffee1, Properties.Resources.coffee2, 4, c_count);

            lblFrame.Text = string.Format("{0} of {1}", m_frame + 1, c_count);
            cmbTransmitterEffects.Items.AddRange(ImageTransmitter.PDImageTransmitter.effectsCollection.ToArray());
            cmbTransmitterEffects.SelectedIndex = 0;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_it.Dispose();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Render(m_it.GetTransmiteBitmap(m_frame));
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (m_frame > 0)
            {
                m_frame--;
                m_it.transmitterEffect = cmbTransmitterEffects.SelectedIndex;
                Render(m_it.GetTransmiteBitmap(m_frame));
                lblFrame.Text = string.Format("{0} of {1}", m_frame + 1, c_count);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_frame < c_count - 1)
            {
                m_frame++;
                m_it.transmitterEffect = cmbTransmitterEffects.SelectedIndex;
                Render(m_it.GetTransmiteBitmap(m_frame));
                lblFrame.Text = string.Format("{0} of {1}", m_frame + 1, c_count);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }
    }
}
