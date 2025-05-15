using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolorPicker
{
    public partial class ZoomForm: Form
    {
        public Bitmap zoomedBitmap;
        public int ZoomFactor { get; set; } = 2;
        private const int zoomSize = 50;
        private Point captureCursor;

        public ZoomForm()
        {
            InitializeComponent();
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

        private void ApplyRoundedCorners(int radius)
        {
            var bounds = new Rectangle(0, 0, this.Width, this.Height);
            var path = new GraphicsPath();

            int r = radius * 2;

            path.AddArc(bounds.X, bounds.Y, r, r, 180, 90);
            path.AddArc(bounds.Right - r, bounds.Y, r, r, 270, 90);
            path.AddArc(bounds.Right - r, bounds.Bottom - r, r, r, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - r, r, r, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedCorners(10);
        }

        public void UpdateZoom(Point cursorPos)
        {
            int formSize = ZoomFactor * zoomSize;
            if(zoomedBitmap == null)
            {
                Rectangle captureArea = new Rectangle(
                cursorPos.X - zoomSize / 2,
                cursorPos.Y - zoomSize / 2,
                zoomSize,
                zoomSize
                );
                zoomedBitmap = new Bitmap(zoomSize, zoomSize);
                using (Graphics g = Graphics.FromImage(zoomedBitmap))
                {
                    g.CopyFromScreen(captureArea.Location, Point.Empty, captureArea.Size);
                }
                Location = new Point(cursorPos.X - formSize / 2, cursorPos.Y - formSize / 2);
                captureCursor = cursorPos;
            } 
            else
            {
                Location = new Point(captureCursor.X - formSize / 2, captureCursor.Y - formSize / 2);

            }
            Size = new Size(formSize, formSize);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (zoomedBitmap != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
                e.Graphics.DrawImage(zoomedBitmap, new Rectangle(0, 0, Width, Height));
            }
        }
    }
}
