namespace KolorPicker
{
    partial class MiniForm
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
            this.colorPreview = new System.Windows.Forms.Panel();
            this.miniHex = new System.Windows.Forms.Label();
            this.miniRgb = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // colorPreview
            // 
            this.colorPreview.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.colorPreview.Location = new System.Drawing.Point(4, 7);
            this.colorPreview.Margin = new System.Windows.Forms.Padding(0);
            this.colorPreview.Name = "colorPreview";
            this.colorPreview.Size = new System.Drawing.Size(50, 50);
            this.colorPreview.TabIndex = 0;
            this.colorPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.colorPreview_Paint);
            // 
            // miniHex
            // 
            this.miniHex.AutoSize = true;
            this.miniHex.Location = new System.Drawing.Point(55, 12);
            this.miniHex.Name = "miniHex";
            this.miniHex.Size = new System.Drawing.Size(42, 15);
            this.miniHex.TabIndex = 0;
            this.miniHex.Text = "label1";
            // 
            // miniRgb
            // 
            this.miniRgb.AutoSize = true;
            this.miniRgb.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.miniRgb.Location = new System.Drawing.Point(55, 33);
            this.miniRgb.Name = "miniRgb";
            this.miniRgb.Size = new System.Drawing.Size(39, 15);
            this.miniRgb.TabIndex = 1;
            this.miniRgb.Text = "label2";
            // 
            // MiniForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(139, 62);
            this.Controls.Add(this.miniRgb);
            this.Controls.Add(this.miniHex);
            this.Controls.Add(this.colorPreview);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MiniForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "MiniForm";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MiniForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel colorPreview;
        private System.Windows.Forms.Label miniHex;
        private System.Windows.Forms.Label miniRgb;
    }
}