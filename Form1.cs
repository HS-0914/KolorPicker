using System;
using System.Collections.Generic;
using System.Drawing;
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

        IKeyboardMouseEvents globalHook = Hook.GlobalEvents();
        private readonly string paletteFilePath = Path.Combine(Application.StartupPath, "Palette.json");
        private List<PaletteItem> paletteItems = new List<PaletteItem>();
        private TextBox editBox;
        private Point lastCursor = Point.Empty;
        private readonly MiniForm miniForm = new MiniForm();
        private readonly ZoomForm zoomForm = new ZoomForm();
        private readonly string formColor = "#EFEFEF";

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }
            TrayContextMenu.Visible = false;
            globalHook?.Dispose();
            base.OnFormClosing(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml(formColor);
            FormInit();
            GlobalInit();
            LoadPalette();
        }

        private void FormInit()
        {
            miniForm.Show();
            zoomForm.Show();
            miniForm.Visible = false;
            zoomForm.Visible = false;
        }

        private void GlobalInit()
        {
            globalHook = Hook.GlobalEvents();
            globalHook.OnCombination(new Dictionary<Combination, Action>
            {
                { Combination.FromString("Control+Shift+C"), () => TogglePreview() }
            });
            globalHook.MouseClick += GlobalMouse_Click;
            globalHook.MouseWheelExt += GlobalMouse_Wheel;
        }

        private void LoadPalette()
        {
            if (!File.Exists(paletteFilePath)) return;

            string json = File.ReadAllText(paletteFilePath);
            paletteItems = JsonSerializer.Deserialize<List<PaletteItem>>(json);
            listPalette.Items.Clear();

            foreach (PaletteItem item in paletteItems)
            {
                ListViewItem lvi = CreatePaletteItem(item);

                listPalette.Items.Add(lvi);
            }
            ShowToast("팔레트 불러오기 완료!");
        }

        private ListViewItem CreatePaletteItem(PaletteItem item)
        {
            Color color = ColorTranslator.FromHtml(item.Hex);

            ListViewItem lvi = new ListViewItem(""); // 색상 셀
            lvi.SubItems[0].BackColor = color;
            lvi.SubItems.Add(item.Hex);
            lvi.SubItems.Add(item.Rgb);
            lvi.SubItems.Add(item.Label);
            lvi.UseItemStyleForSubItems = false;

            return lvi;
        }

        private void GlobalMouse_Click(object sender, EventArgs e)
        {
            if (ColorTimer.Enabled) StopPreview();
        }

        private void GlobalMouse_Wheel(object sender, MouseEventExtArgs e)
        {
            if (ModifierKeys != Keys.Shift || !ColorTimer.Enabled) return;

            e.Handled = true;

            int delta = e.Delta > 0 ? 1 : -1;
            if(delta < 0 && zoomForm.ZoomFactor == 2)
            {
                ResetZoomForm();
                return;
            }
            zoomForm.Visible = true;
            zoomForm.ZoomFactor = Math.Max(2, Math.Min(10, zoomForm.ZoomFactor + delta));
            zoomForm.UpdateZoom(Cursor.Position);

            miniForm.TopMost = false;
            miniForm.TopMost = true;
        }



        private void BtnPicker_Click(object sender, EventArgs e)
        {
            TogglePreview();
        }

        private void TogglePreview()
        {
            if (ColorTimer.Enabled)
            {
                StopPreview();
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

        private void StopPreview()
        {
            ColorTimer.Stop();
            miniForm.Visible = false;
            ShowMainWindow();
            ResetZoomForm();
        }

        private void ShowMainWindow()
        {
            Show(); // 창 보이기
            BringToFront(); // 다른 창 위로 올리기
            Activate();     // 포커스 주기
            TopMost = true;
            TopMost = false;
        }

        private void ResetZoomForm()
        {
            zoomForm.Visible = false;
            zoomForm.ZoomFactor = 2;
            if (zoomForm.zoomedBitmap != null)
            {
                zoomForm.zoomedBitmap.Dispose();
                zoomForm.zoomedBitmap = null;
            }
        }
       
        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            Point pos = Cursor.Position;
            if (pos == lastCursor) return;

            lastCursor = pos;
            Color currentColor = GetCursorColor(pos);
            txtHex.Text = $"#{currentColor.R:X2}{currentColor.G:X2}{currentColor.B:X2}";
            txtRgb.Text = $"{currentColor.R}, {currentColor.G}, {currentColor.B}";
            ColorView.BackColor = currentColor;

            miniForm.UpdatePreview(pos, currentColor);
        }

        private Color GetCursorColor(Point pos)
        {
            using (Bitmap bmp = new Bitmap(1, 1))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(pos, Point.Empty, new Size(1, 1));
                }
                return bmp.GetPixel(0, 0);
            }
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
            if (string.IsNullOrWhiteSpace(txtHex.Text)) return;
            PaletteItem pi = new PaletteItem
            {
                Hex = txtHex.Text,
                Rgb = txtRgb.Text,
                Label = "새 색상"
            };
            paletteItems.Add(pi);
            SavePalette();

            ListViewItem lvi = CreatePaletteItem(pi);
            listPalette.Items.Add(lvi);
            ShowToast("팔레트에 추가되었습니다!");
        }

        private void SavePalette()
        {
            string json = JsonSerializer.Serialize(paletteItems, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(paletteFilePath, json);
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

        private void PaletteToPreview(ListViewItem item)
        {
            ColorView.BackColor = item.SubItems[0].BackColor;
            txtHex.Text = item.SubItems[1].Text;
            txtRgb.Text = item.SubItems[2].Text;
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
            // 역순으로 삭제 (데이터 밀림 방지)
            var selectedIndices = listPalette.SelectedIndices;
            for (int i = selectedIndices.Count - 1; i >= 0; i--)
            {
                int index = selectedIndices[i];
                listPalette.Items.RemoveAt(index);
                paletteItems.RemoveAt(index);
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

        private void PickMenuItem_Click(object sender, EventArgs e)
        {
            TogglePreview();
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
    }
}

/*
Install-Package Gma.System.MouseKeyHook
Install-Package System.Text.Json
https://icon-icons.com/ko/%EC%95%84%EC%9D%B4%EC%BD%98/eyedropper/47201
Install-Package Costura.Fody
*/