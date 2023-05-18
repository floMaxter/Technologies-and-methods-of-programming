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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void lab1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laba_2 form_2 = new Laba_2(this);
            form_2.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.AutoSize = false;
            textBox1.Size = new System.Drawing.Size(520, 250);
        }

        private void lab2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laba2dop form_2 = new Laba2dop(this);
            form_2.ShowDialog();
        }

        private void laba3BASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laba_3 form_3 = new Laba_3(this);
            form_3.ShowDialog();
        }

        private void laba3FuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Laba3Fu form_3f = new Laba3Fu(this);
            form_3f.ShowDialog();
        }
    }
}
