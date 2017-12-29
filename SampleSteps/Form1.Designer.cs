namespace SampleSteps
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
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.lblFrame = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.cmbTransmitterEffects = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(403, 604);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(135, 670);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(116, 36);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(13, 670);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(116, 36);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // lblFrame
            // 
            this.lblFrame.AutoSize = true;
            this.lblFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFrame.Location = new System.Drawing.Point(12, 619);
            this.lblFrame.Name = "lblFrame";
            this.lblFrame.Size = new System.Drawing.Size(92, 31);
            this.lblFrame.TabIndex = 4;
            this.lblFrame.Text = "label1";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(296, 670);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(116, 36);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "To start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // cmbTransmitterEffects
            // 
            this.cmbTransmitterEffects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransmitterEffects.FormattingEnabled = true;
            this.cmbTransmitterEffects.Location = new System.Drawing.Point(275, 622);
            this.cmbTransmitterEffects.Name = "cmbTransmitterEffects";
            this.cmbTransmitterEffects.Size = new System.Drawing.Size(140, 21);
            this.cmbTransmitterEffects.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 718);
            this.Controls.Add(this.cmbTransmitterEffects);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblFrame);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sample Steps";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label lblFrame;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cmbTransmitterEffects;
    }
}

