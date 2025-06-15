using Grpc.Net.Client;
using MultidrawingService;
using System.Drawing.Drawing2D;

namespace MultidrawingClient
{
    public partial class ClientForm : Form
    {
        List<GraphicsPath> remote_paths;

        List<Draw> original;

        GraphicsPath path;

        bool isPen;
        bool isEraser;

        GrpcChannel channel;
        DrawGreeter.DrawGreeterClient client;

        public ClientForm()
        {
            InitializeComponent();

            remote_paths = new List<GraphicsPath>();

            original = new List<Draw>();

            isPen = false;

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            InitializeGrpcClient();

            timer1.Tick += Timer1_Tick;
            timer1.Interval = 500;

            timer1.Start();
        }

        private void Timer1_Tick(object? sender, EventArgs e)
        {
            var response = client.ReceiveDraw(new ReceiveDrawRequest());

            remote_paths.Clear();
            original.Clear();

            remote_paths.AddRange(response.DrawPath.Select(p => 
            {
                var graphicsPath = new GraphicsPath();
                foreach (var point in p.Path)
                {
                    graphicsPath.AddLine(new PointF(point.X, point.Y), new PointF(point.X, point.Y));
                }
                return graphicsPath;
            }));

            original.AddRange(response.DrawPath);

            Canva.Refresh();
            Canva.Invalidate();
        }

        private void InitializeGrpcClient()
        {
            channel = GrpcChannel.ForAddress("http://localhost:5001");
            client = new DrawGreeter.DrawGreeterClient(channel);
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
            Pen mypR = new Pen(System.Drawing.Color.Red, 4);

            foreach (var p in remote_paths)
            {
                g.DrawPath(mypR, p);
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
                foreach(var p in remote_paths)
                {
                    if (p.IsVisible(e.Location))
                    {
                        remote_paths.Remove(p);

                        var response = client.RemoveDraw(new RemoveDrawRequest
                        {
                            Id = original.FirstOrDefault(d => d.Path.SequenceEqual(p.PathPoints.Select(pp => new MultidrawingService.Path() { X = pp.X, Y = pp.Y })))?.Id ?? 0
                        });

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
                var drawPath = new Draw();

                foreach(var p in path.PathPoints)
                {
                    drawPath.Path.Add(new MultidrawingService.Path() { X = p.X, Y=p.Y});
                }

                drawPath.Id = remote_paths.Count + 1;

                var response = client.SendDraw(new SendDrawRequest
                {
                    DrawPath = drawPath
                });

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
