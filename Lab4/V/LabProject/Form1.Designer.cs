
namespace LabProject
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lab2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laba3BASEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.laba3BASEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.laba3FuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(547, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tPToolStripMenuItem
            // 
            this.tPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lab1ToolStripMenuItem,
            this.lab2ToolStripMenuItem,
            this.laba3BASEToolStripMenuItem});
            this.tPToolStripMenuItem.Name = "tPToolStripMenuItem";
            this.tPToolStripMenuItem.Size = new System.Drawing.Size(32, 20);
            this.tPToolStripMenuItem.Text = "TP";
            // 
            // lab1ToolStripMenuItem
            // 
            this.lab1ToolStripMenuItem.Name = "lab1ToolStripMenuItem";
            this.lab1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lab1ToolStripMenuItem.Text = "Lab 2 BASE";
            this.lab1ToolStripMenuItem.Click += new System.EventHandler(this.lab1ToolStripMenuItem_Click);
            // 
            // lab2ToolStripMenuItem
            // 
            this.lab2ToolStripMenuItem.Name = "lab2ToolStripMenuItem";
            this.lab2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lab2ToolStripMenuItem.Text = "Lab 2 DOP";
            this.lab2ToolStripMenuItem.Click += new System.EventHandler(this.lab2ToolStripMenuItem_Click);
            // 
            // laba3BASEToolStripMenuItem
            // 
            this.laba3BASEToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.laba3BASEToolStripMenuItem1,
            this.laba3FuToolStripMenuItem});
            this.laba3BASEToolStripMenuItem.Name = "laba3BASEToolStripMenuItem";
            this.laba3BASEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.laba3BASEToolStripMenuItem.Text = "Laba 3";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // laba3BASEToolStripMenuItem1
            // 
            this.laba3BASEToolStripMenuItem1.Name = "laba3BASEToolStripMenuItem1";
            this.laba3BASEToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.laba3BASEToolStripMenuItem1.Text = "Laba 3 Paint";
            this.laba3BASEToolStripMenuItem1.Click += new System.EventHandler(this.laba3BASEToolStripMenuItem_Click);
            // 
            // laba3FuToolStripMenuItem
            // 
            this.laba3FuToolStripMenuItem.Name = "laba3FuToolStripMenuItem";
            this.laba3FuToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.laba3FuToolStripMenuItem.Text = "Laba 3 Fu";
            this.laba3FuToolStripMenuItem.Click += new System.EventHandler(this.laba3FuToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 289);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lab2ToolStripMenuItem;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem laba3BASEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laba3BASEToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem laba3FuToolStripMenuItem;
    }
}

