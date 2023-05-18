using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TP_All_Labs
{
    public partial class _3labFurieSettings : Form
    {
        Form3 parentFrame;

        public _3labFurieSettings(Form3 f)
        {
            InitializeComponent();
            parentFrame = f;
        }

        private void button_Enter_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
