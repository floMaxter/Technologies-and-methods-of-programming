using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TP_All_Labs
{
    public partial class Form3 : Form
    {
        // Поле - главная форма
        private MainFrame mf;
        // Поле хранящее состояние курсора (нажат или нет)
        private bool mouseDown = false;
        // Поле хранящее наше изображение
        private Bitmap bitmap = new Bitmap(1280, 720);
        // Поле - объект интерфеса реализующий рисования
        Graphics graphics;
        // Поле - интрументы для рисования
        Pen pen = new Pen(Color.Black, 3);
        Pen my = new Pen(Color.Black, 4f);
        Pen erase = new Pen(Color.White, 10);
        // Поле - высота pictureBox
        int myHeight = 720;
        // Поле - ширина pictureBox
        int myWidth = 1280;
        // Поле - коэфицент приблежения, для определения степени zoom'а
        double cof;
        // Поле - текущий пиксель курсора на оси x
        Point px;
        // Поле - текущий пиксель курсора на оси y
        Point py;
        // Поле - каким инструментов работаем (перо, стирательная резинка, элипс, треугольник, линия)
        int ind = 1;
        // Поля необходимые для отрисовки фигур
        int x, y, sX, sY, cX, cY;

        public Form3(MainFrame mf)
        {
            this.mf = mf;
            InitializeComponent();
            SetSize();
            cof = 1;
        }

        // Установка всех размеров
        private void SetSize()
        {   
            // Вспомогательные настройки
            pictureBox.Width = myWidth;
            pictureBox.Height = myHeight;

            // Основные настройки
            bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox.Image = bitmap;

            //Дополнительная настройка "пера" для плавности линий
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        // Нажатие мышки
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;

            py = e.Location;
            py.X = (int)Math.Round(py.X / cof);
            py.Y = (int)Math.Round(py.Y / cof);

            cX = (int)Math.Round(e.X / cof);
            cY = (int)Math.Round(e.Y / cof);
        }

        // Отпуск нажатия
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;

            sX = x - cX;
            sY = y - cY;
            if (ind == 3)
            {
                graphics.DrawEllipse(pen, cX, cY, sX, sY);
            }
            if (ind == 4)
            {
                graphics.DrawRectangle(pen, cX, cY, sX, sY);
            }
            if (ind == 5)
            {
                graphics.DrawLine(pen, cX, cY, x, y);
            }
            if (ind == 6)
            {

            }
            pictureBox.Refresh();
        }

        // Движение пера (мышки)
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) { return; }

            if (ind == 1)
            {
                px = e.Location;
                px.X = (int)Math.Round(px.X / cof);
                px.Y = (int)Math.Round(px.Y / cof);


                graphics.DrawLine(pen, px, py);
                py = px;
            }
            if (ind == 2)
            {
                px = e.Location;
                px.X = (int)Math.Round(px.X / cof);
                px.Y = (int)Math.Round(px.Y / cof);
                graphics.DrawLine(erase, px, py);
                py = px;
            }

            pictureBox.Refresh();
            x = (int)Math.Round(e.X / cof);
            y = (int)Math.Round(e.Y / cof);
            sX = (int)Math.Round(e.X / cof) - cX;
            sY = (int)Math.Round(e.Y / cof) - cY;
        }

        // Смена цвета пера
        private void colorButton1_Click(object sender, EventArgs e)
        {
            pen.Color = ((Button)sender).BackColor;
        }

        // Смена цвета пера
        private void colorButton8_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
                ((Button)sender).BackColor = colorDialog1.Color;
            }
        }

        // Создание нового листа
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
               "Вы действительно хотите создать новый лист?",
               "Внимание",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning
           );
            if (dialog == DialogResult.Yes)
            {
                graphics.Clear(pictureBox.BackColor);
                pictureBox.Image = bitmap;
            }
        }

        private void button_Pen_Click(object sender, EventArgs e)
        {
            ind = 1;
        }

        private void button_erase_Click(object sender, EventArgs e)
        {
            ind = 2;
        }

        private void button_Ellipse_Click(object sender, EventArgs e)
        {
            ind = 3;
        }

        private void button_Rectangle_Click(object sender, EventArgs e)
        {
            ind = 4;
        }

        private void button_Furie_Click(object sender, EventArgs e)
        {
            _3labFurieSettings settings = new _3labFurieSettings(this);
            settings.ShowDialog();

            graphics.Clear(pictureBox.BackColor);
            //pictureBox.Image = bitmap;

            int frequency = (int)settings.numericUpDown_Frequency.Value; 
            int amplitude = (int)settings.numericUpDown_Amplitude.Value;
            int harmonics = (int)settings.numericUpDown_Harmonics.Value;

            int angle = 0, interval = pictureBox.Width;
            double y = 0, y1, y2;

            Arrayoints array = new Arrayoints(interval);

            if (settings.radioButton1.Checked)
            {
                for (int i = 0; i < interval; i++)
                {
                    for (int j = 1; j < frequency; j++)
                    {
                        y1 = amplitude / ((2 * j) - 1);
                        y2 = Math.Sin(((j * 2) - 1) * harmonics * 0.01397 * angle);
                        y += +y1 * y2;
                    }
                    array.setPoint(i, pictureBox.Height / 2 + (int)Math.Truncate(y));
                    y = 0;
                    angle++;
                }
                pictureBox.Image = bitmap;
                graphics.DrawLines(pen, array.GetPoints());
            }

            if (settings.radioButton2.Checked)
            {
                for (int i = 0; i < interval; i++)
                {
                    for (int j = 1; j < frequency; j++)
                    {
                        y1 = amplitude / (((2 * j) - 1) * ((2 * j) - 1));
                        y2 = Math.Cos(((j * 2) - 1) * harmonics * 0.01397 * angle);
                        y = y + y1 * y2;
                    }
                    array.setPoint(i, pictureBox.Height / 2 + (int)Math.Truncate(y));
                    y = 0;
                    angle++;
                }
                pictureBox.Image = bitmap;
                graphics.DrawLines(pen, array.GetPoints());
            }

            if (settings.radioButton3.Checked)
            {
                for (int i = 0; i < interval; i++)
                {
                    for (int j = 1; j < frequency; j++)
                    {
                        y1 = amplitude / j;
                        y2 = Math.Sin(j * harmonics * 0.01397 * angle);
                        y += (+y1 * y2);
                    }
                    array.setPoint(i, pictureBox.Height / 2 + (int)Math.Truncate(y));
                    y = 0;
                    angle++;
                }
                pictureBox.Image = bitmap;
                graphics.DrawLines(pen, array.GetPoints());
            }
            array.resetPoints();

            settings.Close();
        }

        private void button__Click(object sender, EventArgs e)
        {
            ind = 5;
        }

        // Установление ширины линии 
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pen.Width = trackBar1.Value;
        }

        // Сохранение рисунка
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            // Ставим расширение сохраняемого файла по умолчанию
            saveFile.Filter = "JPG(*.JPG|*.jpg";
            // Открываем окно сохранения
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (pictureBox.Image != null)
                {
                    try
                    {
                        pictureBox.Image.Save(saveFile.FileName);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;
                try
                {
                    Rectangle rectangle = Screen.PrimaryScreen.Bounds;
                    Bitmap bitmaptmp2 = new Bitmap(file);
                    graphics.DrawImage(bitmaptmp2, new Point());
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                    string stringToLog = "In ";
                    stringToLog += DateTime.Now.ToString();
                    stringToLog += " in ThirdLabForm in method \"toolStripButton1_Click\" - FileNotFoundException";
                    mf.setExeptionInLog(stringToLog);
                    return;
                }
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            double zoom = trackBar2.Value < 0 ? Math.Pow(2f, trackBar2.Value) : trackBar2.Value+1f;
            //zoom = zoom * 0.5;
            cof = zoom;
            pictureBox.Size = new Size((int)Math.Round(myWidth * zoom), (int)Math.Round(myHeight * zoom));
            graphics = Graphics.FromImage(pictureBox.Image);
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
    }
}
