using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LabProject
{
    public partial class Laba_2 : Form
    {
        private StreamWriter logWriter;
        private int size = 12;
        private int frequency = 0;
        private int seed = 2777;
        private int k = 0;

        private Form1 mainform;
        public Laba_2()
        {
            InitializeComponent();
        }

        public Laba_2(Form1 f)
        {
            InitializeComponent();
            mainform = f;
            reloadGrid_in();
            reloadGrid_out();
            File.WriteAllText("log.txt", string.Empty);
            logWriter = new StreamWriter(new FileStream("log.txt", FileMode.OpenOrCreate));

        }

        private void reloadGrid_in()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Columns.Add(i.ToString(), i.ToString());
            }
            dataGridView1.Rows.Add(1);
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = null;
            }
            dataGridView1.Refresh();
        }

        private void reloadGrid_out()
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            for (int i = 0; i < size; i++)
            {
                dataGridView2.Columns.Add(i.ToString(), i.ToString());
            }
            dataGridView2.Rows.Add(1);
            for (int i = 0; i < size; i++)
            {
                dataGridView2.Rows[0].Cells[i].Value = null;
            }
            dataGridView2.Refresh();
        }

        private void WriteExeption(Exception e, string s)
        {
            string str = "Time: " + s + ": " + e.ToString() + '\n';
            mainform.textBox1.Text += str;
            logWriter.Write(str);
            logWriter.Flush();
        }



        private void Laba_2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                reloadGrid_in();
                dataGridView1.ReadOnly = false;
            }
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                dataGridView1.ReadOnly = true;
                var random = new Random(seed);
                int listSize = size;
                for (int i = 0; i < listSize; i++)
                {
                    dataGridView1.Rows[0].Cells[i].Value = random.Next(-10, 60);
                }
            }
        }


        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                dataGridView1.ReadOnly = true;
                try
                {
                    if (frequency == 0)
                    {
                        throw new Exception(" Frequency isn't entered");
                    }

                    if (frequency > 12)
                    {
                        throw new Exception(" Frequency is too large");
                    }
                }
                catch (Exception ex)
                {
                    WriteExeption(ex, DateTime.Now.ToString());
                    return;
                }
                var random = new Random(seed);
                int listSize = size;
                for (int i = 0; i < frequency; i++)
                {
                    dataGridView1.Rows[0].Cells[i].Value = random.Next(-10, 60);
                }
                for (int i = frequency; i < listSize; i++)
                {
                    dataGridView1.Rows[0].Cells[i].Value = (dataGridView1.Rows[0].Cells[i % frequency].Value);
                }
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int[] temp = new int[12];
                if (radioButton1.Checked == true)
                {
                    for (int i = 0; i < size; i++)
                    {
                        if (dataGridView1.Rows[0].Cells[i].Value == null)
                        {
                            throw new Exception("Array empty");
                        }
                        temp[i] = int.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString());
                        if (temp[i] < -10 || temp[i] > 60)
                        {
                            throw new Exception("Out of  needed range");
                        }
                    }
                }

                if (textBox1.Text == "")
                {
                    throw new Exception("k is not entered");
                }
            }
            catch (Exception ex)
            {
                WriteExeption(ex, DateTime.Now.ToString());
                return;
            }


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task_direct();
            stopwatch.Stop();
            label9.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
            stopwatch.Restart();
            Task_parallel();
            stopwatch.Stop();
            label8.Text = stopwatch.ElapsedMilliseconds.ToString() + " milliseconds";
        }

        private void Task_direct()
        {
            try
            {
                k = int.Parse(textBox1.Text);
            }
            catch (Exception e)
            {
                WriteExeption(e, DateTime.Now.ToString());
                return;
            }

            
            int temp = 0;
            for (int i = 0; i < size; i++)
            {
                temp = int.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString());
                if ((temp % 10) != 0)
                {
                    if ((((temp % 10) % 2 == 0)
                        && ((temp / (temp % 10)) == 0)))
                    {
                        dataGridView2.Rows[0].Cells[i].Value = null;
                        break;
                    }
                }

                dataGridView2.Rows[0].Cells[i].Value = dataGridView1.Rows[0].Cells[i].Value;
            }
            for (int i = 0; i < size; i++)
            {
                if ((temp % 10) == k)
                {
                    if (i == 0)
                    {
                        dataGridView2.Rows[0].Cells[i + 1].Value = k;
                    }
                    else if (i == size - 1)
                    {
                        dataGridView2.Rows[0].Cells[i - 1].Value = k;
                    }
                    else
                    {
                        dataGridView2.Rows[0].Cells[i - 1].Value = k;
                        dataGridView2.Rows[0].Cells[i + 1].Value = k;
                    }
                }
            }

            for (int i = 1; i < size; i += 2)
            {
                temp = int.Parse(dataGridView2.Rows[0].Cells[size - 1].Value.ToString());
                for (int k = size - 2; k >= i; k--)
                    dataGridView2.Rows[0].Cells[k + 1].Value = dataGridView2.Rows[0].Cells[k].Value;
                dataGridView2.Rows[0].Cells[i].Value = temp;
            }
        }

        private void Task_parallel()
        {
            try
            {
            }
            catch (Exception e)
            {
                WriteExeption(e, DateTime.Now.ToString());
                return;
            }

            Thread threadFirst = new Thread(deleteTask);
            threadFirst.Start();
            while (threadFirst.IsAlive)
            {
            }

            Thread threadSecond = new Thread(replacingTask);
            threadSecond.Start();
            while (threadSecond.IsAlive)
            {
            }

            Thread threadThird = new Thread(repositioningTask);
            threadThird.Start();
            while (threadThird.IsAlive)
            {
            }

        }

        private void deleteTask()
        {
            int temp = 0;
            for (int i = 0; i < size; i++)
            {
                temp = int.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString());
                if ((temp % 10) != 0)
                {
                    if ((((temp % 10) % 2 == 0)
                        && ((temp / (temp % 10)) == 0)))
                    {
                        dataGridView2.Rows[0].Cells[i].Value = null;
                        break;
                    }
                }

                dataGridView2.Rows[0].Cells[i].Value = dataGridView1.Rows[0].Cells[i].Value;
            }
        }

        private void replacingTask()
        {
            int temp = 0;
            for (int i = 0; i < size; i++)
            {
                temp = int.Parse(dataGridView1.Rows[0].Cells[i].Value.ToString());
                if ((temp % 10) == k)
                {
                    if (i == 0)
                    {
                        dataGridView2.Rows[0].Cells[i + 1].Value = k;
                    }
                    else if (i == size - 1)
                    {
                        dataGridView2.Rows[0].Cells[i - 1].Value = k;
                    }
                    else
                    {
                        dataGridView2.Rows[0].Cells[i - 1].Value = k;
                        dataGridView2.Rows[0].Cells[i + 1].Value = k;
                    }
                }
            }
        }

        private void repositioningTask()
        {
            int temp = 0;

            for (int i = 1; i < size; i += 2)
            {
                temp = int.Parse(dataGridView2.Rows[0].Cells[size - 1].Value.ToString());
                for (int k = size - 2; k >= i; k--)
                    dataGridView2.Rows[0].Cells[k + 1].Value = dataGridView2.Rows[0].Cells[k].Value;
                dataGridView2.Rows[0].Cells[i].Value = temp;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                k = int.Parse(textBox1.Text);
                if (k <= 0)
                {
                    throw new Exception("Incorrect array size");
                }
            }
            catch (Exception ex)
            {
                WriteExeption(ex, DateTime.Now.ToString());
                return;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                frequency = int.Parse(textBox2.Text);
                if (frequency <= 0)
                {
                    throw new Exception(" Incorrect frequency");
                }
            }
            catch (Exception ex)
            {
                WriteExeption(ex, DateTime.Now.ToString());
                return;
            }
            if (frequency < 0)
            {
                frequency = 0;
            }

        }
    }

}
