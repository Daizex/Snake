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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button1.Location = new System.Drawing.Point(225, 175);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button2.Location = new System.Drawing.Point(225, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Exit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            this.label1.Font = new System.Drawing.Font("Old English Text MT", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(170, 40);
            this.label1.Name = "label1";
            this.label1.TabIndex = 2;
            this.label1.Text = "Snake game by Daizex";

            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            //this.Text = "Snake game by Daizex"; // заголовок формы
            //this.FormBorderStyle = FormBorderStyle.FixedDialog; // запрет растягивания формы
            //this.MaximizeBox = false; // недоступность кнопки "развернуть во весь экран"
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // недоступность всех элементов управления окном
            this.StartPosition = FormStartPosition.CenterScreen; // форма отображается по центру экрана
            int caption_size = SystemInformation.CaptionHeight; // высота шапки формы
            int frame_size = SystemInformation.FrameBorderSize.Height; // ширина границы формы
            // установка размера внутренней области формы W * H с учетом высоты шапки и ширины границ
            this.Size = new Size(M * С + frame_size, N * С + caption_size + frame_size);
            this.ResumeLayout(false);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 SnakeGame = new Form2(this);
            SnakeGame.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
