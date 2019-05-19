using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace snake_game
{
    public class Snake : Pixel
    {
        public Snake(int xPosition, int yPosition, ConsoleColor colorPixel) : base(xPosition, yPosition, colorPixel)
        {
            xTale = new List<int>();
            yTale = new List<int>();
        }
        public List<int> xTale { get; set; }
        public List<int> yTale { get; set; }
        public bool IsCanibal { get; set; } = false;
        public void Appear()
        {
            for (int i = 0; i < xTale.Count(); i++)
            {
                base.Draw(xTale[i], yTale[i], "¦");
                if (xTale[i] == xPos && yTale[i] == yPos)
                {
                    IsCanibal = true;
                    return;
                }
            }
            base.Draw();//head
        }

        public void Grow()
        {
            xTale.Add(xPos);
            yTale.Add(yPos);
        }

        public void Reduce()
        {
            xTale.RemoveAt(0);
            yTale.RemoveAt(0);
        }


        //TODO: Polymorphism??
        public void Move(MoveTo currentMove)
        {
            switch (currentMove)
            {
                case MoveTo.Right:
                    xPos++;
                    break;
                case MoveTo.Left:
                    xPos--;
                    break;
                case MoveTo.Up:
                    yPos--;
                    break;
                case MoveTo.Down:
                    yPos++;
                    break;
            }
        }
    }
}
