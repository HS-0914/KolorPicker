namespace KolorPicker
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Preview = new System.Windows.Forms.PictureBox();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.txtRgb = new System.Windows.Forms.TextBox();
            this.ColorTimer = new System.Windows.Forms.Timer(this.components);
            this.BtnPicker = new System.Windows.Forms.Button();
            this.lblToast = new System.Windows.Forms.Label();
            this.ToastTimer = new System.Windows.Forms.Timer(this.components);
            this.BtnAddPalette = new System.Windows.Forms.Button();
            this.listPalette = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PaletteContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CopyHexMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyRgbMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RecentColorsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.notifyPicker = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PickMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            this.PaletteContextMenu.SuspendLayout();
            this.TrayContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // Preview
            // 
            this.Preview.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Preview.Location = new System.Drawing.Point(23, 21);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(80, 80);
            this.Preview.TabIndex = 0;
            this.Preview.TabStop = false;
            // 
            // txtHex
            // 
            this.txtHex.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtHex.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtHex.Location = new System.Drawing.Point(123, 29);
            this.txtHex.Name = "txtHex";
            this.txtHex.ReadOnly = true;
            this.txtHex.Size = new System.Drawing.Size(150, 27);
            this.txtHex.TabIndex = 1;
            this.txtHex.Click += new System.EventHandler(this.TxtHex_Click);
            // 
            // txtRgb
            // 
            this.txtRgb.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtRgb.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtRgb.Location = new System.Drawing.Point(123, 67);
            this.txtRgb.Name = "txtRgb";
            this.txtRgb.ReadOnly = true;
            this.txtRgb.Size = new System.Drawing.Size(150, 27);
            this.txtRgb.TabIndex = 2;
            this.txtRgb.Click += new System.EventHandler(this.TxtRgb_Click);
            // 
            // ColorTimer
            // 
            this.ColorTimer.Tick += new System.EventHandler(this.ColorTimer_Tick);
            // 
            // BtnPicker
            // 
            this.BtnPicker.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnPicker.Location = new System.Drawing.Point(23, 107);
            this.BtnPicker.Name = "BtnPicker";
            this.BtnPicker.Size = new System.Drawing.Size(80, 30);
            this.BtnPicker.TabIndex = 3;
            this.BtnPicker.Text = "PICK!";
            this.BtnPicker.UseVisualStyleBackColor = true;
            this.BtnPicker.Click += new System.EventHandler(this.BtnPicker_Click);
            // 
            // lblToast
            // 
            this.lblToast.AutoSize = true;
            this.lblToast.BackColor = System.Drawing.Color.LightYellow;
            this.lblToast.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblToast.Location = new System.Drawing.Point(219, 380);
            this.lblToast.Name = "lblToast";
            this.lblToast.Size = new System.Drawing.Size(41, 17);
            this.lblToast.TabIndex = 4;
            this.lblToast.Text = "label1";
            this.lblToast.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblToast.Visible = false;
            // 
            // ToastTimer
            // 
            this.ToastTimer.Interval = 2000;
            this.ToastTimer.Tick += new System.EventHandler(this.ToastTimer_Tick);
            // 
            // BtnAddPalette
            // 
            this.BtnAddPalette.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnAddPalette.Location = new System.Drawing.Point(296, 128);
            this.BtnAddPalette.Name = "BtnAddPalette";
            this.BtnAddPalette.Size = new System.Drawing.Size(179, 41);
            this.BtnAddPalette.TabIndex = 5;
            this.BtnAddPalette.Text = "+ 팔레트에 추가";
            this.BtnAddPalette.UseVisualStyleBackColor = true;
            this.BtnAddPalette.Click += new System.EventHandler(this.BtnAddPalette_Click);
            // 
            // listPalette
            // 
            this.listPalette.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listPalette.FullRowSelect = true;
            this.listPalette.HideSelection = false;
            this.listPalette.Location = new System.Drawing.Point(123, 175);
            this.listPalette.Name = "listPalette";
            this.listPalette.Size = new System.Drawing.Size(352, 240);
            this.listPalette.TabIndex = 6;
            this.listPalette.UseCompatibleStateImageBehavior = false;
            this.listPalette.View = System.Windows.Forms.View.Details;
            this.listPalette.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListPalette_KeyDown);
            this.listPalette.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ListPalette_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "색상";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "HEX";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "RGB";
            this.columnHeader3.Width = 101;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "라벨";
            this.columnHeader4.Width = 87;
            // 
            // PaletteContextMenu
            // 
            this.PaletteContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyHexMenuItem,
            this.CopyRgbMenuItem,
            this.toolStripSeparator1,
            this.DeleteMenuItem});
            this.PaletteContextMenu.Name = "contextMenuStrip1";
            this.PaletteContextMenu.Size = new System.Drawing.Size(125, 76);
            // 
            // CopyHexMenuItem
            // 
            this.CopyHexMenuItem.Name = "CopyHexMenuItem";
            this.CopyHexMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CopyHexMenuItem.Text = "Hex 복사";
            this.CopyHexMenuItem.Click += new System.EventHandler(this.CopyHexMenuItem_Click);
            // 
            // CopyRgbMenuItem
            // 
            this.CopyRgbMenuItem.Name = "CopyRgbMenuItem";
            this.CopyRgbMenuItem.Size = new System.Drawing.Size(124, 22);
            this.CopyRgbMenuItem.Text = "RGB 복사";
            this.CopyRgbMenuItem.Click += new System.EventHandler(this.CopyRgbMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(124, 22);
            this.DeleteMenuItem.Text = "삭제";
            this.DeleteMenuItem.Click += new System.EventHandler(this.DeleteMenuItem_Click);
            // 
            // RecentColorsPanel
            // 
            this.RecentColorsPanel.Location = new System.Drawing.Point(23, 175);
            this.RecentColorsPanel.Name = "RecentColorsPanel";
            this.RecentColorsPanel.Size = new System.Drawing.Size(80, 240);
            this.RecentColorsPanel.TabIndex = 7;
            // 
            // notifyPicker
            // 
            this.notifyPicker.ContextMenuStrip = this.TrayContextMenu;
            this.notifyPicker.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyPicker.Icon")));
            this.notifyPicker.Text = "Kolor Picker";
            this.notifyPicker.Visible = true;
            this.notifyPicker.DoubleClick += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // TrayContextMenu
            // 
            this.TrayContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PickMenuItem,
            this.OpenMenuItem,
            this.toolStripSeparator2,
            this.ExitMenuItem});
            this.TrayContextMenu.Name = "TrayContextMenu";
            this.TrayContextMenu.Size = new System.Drawing.Size(127, 76);
            // 
            // PickMenuItem
            // 
            this.PickMenuItem.Name = "PickMenuItem";
            this.PickMenuItem.Size = new System.Drawing.Size(126, 22);
            this.PickMenuItem.Text = "색 고르기";
            this.PickMenuItem.Click += new System.EventHandler(this.PickMenuItem_Click);
            // 
            // OpenMenuItem
            // 
            this.OpenMenuItem.Name = "OpenMenuItem";
            this.OpenMenuItem.Size = new System.Drawing.Size(126, 22);
            this.OpenMenuItem.Text = "창 열기";
            this.OpenMenuItem.Click += new System.EventHandler(this.OpenMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(123, 6);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ExitMenuItem.Text = "종료";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(494, 434);
            this.Controls.Add(this.RecentColorsPanel);
            this.Controls.Add(this.BtnAddPalette);
            this.Controls.Add(this.lblToast);
            this.Controls.Add(this.BtnPicker);
            this.Controls.Add(this.txtRgb);
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.listPalette);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kolor Picker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            this.PaletteContextMenu.ResumeLayout(false);
            this.TrayContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.TextBox txtRgb;
        private System.Windows.Forms.Timer ColorTimer;
        private System.Windows.Forms.Button BtnPicker;
        private System.Windows.Forms.Label lblToast;
        private System.Windows.Forms.Timer ToastTimer;
        private System.Windows.Forms.Button BtnAddPalette;
        private System.Windows.Forms.ListView listPalette;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ContextMenuStrip PaletteContextMenu;
        private System.Windows.Forms.ToolStripMenuItem CopyHexMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CopyRgbMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem DeleteMenuItem;
        private System.Windows.Forms.FlowLayoutPanel RecentColorsPanel;
        private System.Windows.Forms.NotifyIcon notifyPicker;
        private System.Windows.Forms.ContextMenuStrip TrayContextMenu;
        private System.Windows.Forms.ToolStripMenuItem PickMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
    }
}

