using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TP_All_Labs
{
    public partial class DialogForSetKIn2Lab : Form
    {
        private string k;
        public DialogForSetKIn2Lab()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            k = textBox1.Text;
            Close();

        }

        public string getK()
        {
            return k;
        }
    }
}
