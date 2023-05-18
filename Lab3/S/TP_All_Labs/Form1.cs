using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_All_Labs
{
    public partial class MainFrame : Form
    {
        public MainFrame()
        {
            InitializeComponent();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
        }
        void timer_Tick(object sender, EventArgs e)
        {
            toolStripStatusDataTime.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
                "Вы действительно хотите выйти из программы?",
                "Завершение программы",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (dialog == DialogResult.Yes)
            {
                Close();
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was made by Stepanov Alexander.\nStudent of the 3031 (5011) group");
        }

        private void labToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecondLabForm newForm = new SecondLabForm(this);
            newForm.Show();
        }

        public void setExeptionInLog(string message)
        {   
            this.textBoxLog.Text += message;
            this.textBoxLog.Text += Environment.NewLine;
        }

        private void toolStripCreateBottom_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show(
                "Вы действительно хотите очистить логи?",
                "Внимание",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (dialog == DialogResult.Yes)
            {
                this.textBoxLog.Text = null;
            }
        }

        private void toolStripStatusDataTime_Click(object sender, EventArgs e)
        {

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
                    string text = File.ReadAllText(file);
                    textBoxLog.Text = (text);
                }
                catch (IOException exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string text = textBoxLog.Text;
            SaveFileDialog open = new SaveFileDialog();

            // открываем окно сохранения
            open.ShowDialog();

            // присваниваем строке путь из открытого нами окна
            string path = open.FileName;

            try
            {
                // создаем файл используя конструкцию using

                using (FileStream fs = File.Create(path))
                {
                    // создаем переменную типа массива байтов
                    // и присваиваем ей метод перевода текста в байты
                    byte[] info = new UTF8Encoding(true).GetBytes(text);
                    // производим запись байтов в файл
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

            }
        }

        private void labToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.Show();
        }

        private void labToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form32 form3_2 = new Form32(this);
            form3_2.Show();
        }
    }
}
