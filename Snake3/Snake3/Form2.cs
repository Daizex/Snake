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
    public partial class Form2 : Form
    {
        public class Coord
        {
            public int X;
            public int Y;
            public Coord(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
        // инициализация таймера
        Timer timer = new Timer();
        Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        // ширина и высота поля в кетках, размер клетки в пикселах
        int M = 60, N = 40, С = 10;
        // змея: список сегментов (нулевой индекс в списке - голова змеи)  
        List<Coord> snake = new List<Coord>();
        Coord justFruit; // координаты фрукта
        Coord badFruit; // координаты фрукта, уменьшающего змею на 1
        int way; // направление движения змеи
        int score = 0; // набранные очки в игре
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(Form1 f)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // недоступность всех элементов управления окном
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.StartPosition = FormStartPosition.CenterScreen; // форма отображается по центру экрана
            this.DoubleBuffered = true; // для прорисовки, чтобы не мигало
            int caption_size = SystemInformation.CaptionHeight; // высота шапки формы
            int frame_size = SystemInformation.FrameBorderSize.Height; // ширина границы формы
            // установка размера внутренней области формы W * H с учетом высоты шапки и ширины границ
            this.Size = new Size(M * С + frame_size, N * С + caption_size + frame_size);
            this.Paint += new PaintEventHandler(Rendering); // привязка обработчика прорисовки формы
            this.KeyDown += new KeyEventHandler(KeyD); // привязка обработчика нажатий на кнопки
            timer.Interval = 200; // таймер в 200 миллисекунд
            timer.Tick += new EventHandler(Timer); // привязка обработчика таймера
            timer.Start(); // запуск таймера
            // рандом змеи из трёх сегментов
            int wayX = rand.Next(M);
            int wayY = rand.Next(N);
            snake.Add(new Coord(wayX, wayY)); // голова
            way = rand.Next(0, 3); // рандомное направление змеи при появлении (0 - вверх, 1 - вправо, 2 - вниз, 3 - влево)
            // хвост, в зависимости от нарандомленного направления
            switch (way)
            {
                case 0:
                    snake.Add(new Coord(wayX, wayY + 1));
                    snake.Add(new Coord(wayX, wayY + 2));
                    break;
                case 1:
                    snake.Add(new Coord(wayX - 1, wayY));
                    snake.Add(new Coord(wayX - 2, wayY));
                    break;
                case 2:
                    snake.Add(new Coord(wayX, wayY - 1));
                    snake.Add(new Coord(wayX, wayY - 2));
                    break;
                case 3:
                    snake.Add(new Coord(wayX + 1, wayY));
                    snake.Add(new Coord(wayX + 2, wayY));
                    break;
            }
            justFruit = new Coord(rand.Next(M), rand.Next(N)); // координаты простого фрукта
            badFruit = new Coord(rand.Next(M), rand.Next(N)); // координаты фрукта, уменьшающего мею на 1
        }
        // обработка нажатий на клавиши
        void KeyD(object sender, KeyEventArgs e)
        {
            // стрелками меняется направление движения, если оно не противоположное
            switch (e.KeyData)
            {
                case Keys.Up:
                    if (way != 2)
                        way = 0;
                    break;
                case Keys.Right:
                    if (way != 3)
                        way = 1;
                    break;
                case Keys.Down:
                    if (way != 0)
                        way = 2;
                    break;
                case Keys.Left:
                    if (way != 1)
                        way = 3;
                    break;
                case Keys.Escape: // пауза
                    timer.Stop();
                    DialogResult pause = MessageBox.Show("Continue?", "Pause", MessageBoxButtons.YesNo);
                    if (pause == System.Windows.Forms.DialogResult.Yes)
                        timer.Start();
                    if (pause == System.Windows.Forms.DialogResult.No)
                        Close();
                    break;
            }
        }

        void Timer(object sender, EventArgs e)
        {
            // запоминание координат головы змеи
            int x = snake[0].X, y = snake[0].Y;
            // в зависимости от направления, вычисляется, где будет голова на следующем шаге
            switch (way)
            {
                case 0:
                    y--;
                    if (y < 0)
                        y = N - 1;
                    break;
                case 1:
                    x++;
                    if (x >= M)
                        x = 0;
                    break;
                case 2:
                    y++;
                    if (y >= N)
                        y = 0;
                    break;
                case 3:
                    x--;
                    if (x < 0)
                        x = M - 1;
                    break;
            }
            Coord newhead = new Coord(x, y); // сегмент с новыми координатами головы
            snake.Insert(0, newhead); // вставляем его в начало списка сегментов змеи(змея выросла на один сегмент)
            if (snake[0].X == justFruit.X && snake[0].Y == justFruit.Y) // если координаты головы и простого фрукта совпали
            {
                justFruit = new Coord(rand.Next(M), rand.Next(N)); // рандомится новый простой фрукт
                score++; // увеличение набранных очков в игре
            }
            else // чтобы змея не росла постонно, когда не ест фрукт
                snake.RemoveAt(snake.Count - 1);
            if (snake[0].X == badFruit.X && snake[0].Y == badFruit.Y) // если координаты головы и фрукта, уменьшеающего змею на 1, совпали
            {
                snake.Remove(newhead);
                badFruit = new Coord(rand.Next(M), rand.Next(N)); // рандомится новый фрукт
                if (score > 0)
                    score--; // уменьшение набранных очков в игре
                else
                {
                    snake.Insert(0, newhead);
                    score = 0;
                }
            }


            // проигрыш при столкновении змеи с собой
            // не работает
            int count = 0; // количество сегментов, равных голове
            foreach (Coord t in snake)
            {
                if (t == snake[0])
                    count++;
            }
            if (snake.Count > 1 && justFruit != snake[0] && snake[0].X == snake[count].X && snake[0].Y == snake[count].Y)
            {
                MessageBox.Show("Game over");
                this.Close();
            }


            Invalidate(); // перерисовка, вызов Rendering
        }

        // отрисовка
        void Rendering(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Green, new Rectangle(justFruit.X * С, justFruit.Y * С, С, С)); // простой фрукт
            e.Graphics.FillEllipse(Brushes.Orange, new Rectangle(badFruit.X * С, badFruit.Y * С, С, С)); // фрукт, уменьшающий змею на 1
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(snake[0].X * С, snake[0].Y * С, С, С)); // голова змеи
            for (int i = 1; i < snake.Count; i++)
                e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(snake[i].X * С, snake[i].Y * С, С, С)); // тело змеи
            string state = "Score: " + score.ToString() + "\n''Escape'' - pause"; // количество очков
            e.Graphics.DrawString(state, new Font("Arial", 10, FontStyle.Bold), Brushes.Black, new Point(5, 5)); // вывод очков
        }
    }
}
