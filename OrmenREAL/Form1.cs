using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrmenREAL
{
    public partial class Form1 : Form
    {
        const int SIZE_SNAKE = 32;
        const int SIZE_FOOD = 16;
        int Length = 0;
        

        List<PictureBox> tailPieces = new List<PictureBox>();
        Random rng = new Random();
        int i = 0;
        string direction = "right";
        public Form1()
        {
            InitializeComponent();
        }
        bool isIntersectingWithTail()
        {
            for(int i = 0; i < tailPieces.Count; i++)
            {
                if (Snake.Bounds.IntersectsWith(tailPieces[i].Bounds))
                {
                    return true;
                }
            }
            return false;
        }
        void addTail()
        {
            PictureBox box = new PictureBox();
            box.Location = Snake.Location;
            box.Size = Snake.Size;
            box.BackColor = Snake.BackColor;

            Controls.Add(box);

            tailPieces.Add(box); 

        }
        void moveTail()
        {
            if(tailPieces.Count > 0)
            {
                tailPieces[i].Location = Snake.Location;
                i = i + 1;
                if(i >= tailPieces.Count)
                {
                    i = 0;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = Length.ToString();
            ClientSize = new Size(SIZE_SNAKE * 20, SIZE_SNAKE * 15);
            Snake.Size = new Size(SIZE_SNAKE, SIZE_SNAKE);
            Food.Size = new Size(SIZE_FOOD, SIZE_FOOD);

            Food.Location = new Point(rng.Next(0, ClientSize.Width - SIZE_FOOD));
            Snake.Location = new Point(0, 0);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a')
            {
                direction = "left";
            }
            else if (e.KeyChar == 's')
            {
                direction = "down";
            }
            else if (e.KeyChar == 'w')
            {
                direction = "up";
            }
            else if(e.KeyChar == 'd')
            {
                direction = "right";
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            moveTail();
            if(direction == "right")
            {
                Snake.Left += SIZE_SNAKE;
            }
            else if(direction == "left")
            {
                Snake.Left -= SIZE_SNAKE;
            }
            else if(direction == "up")
            {
                Snake.Top -= SIZE_SNAKE;
            }
            else if(direction == "down")
            {
                Snake.Top += SIZE_SNAKE;
            }
            if(Snake.Bounds.IntersectsWith(Food.Bounds))
            {
                Food.Location = new Point(rng.Next(0, ClientSize.Width - SIZE_FOOD), rng.Next(0, ClientSize.Height - SIZE_FOOD));

                Length++;
                addTail();

                label1.Text = "Food" + Length.ToString();
            }
            
        }
    }
}
