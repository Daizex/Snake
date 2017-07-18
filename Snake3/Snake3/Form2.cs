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
        // for job with coordinates
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
        // timers initialization
        Timer timer = new Timer();
        Timer timer2 = new Timer();
        Random rand = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        // width and height of the field in cells, cell size in pixels
        int M = 60, N = 40, С = 10;
        // list of segments of the snake (zero index in the list - the head of the snake)
        List<Coord> snake = new List<Coord>();
        Coord simpleFruit; // сoordinates of fruit
        Coord badFruit; // coordinates of the fruit, reducing the snake by 1
        // "game over" fruit
        Coord goFruit1;
        Coord goFruit2;
        Coord goFruit3;
        int way; // direction of movement of the snake
        int score = 0; // points scored in the game
        public Form2(Form1 f)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; // inaccessibility of all the window controls
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption; // background color
            this.StartPosition = FormStartPosition.CenterScreen; // the shape is displayed in the center of the screen
            this.DoubleBuffered = true; // for drawing, so as not to blink
            this.Size = new Size(M * С, N * С); //form size
            this.Paint += new PaintEventHandler(Rendering); // binding the handler drawing of form
            this.KeyDown += new KeyEventHandler(KeyD); // binding the handler button presses
            timer.Interval = 200; // timer in 200 milliseconds
            timer.Tick += new EventHandler(Timer); // binding timer handler
            timer.Start(); // start the timer
            timer2.Interval = 10000; // timer in 10000 milliseconds for "game over" fruit
            timer2.Tick += new EventHandler(randGoFruit); // binding timer handler
            timer2.Start(); // start the timer
            // random snake of 3 segments
            int wayX = rand.Next(M);
            int wayY = rand.Next(N);
            snake.Add(new Coord(wayX, wayY)); // head
            way = rand.Next(0, 3); // random direction of the snake when it appears (0 - up, 1 - right, 2 - down, 3 - left)
            // tail, depending on the outward direction
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
            simpleFruit = new Coord(rand.Next(M), rand.Next(N)); // coordinates of a simple fruit
            badFruit = new Coord(rand.Next(M), rand.Next(N)); // coordinates of the fruit reducing the snake by 1
            // coordinates of the "game over" fruit
            goFruit1 = new Coord(rand.Next(M), rand.Next(N));
            goFruit2 = new Coord(rand.Next(M), rand.Next(N));
            goFruit3 = new Coord(rand.Next(M), rand.Next(N));
        }
        // processing keydown
        void KeyD(object sender, KeyEventArgs e)
        {
            // the direction of movement changes if the arrow is not the opposite
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
                case Keys.Escape: // pause
                    timer.Stop();
                    DialogResult pause = MessageBox.Show("Continue?", "Pause", MessageBoxButtons.YesNo);
                    if (pause == System.Windows.Forms.DialogResult.Yes)
                        timer.Start();
                    if (pause == System.Windows.Forms.DialogResult.No)
                        Close();
                    break;
            }
        }
        // random "game over" fruit
        void randGoFruit(object sender, EventArgs e)
        {
            goFruit1 = new Coord(rand.Next(M), rand.Next(N));
            goFruit2 = new Coord(rand.Next(M), rand.Next(N));
            goFruit3 = new Coord(rand.Next(M), rand.Next(N));
        }
        void Timer(object sender, EventArgs e)
        {
            // storing coordinates snake head
            int x = snake[0].X, y = snake[0].Y;
            // depending on the direction, it is calculated where the head will be in the next step
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
            Coord newhead = new Coord(x, y); // segment with new coordinates of the head
            snake.Insert(0, newhead); // insert it at the top of the list of segments of the snake (the snake grew by one segment)
            // if the coordinates of the head and simple fruit coincide
            if (snake[0].X == simpleFruit.X && snake[0].Y == simpleFruit.Y)
            {
                // a random new fruit
                simpleFruit = new Coord(rand.Next(M), rand.Next(N));
                badFruit = new Coord(rand.Next(M), rand.Next(N));
                score++; // increment of points
                // snake acceleration
                if (timer.Interval <= 10)
                    timer.Interval = 10;
                else
                    timer.Interval -= 10;
            }
            else // that the snake does not grow constantly, when does not eat the fruit
                snake.RemoveAt(snake.Count - 1);
            // if the coordinates of the head and the fruit reducing the snake by 1 coincide
            if (snake[0].X == badFruit.X && snake[0].Y == badFruit.Y)
            {
                snake.Remove(newhead); // removing an element from the snake's list
                // a random new fruit
                simpleFruit = new Coord(rand.Next(M), rand.Next(N));
                badFruit = new Coord(rand.Next(M), rand.Next(N));
                // decrement of points
                if (score > 0)
                    score--;
                else
                {
                    snake.Insert(0, newhead);
                    score = 0;
                }
                timer.Interval += 10; // snake deceleration
            }
            // game over
            if ((snake[0].X == goFruit1.X && snake[0].Y == goFruit1.Y) ||
                (snake[0].X == goFruit2.X && snake[0].Y == goFruit2.Y) ||
                (snake[0].X == goFruit3.X && snake[0].Y == goFruit3.Y))
            {
                timer.Stop();
                DialogResult result = MessageBox.Show("Score: " + score + "\nRepeat?", "Game Over", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    simpleFruit = new Coord(rand.Next(M), rand.Next(N)); // coordinates of a simple fruit
                    badFruit = new Coord(rand.Next(M), rand.Next(N)); // coordinates of the fruit reducing the snake by 1
                    // coordinates of the "game over" fruit
                    goFruit1 = new Coord(rand.Next(M), rand.Next(N));
                    goFruit2 = new Coord(rand.Next(M), rand.Next(N));
                    goFruit3 = new Coord(rand.Next(M), rand.Next(N));
                    timer.Start();
                }
                if (result == System.Windows.Forms.DialogResult.No)
                    this.Close();
            }
            Invalidate(); // rendering, calling Rendering
        }
        // rendering
        void Rendering(object sender, PaintEventArgs e)
        {
            // simple fruit
            e.Graphics.FillEllipse(Brushes.Green, new Rectangle(simpleFruit.X * С, simpleFruit.Y * С, С, С));
            // a fruit that reduces the snake by 1
            e.Graphics.FillEllipse(Brushes.Orange, new Rectangle(badFruit.X * С, badFruit.Y * С, С, С));
            // "game over" fruit
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(goFruit1.X * С, goFruit1.Y * С, С, С));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(goFruit2.X * С, goFruit2.Y * С, С, С));
            e.Graphics.FillEllipse(Brushes.Red, new Rectangle(goFruit3.X * С, goFruit3.Y * С, С, С));
            // head of snake
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(snake[0].X * С, snake[0].Y * С, С, С));
            // the body of the snake
            for (int i = 1; i < snake.Count; i++)
                e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(snake[i].X * С, snake[i].Y * С, С, С));
            string state = "Score: " + score.ToString() + "\n''Escape'' - pause"; // points
            e.Graphics.DrawString(state, new Font("Arial", 10), Brushes.Black, new Point(5, 5)); // drawing points
        }
    }
}