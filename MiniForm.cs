using System.Drawing;
using System.Windows.Forms;

namespace KolorPicker
{
    public partial class MiniForm: Form
    {
        public MiniForm()
        {
            InitializeComponent();
        }

        public void UpdatePreview(Point pos, Color previewColor)
        {
            Location = new Point(pos.X + 10, pos.Y + 10);
            miniHex.Text = $"#{previewColor.R:X2}{previewColor.G:X2}{previewColor.B:X2}";
            miniRgb.Text = $"{previewColor.R}, {previewColor.G}, {previewColor.B}";
            Preview.BackColor = previewColor;
        }

        // 그림자
        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x00020000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }
    }
}
