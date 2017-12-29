namespace SampleAnimation
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbTransmitterEffect = new System.Windows.Forms.ComboBox();
            this.btnPlay = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(403, 604);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(421, 580);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(148, 36);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Ok";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbTransmitterEffect
            // 
            this.cmbTransmitterEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransmitterEffect.FormattingEnabled = true;
            this.cmbTransmitterEffect.Location = new System.Drawing.Point(421, 12);
            this.cmbTransmitterEffect.Name = "cmbTransmitterEffect";
            this.cmbTransmitterEffect.Size = new System.Drawing.Size(148, 21);
            this.cmbTransmitterEffect.TabIndex = 7;
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(421, 39);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(148, 40);
            this.btnPlay.TabIndex = 8;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 625);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.cmbTransmitterEffect);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sample Animation Image Transmitter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbTransmitterEffect;
        private System.Windows.Forms.Button btnPlay;
    }
}

