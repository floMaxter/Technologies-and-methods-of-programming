using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace TP_All_Labs
{
    public partial class Form32 : Form
    {   
        // Главное поле
        private MainFrame mf;
        private Bitmap bmp_pifagor;
        private Bitmap bmp_serpinski;
        private Bitmap bmp_pop;
        private int tree;
        private int serp;
        private int dragon;
        private String pop;

        public Form32(MainFrame mf)
        {
            InitializeComponent();
            this.mf = mf;
        }


        private void button_pifagor_Click(object sender, EventArgs e)
        {
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            int it = ((int)numericUpDown1.Value);
            Task task;
            task = Task.Run(() => pifagor_tree(it));
        }

        private void button_serpinski_Click(object sender, EventArgs e)
        {
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            int it = ((int)numericUpDown1.Value);
            Task task;
            task = Task.Run(() => serpinski(it));
        }

        private void button_Pop_Click(object sender, EventArgs e)
        {
            this.pictureBox.SizeMode = PictureBoxSizeMode.Normal;
            int it = ((int)numericUpDown1.Value);
            Task task;
            task = Task.Run(() => popTask(it));
        }

        private void button_DragonCurve_Click(object sender, EventArgs e)
        {
            this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            int it = ((int)numericUpDown1.Value);
            Task task;
            task = Task.Run(() => Draw_dragon_curve(it));
        }

        private void pifagor_tree(int it)
        {
            double length = 100;
            int size = (int)length * 2 + it * 60;

            bmp_pifagor = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bmp_pifagor);

            tree = it;
            int red = 255;
            DrawSquare(g, red, new PointF((float)((size - length) / 2), size), it - 1, length, 0);
            pictureBox.Image = bmp_pifagor;

        }

        private void serpinski(int it)
        {
            int size = 5000;
            bmp_serpinski = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bmp_serpinski);

            int red = 255;
            serp = it;
            DrawTriangle(g, red, new PointF(size / 2, 0), new PointF(0, size), new PointF(size, size), it);
            pictureBox.Image = bmp_serpinski;
        }

        private void DrawTriangle(Graphics g, int red, PointF top, PointF left, PointF right, int level)
        {

            PointF[] points = new PointF[3] { top, right, left };
            SolidBrush brush = new SolidBrush(Color.FromArgb(red, 127, 127));
            g.FillPolygon(brush, points);
            if (level > 0)
            {
                red = 255 * level / serp;
                PointF leftMid = MidPoint(top, left);
                PointF rightMid = MidPoint(top, right);
                PointF topMid = MidPoint(left, right);
                DrawTriangle(g, red, top, leftMid, rightMid, level - 1);
                DrawTriangle(g, red, leftMid, left, topMid, level - 1);
                DrawTriangle(g, red, rightMid, topMid, right, level - 1);
            }
        }
        private PointF MidPoint(PointF p1, PointF p2)
        {
            return new PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        private void DrawSquare(Graphics g, int red, PointF bottom_left, int level, double length, double angle)
        {
            SizeF up = new SizeF((float)(Math.Sin(angle) * length), -(float)(Math.Cos(angle) * length));
            SizeF right = new SizeF((float)(Math.Cos(angle) * length), (float)(Math.Sin(angle) * length));
            PointF[] points = new PointF[4] { bottom_left, bottom_left + right, bottom_left + up + right, bottom_left + up };

            SolidBrush brush = new SolidBrush(Color.FromArgb(red, 0, 0));
            g.FillPolygon(brush, points);
            if (level > 0)
            {
                red = 255 * level / tree;
                length = length / Math.Sqrt(2);
                DrawSquare(g, red, points[3], level - 1, length, angle - Math.PI / 4);
                DrawSquare(g, red, points[3] + new SizeF((float)(Math.Cos(angle - Math.PI / 4) * length), (float)(Math.Sin(angle - Math.PI / 4) * length)), level - 1, length, angle + Math.PI / 4);
            }
        }

        private void popTask(int it)
        {
            int size = 5000;
            bmp_pop = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bmp_pop);


            o_sobake(it);
            g.DrawString(pop, new Font("Times New Roman", 14), Brushes.Black, 2, 2);
            pictureBox.Image = bmp_pop;

            pop = null;
        }

        private void o_sobake(int level)
        {
            if (level > 0)
            {
                Thread.Sleep(500);

                pop += Environment.NewLine;
                pop += Environment.NewLine + "“Как это было:";
                pop += Environment.NewLine + "“У попа была собака -он ее любил”";
                pop += Environment.NewLine + "Она съела кусок мяса -он ее убил.”";
                pop += Environment.NewLine + "“Убил и закопал и на могиле написал:”";

                o_sobake(level - 1);
            }
        }

        void dragon_func(int x1, int y1, int x2, int y2, int n, Graphics g, int red)
        {
            int xn, yn;
            var drawingPen = new Pen((Color.FromArgb(red, 127, 127)), 6f);

            if (n > 0)
            {   
                red = 255 * n / dragon;
                xn = (x1 + x2) / 2 + (y2 - y1) / 2;
                yn = (y1 + y2) / 2 - (x2 - x1) / 2;

                dragon_func(x2, y2, xn, yn, n - 1, g, red);
                dragon_func(x1, y1, xn, yn, n - 1, g, red);
            }

            Point point1 = new Point(x1, y1);
            Point point2 = new Point(x2, y2);
            g.DrawLine(drawingPen, point1, point2);
        }

        private void Draw_dragon_curve(int depth)
        {
            int x1, y1, x2, y2;
            int k = depth;

            x1 = (pictureBox.Width);
            y1 = (pictureBox.Height * 2);
            x2 = (int)(pictureBox.Width * 2.7f);
            y2 = (int)(pictureBox.Height * 4);

            int size = 5000;
            bmp_serpinski = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bmp_serpinski);
            int red = 255;
            dragon = depth;

            dragon_func(x1, y1, x2, y2, k, g, red);
            pictureBox.Image = bmp_serpinski;
        }
    }
}
