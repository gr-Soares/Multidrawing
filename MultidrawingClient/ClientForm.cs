using System.Drawing.Drawing2D;

namespace MultidrawingClient
{
    public partial class ClientForm : Form
    {
        List<GraphicsPath> paths;
        GraphicsPath path;

        bool isPen;
        bool isEraser;

        public ClientForm()
        {
            InitializeComponent();

            paths = new List<GraphicsPath>();
            isPen = false;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void Canva_MouseMove(object sender, MouseEventArgs e)
        {
            if (path == null)
                return;

            Point p = new Point(e.X, e.Y);

            if (e.Button == MouseButtons.Left && isPen)
            {
                path.AddLine(p, p);
                Canva.Refresh();
                Canva.Invalidate();
            }
        }

        private void Canva_Paint(object sender, PaintEventArgs e)
        {
            if (path == null)
                return;

            var g = e.Graphics;
            Pen myp = new Pen(System.Drawing.Color.Black, 4);

            foreach (var p in paths)
            {
                g.DrawPath(myp, p);
            }

            g.DrawPath(myp, path);

            g.Flush();
        }

        private void Canva_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isPen)
            {
                path = new GraphicsPath();
            }

            if (e.Button == MouseButtons.Left && isEraser)
            {
                foreach(var p in paths)
                {
                    if (p.IsVisible(e.Location))
                    {
                        paths.Remove(p);
                        Canva.Refresh();
                        Canva.Invalidate();
                        return;
                    }
                }
            }
        }

        private void Canva_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isPen)
            {
                paths.Add(path);
                path = new GraphicsPath();
            }
        }

        private void btPen_Click(object sender, EventArgs e)
        {
            isPen = !isPen;
            isEraser = false;
            btPen.Checked = isPen;
            btErase.Checked = isEraser;
        }

        private void btErase_Click(object sender, EventArgs e)
        {
            isEraser = !isEraser;
            isPen = false;
            btErase.Checked = isEraser;
            btPen.Checked = isPen;
        }
    }
}
