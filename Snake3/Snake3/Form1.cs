using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Snake3
{
    public partial class Form1 : Form
    {
        int M = 60, N = 40, С = 10;
        public Form1()
        {
            // form elements
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // "Start" button
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.Location = new System.Drawing.Point(225, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // "Exit" button
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button2.Location = new System.Drawing.Point(225, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // header
            this.label1.Font = new System.Drawing.Font("Old English Text MT", 30F);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 40);
            this.label1.Name = "label1";
            this.label1.TabIndex = 2;
            this.label1.Text = "Snake game";
            // copyright
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(545, 382);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "by Daizex";
            // get collection of controls
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // inaccessibility of all the window controls
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption; // background color
            this.StartPosition = FormStartPosition.CenterScreen; // the shape is displayed in the center of the screen
            this.DoubleBuffered = true; // for drawing, so as not to blink
            this.Size = new Size(M * С, N * С); //form size
            this.ResumeLayout(false);
        }
        // processing of the "Start" button
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 SnakeGame = new Form2(this);
            SnakeGame.Show(this);
        }
        // processing of the "Exit" button
        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
