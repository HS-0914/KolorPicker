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
using System.Text.Json;
using System.IO;

using Gma.System.MouseKeyHook;

namespace KolorPicker
{
    public class PaletteItem
    {
        public string Hex { get; set; }
        public string Rgb { get; set; }
        public string Label { get; set; }
    }

    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int x, int y);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        IKeyboardMouseEvents globalHook = Hook.GlobalEvents();
        private readonly string paletteFilePath = Path.Combine(Application.StartupPath, "Palette.json");
        private TextBox editBox;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            globalHook = Hook.GlobalEvents();
            globalHook.MouseClick += GlobalMouseClick;
            LoadPalette();
        }

        private void GlobalMouseClick(object sender, EventArgs e)
        {
            if (ColorTimer.Enabled) ColorTimer.Stop();
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
            txtHex.Text = $"#{currentColor.R:X2}{currentColor.G:X2}{currentColor.B:X2}";
            txtRgb.Text = $"{currentColor.R}, {currentColor.G}, {currentColor.B}";
            Preview.BackColor = currentColor;
        }

        private void TxtHex_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txtHex.Text))
            {
                string text = txtHex.Text.Trim();
                Clipboard.SetText(text);
                ShowToast("색상 HEX가 복사완료!");
            }
        }

        private void TxtRgb_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHex.Text))
            {
                string text = txtRgb.Text.Trim();
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

        private void BtnAddPalette_Click(object sender, EventArgs e)
        {
            string hex = txtHex.Text;
            string rgb = txtRgb.Text;
            Color color = Preview.BackColor;

            if (string.IsNullOrWhiteSpace(hex)) return;

            ListViewItem item = new ListViewItem();
            item.Text = "";
            item.SubItems.Add(hex);
            item.SubItems.Add(rgb);
            item.SubItems.Add("새 색상");

            item.UseItemStyleForSubItems = false;
            item.SubItems[0].BackColor = color;
            listPalette.Items.Add(item);
            ShowToast("팔레트에 추가되었습니다!");
            SavePalette();
        }

        private void ListPalette_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo info = listPalette.HitTest(e.Location);
            ListViewItem item = info.Item;
            var subItem = info.SubItem;
            int subIndex = item.SubItems.IndexOf(subItem);

            if (item == null || subItem == null) return;

            switch (subIndex)
            {
                case 0:
                    PaletteToPreview(item);
                    break;

                case 1: // HEX
                    TxtHex_Click(sender, e);
                    break;

                case 2: // RGB
                    TxtRgb_Click(sender, e);
                    break;

                case 3: // 라벨 편집
                    EditSubItemLabel(item, subIndex);
                    break;
            }
        }


        private void EditSubItemLabel(ListViewItem item, int subItemIndex)
        {
            if (editBox != null && !editBox.IsDisposed)
            {
                this.Controls.Remove(editBox);
                editBox.Dispose();
            }

            Rectangle rect = item.SubItems[subItemIndex].Bounds;
            editBox = new TextBox
            {
                Bounds = rect,
                Text = item.SubItems[subItemIndex].Text,
                BorderStyle = BorderStyle.FixedSingle
            };

            listPalette.Controls.Add(editBox);
            editBox.Focus();

            editBox.LostFocus += (s, e) =>
            {
                item.SubItems[subItemIndex].Text = editBox.Text;
                this.Controls.Remove(editBox);
                editBox.Dispose();
            };

            editBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    item.SubItems[subItemIndex].Text = editBox.Text;
                    this.Controls.Remove(editBox);
                    editBox.Dispose();
                }
            };
        }

        private void PaletteToPreview(ListViewItem item)
        {
            Preview.BackColor = item.SubItems[0].BackColor;
            txtHex.Text = item.SubItems[1].Text;
            txtRgb.Text = item.SubItems[2].Text;
        }

        private void SavePalette()
        {
            List<PaletteItem> items = new List<PaletteItem>();
            foreach(ListViewItem item in listPalette.Items)
            {
                string hex = item.SubItems[1].Text;
                string rgb = item.SubItems[2].Text;
                string label = item.SubItems[3].Text;

                items.Add(new PaletteItem
                {
                    Hex = hex,
                    Rgb = rgb,
                    Label = label,
                });
            }
            string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(paletteFilePath, json);
        }

        private void LoadPalette()
        {
            if (!File.Exists(paletteFilePath)) return;

            string json = File.ReadAllText(paletteFilePath);
            var items = JsonSerializer.Deserialize<List<PaletteItem>>(json);

            listPalette.Items.Clear();

            foreach (var item in items)
            {
                ListViewItem lvi = new ListViewItem(""); // 색상 셀
                lvi.SubItems.Add(item.Hex);
                lvi.SubItems.Add(item.Rgb);
                lvi.SubItems.Add(item.Label);
                listPalette.Items.Add(lvi);
            }

            ShowToast("팔레트 불러오기 완료!");
        }
    }
}

/*
Install-Package Gma.System.MouseKeyHook
Install-Package System.Text.Json
 
*/