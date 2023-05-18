using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabProject
{
    public partial class Laba3Fu : Form
    {
        private Form1 mainform;

        private Bitmap bmp_pifagor;
        private Bitmap bmp_serpinski;
        private Bitmap bmp_pop;
        private int tree;
        private int serp;
        private String pop;

        public Laba3Fu(Form1 f)
        {
            InitializeComponent();
            mainform = f;
        }

        public Laba3Fu()
        {
            InitializeComponent();
        }

        //смоделировать
        private void button1_Click(object sender, EventArgs e)
        {
            int it = ((int)numericUpDown1.Value);
            Task task;

            switch (tabControl1.SelectedIndex)
            {
                case 0: task = Task.Run(() => pifagor_tree(it)); break;
                case 1: task = Task.Run(() => serpinski(it)); break;
                case 2: task = Task.Run(() => popTask(it)); break;
                default: break;
            }
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
            pictureBox1.Image = bmp_pifagor;

        }

        private void serpinski(int it)
        {
            int size = 5000;
            bmp_serpinski = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(bmp_serpinski);

            int red = 255;
            serp = it;
            DrawTriangle(g, red, new PointF(size / 2, 0), new PointF(0, size), new PointF(size, size), it);
            pictureBox2.Image = bmp_serpinski;
        }

        private void DrawTriangle(Graphics g, int red, PointF top, PointF left, PointF right, int level)
        {
            PointF[] points = new PointF[3] { top, right, left };
            SolidBrush brush = new SolidBrush(Color.FromArgb(red, 127, 127));
            g.FillPolygon(brush, points);
            if (level > 0)
            {
                red = 255 * level/ serp;
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
            pictureBox3.Image = bmp_pop;

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
    }
}
