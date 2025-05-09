using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolorPicker
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int x, int y);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnPicker_Click(object sender, EventArgs e)
        {
            if(ColorTimer.Enabled)
            {
                ColorTimer.Stop();
            }
            else 
            {
                ColorTimer.Start();
            }
        }

        private Color GetCursorColor()
        {
            Point cursorPos = Cursor.Position;
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, cursorPos.X, cursorPos.Y);
            ReleaseDC(IntPtr.Zero, hdc);

            Color color = Color.FromArgb(
                (int)(pixel & 0x000000FF),           // R
                (int)(pixel & 0x0000FF00) >> 8,      // G
                (int)(pixel & 0x00FF0000) >> 16      // B
            );

            return color;
        }

        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            Color currentColor = GetCursorColor();
            Controls["txtHex"].Text = $"#{currentColor.R:X2}{currentColor.G:X2}{currentColor.B:X2}";
            Controls["txtRgb"].Text = $"{currentColor.R}, {currentColor.G}, {currentColor.B}";
            Controls["Preview"].BackColor = currentColor;
        }

        private void TxtHex_Click(object sender, EventArgs e)
        {
            if(sender is TextBox textBox)
            {
                string text = textBox.Text.Trim();
                Clipboard.SetText(text);
                ShowToast("색상 HEX가 복사완료!");
            }
        }

        private void TxtRgb_Click(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string text = textBox.Text.Trim();
                Clipboard.SetText(text);
                ShowToast("색상 RGB가 복사완료!");
            }
        }

        private void ShowToast(string message)
        {
            lblToast.Text = message;
            int x = (ClientSize.Width - lblToast.PreferredWidth) / 2;
            lblToast.Location = new Point(x, lblToast.Location.Y);

            lblToast.Visible = true;
            ToastTimer.Start();
        }

        private void ToastTimer_Tick(object sender, EventArgs e)
        {
            lblToast.Visible = false;
            ToastTimer.Stop();
        }
    }
}
