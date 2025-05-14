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
    public partial class Form1 : Form
    {
        public class PaletteItem
        {
            public string Hex { get; set; }
            public string Rgb { get; set; }
            public string Label { get; set; }
        }

        //[DllImport("user32.dll")]
        //public static extern IntPtr GetDC(IntPtr hWnd);

        //[DllImport("gdi32.dll")]
        //public static extern uint GetPixel(IntPtr hdc, int x, int y);

        //[DllImport("user32.dll")]
        //public static extern int ReleaseDC(IntPtr hWnd, IntPtr hdc);

        IKeyboardMouseEvents globalHook = Hook.GlobalEvents();
        private readonly string paletteFilePath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Palette.json");
        private TextBox editBox;
        private Queue<Color> recentColors = new Queue<Color>();
        private const int MaxRecent = 10;
        //private Queue<string> 
        private Point lastCursor = Point.Empty;
        private readonly MiniForm miniForm = new MiniForm();
        private readonly ZoomForm zoomForm = new ZoomForm();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            miniForm.Show();
            miniForm.Visible = false;
            zoomForm.Show();
            zoomForm.Visible = false;

            globalHook = Hook.GlobalEvents();
            globalHook.OnCombination(new Dictionary<Combination, Action>
            {
                { Combination.FromString("Control+Shift+C"), () => BtnPicker_Click(sender, e) }
            }); 
            globalHook.MouseClick += GlobalMouseClick;
            globalHook.MouseWheelExt += GlobalMouseWheel;
            LoadPalette();
        }

        private void GlobalMouseClick(object sender, EventArgs e)
        {
            if (ColorTimer.Enabled)
            {
                ShowMainWindow();
                TxtHex_Click(sender, e);
                ColorTimer.Stop();
                miniForm.Visible = false;
                zoomForm.Visible = false;
            }
        }

        private void GlobalMouseWheel(object sender, MouseEventExtArgs e)
        {
            if (ModifierKeys == Keys.Shift && ColorTimer.Enabled)
            {
                e.Handled = true;
                int delta = e.Delta > 0 ? 1 : -1;
                if(delta < 0 && zoomForm.ZoomFactor == 2)
                {
                    zoomForm.Visible = false;
                    if(zoomForm.zoomedBitmap != null)
                    {
                        zoomForm.zoomedBitmap.Dispose();
                        zoomForm.zoomedBitmap = null;
                    }
                }
                else
                {
                    zoomForm.Visible = true;
                    zoomForm.ZoomFactor = Math.Max(2, Math.Min(10, zoomForm.ZoomFactor + delta));
                    zoomForm.UpdateZoom(Cursor.Position);
                }
            }
            e.Handled = false;
        }



        private void BtnPicker_Click(object sender, EventArgs e)
        {
            if (ColorTimer.Enabled)
            {
                ShowMainWindow();
                ColorTimer.Stop();
                miniForm.Visible = false;
                zoomForm.Visible = false;
                if (zoomForm.zoomedBitmap != null)
                {
                    zoomForm.zoomedBitmap.Dispose();
                    zoomForm.zoomedBitmap = null;
                }
            }
            else
            {
                Hide();
                ColorTimer.Start();
                Point pos = Cursor.Position;
                miniForm.Location = new Point(pos.X + 10, pos.Y + 10);
                miniForm.Visible = true;
            }
        }

        //private Color GetCursorColor()
        //{
        //    Point cursorPos = Cursor.Position;
        //    IntPtr hdc = GetDC(IntPtr.Zero);
        //    uint pixel = GetPixel(hdc, cursorPos.X, cursorPos.Y);
        //    ReleaseDC(IntPtr.Zero, hdc);

        //    Color color = Color.FromArgb(
        //        (int)(pixel & 0x000000FF),           // R
        //        (int)(pixel & 0x0000FF00) >> 8,      // G
        //        (int)(pixel & 0x00FF0000) >> 16      // B
        //    );

        //    return color;
        //}

        private Color GetCursorColor(Point pos)
        {
            //Point pos = Cursor.Position;
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(pos, Point.Empty, new Size(1, 1));
                }
                return bmp.GetPixel(0, 0);
            }
        }


        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            Point pos = Cursor.Position;
            if (pos == lastCursor) return;
            lastCursor = pos;
            miniForm.Location = new Point(pos.X + 10, pos.Y + 10);
            Color currentColor = GetCursorColor(pos);
            txtHex.Text = $"#{currentColor.R:X2}{currentColor.G:X2}{currentColor.B:X2}";
            txtRgb.Text = $"{currentColor.R}, {currentColor.G}, {currentColor.B}";
            Preview.BackColor = currentColor;
            miniForm.Controls["miniHex"].Text = txtHex.Text;
            miniForm.Controls["miniRgb"].Text = txtRgb.Text;
            miniForm.Controls["colorPreview"].BackColor = currentColor;
        }

        private void TxtHex_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHex.Text) || !(sender is TextBox))
            {
                CopyColorCode(txtHex.Text);
                ShowToast("색상 HEX가 복사완료!");
            }
        }

        private void TxtRgb_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtHex.Text))
            {
                CopyColorCode(txtRgb.Text);
                ShowToast("색상 RGB가 복사완료!");
            }
        }

        private void CopyColorCode(string text)
        {
            Clipboard.SetText(text.Trim());    
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
            if (e.Button == MouseButtons.Left)
            {
                ListPalette_Left(sender, e);
            } else if(e.Button == MouseButtons.Right) {
                ListPalette_Right(sender, e);
            }
        }

        private void ListPalette_Left(object sender, MouseEventArgs e)
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
                    CopyColorCode(item.SubItems[1].Text);
                    ShowToast("색상 HEX가 복사완료!");
                    break;

                case 2: // RGB
                    CopyColorCode(item.SubItems[2].Text);
                    ShowToast("색상 RGB가 복사완료!");
                    break;

                case 3: // 라벨 편집
                    EditSubItemLabel(item, subIndex);
                    break;
            }
        }

        private void ListPalette_Right(object sender, MouseEventArgs e)
        {
            PaletteContextMenu.Show(listPalette, e.Location);
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
                SavePalette();

            };

            editBox.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    item.SubItems[subItemIndex].Text = editBox.Text;
                    this.Controls.Remove(editBox);
                    editBox.Dispose();
                    SavePalette();
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
            foreach (ListViewItem item in listPalette.Items)
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
                Color color = ColorTranslator.FromHtml(item.Hex);
                ListViewItem lvi = new ListViewItem(""); // 색상 셀
                lvi.SubItems.Add(item.Hex);
                lvi.SubItems.Add(item.Rgb);
                lvi.SubItems.Add(item.Label);
                lvi.SubItems[0].BackColor = color;
                lvi.UseItemStyleForSubItems = false;


                listPalette.Items.Add(lvi);
            }

            ShowToast("팔레트 불러오기 완료!");
        }

        private void ListPalette_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletePaletteItem();
            }
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            DeletePaletteItem();
        }

        private void DeletePaletteItem()
        {
            foreach (ListViewItem item in listPalette.SelectedItems)
            {
                listPalette.Items.Remove(item);
            }
            ShowToast("선택 항목 삭제됨");
            SavePalette();
        }

        private void CopyHexMenuItem_Click(object sender, EventArgs e)
        {
            CopyMenuItem(1);
            ShowToast("색상 HEX가 복사완료!");
        }

        private void CopyRgbMenuItem_Click(object sender, EventArgs e)
        {
            CopyMenuItem(2);
            ShowToast("색상 RGB가 복사완료!");
        }

        private void CopyMenuItem(int i)
        {
            List<string> textList = new List<string>();
            foreach (ListViewItem item in listPalette.SelectedItems)
            {
                textList.Add(item.SubItems[i].Text);
            }
            string text = string.Join(Environment.NewLine, textList);
            CopyColorCode(text);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if(e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            TrayContextMenu.Visible = false;
            globalHook?.Dispose();
            base.OnFormClosing(e);
        }

        private void PickMenuItem_Click(object sender, EventArgs e)
        {
            BtnPicker_Click(sender, e);
        }

        private void OpenMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            TrayContextMenu.Visible = false;
            Application.Exit();
        }

        private void ShowMainWindow()
        {
            Show(); // 창 보이기
            BringToFront(); // 다른 창 위로 올리기
            Activate();     // 포커스 주기
        }
    }
}

/*
Install-Package Gma.System.MouseKeyHook
Install-Package System.Text.Json
https://icon-icons.com/ko/%EC%95%84%EC%9D%B4%EC%BD%98/eyedropper/47201
Install-Package Costura.Fody

*/