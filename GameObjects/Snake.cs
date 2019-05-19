using snake_game.GameObjects;
using snake_game.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace snake_game.Objects
{
    /// <summary>
    /// Representa al snake en el canvas y maneja su comportamiento
    /// </summary>
    public class Snake : Pixel, IShowable
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

        public bool DidCrash(Screen screen) =>
                (xPos == screen.Width - 1 || xPos == 0 || yPos == screen.Height - 1 || yPos == 0);

        public void TryEat(Food food, Action EatSuccess)
        {
            if(food.xPos == xPos && food.yPos == yPos)
                EatSuccess();

        }
    }
}
