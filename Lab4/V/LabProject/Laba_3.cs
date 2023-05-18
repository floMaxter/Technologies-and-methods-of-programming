using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabProject
{
    public partial class Laba_3 : Form
    {
        private Form1 mainform;
        public Laba_3()
        {
            InitializeComponent();
        }

        public Laba_3(Form1 f)
        {
            InitializeComponent();
            mainform = f;

            setSize();
        }

        //color for pen
        private void button4_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        private class Arrayoints
        {
            private int index = 0;
            private Point[] points;

            public Arrayoints(int size)
            {
                if (size <= 0)
                {
                    size = 2;
                }
                points = new Point[size];
            }

            public void setPoint(int x, int y)
            {
                if (index >= points.Length)
                {
                    index = 0;
                }

                points[index] = new Point(x, y);
                index++;
            }

            public void resetPoints()
            {
                index = 0;
            }
            public int GetCountPoints()
            {
                return index;
            }
            public Point[] GetPoints()
            {
                return points;
            }
        }

        private bool holdMouse = false;
        private Arrayoints arrayoints = new Arrayoints(2);
        Bitmap map = new Bitmap(100, 100);
        Graphics graphics;
        Pen pen = new Pen(Color.Black, 3f);

        private void setSize() 
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            map = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(map);
            pictureBox1.Image = map;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            holdMouse = true;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            holdMouse = false;
            arrayoints.resetPoints();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!holdMouse)
            {
                return;
            }
            
            arrayoints.setPoint(e.X, e.Y);
            if (arrayoints.GetCountPoints() >= 2)
            {
                graphics.DrawLines(pen, arrayoints.GetPoints());
                pictureBox1.Image = map;
                arrayoints.setPoint(e.X, e.Y);
            }
        }

        //change color
        private void button11_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        //save
        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Save(saveFileDialog1.FileName);
                }
            }
        }

        //clear
        private void button2_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;
        }

        //open 
        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "JPG(*.JPG)|*.jpg";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                map = new Bitmap(Image.FromFile(openFileDialog1.FileName));
                graphics = Graphics.FromImage(map);
            }
        }

        //line thickness
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        //Type pen
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Triangle;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Triangle;
        }

        //buttonDraw
        private void button13_Click(object sender, EventArgs e)
        {
            graphics.Clear(pictureBox1.BackColor);
            pictureBox1.Image = map;

            int acc = (int)numericUpDown1.Value; //частота
            int A = (int)numericUpDown2.Value; //амплитуда
            int F = (int)numericUpDown3.Value; //гармоники

            int angle = 0, interval = pictureBox1.Width;
            double y = 0, y1, y2;

            Arrayoints array = new Arrayoints(interval);

            if (radioButton4.Checked)
            {
                for (int i = 0; i < interval; i++)
                {
                    for (int j = 1; j < acc; j++)
                    {
                        y1 = A / ((2 * j) - 1);
                        y2 = Math.Sin(((j * 2) - 1) * F * 0.01397 * angle);
                        y += + y1 * y2;
                    }
                    array.setPoint(i, pictureBox1.Height / 2 + (int)Math.Truncate(y));
                    y = 0;
                    angle++;
                }
                pictureBox1.Image = map;
                graphics.DrawLines(pen, array.GetPoints());
            }

            if (radioButton5.Checked) 
            {
                for (int i = 0; i < interval; i++)
                {
                    for (int j = 1; j < acc; j++)
                    {
                        y1 = A / (((2 * j) - 1) * ((2 * j) - 1));
                        y2 = Math.Cos(((j * 2) - 1) * F * 0.01397 * angle);
                        y = y + y1 * y2;
                    }
                    array.setPoint(i, pictureBox1.Height / 2 + (int)Math.Truncate(y));
                    y = 0;
                    angle++;
                }
                pictureBox1.Image = map;
                graphics.DrawLines(pen, array.GetPoints());

            }

            if (radioButton6.Checked)
            {
                for (int i = 0; i < interval; i++)
                {
                    for (int j = 1; j < acc; j++)
                    {
                        y1 = A / j;
                        y2 = Math.Sin(j * F * 0.01397 * angle);
                        y += (+y1 * y2);
                    }
                    array.setPoint(i, pictureBox1.Height / 2 + (int)Math.Truncate(y));
                    y = 0;
                    angle++;
                }
                pictureBox1.Image = map;
                graphics.DrawLines(pen, array.GetPoints());
            }
            array.resetPoints();
        }

        //color background
        private void button14_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog2.Color;
                graphics.Clear(pictureBox1.BackColor);
                ((Button)sender).BackColor = colorDialog2.Color;
            }
        }
    }
}
