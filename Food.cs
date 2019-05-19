using System;

namespace snake_game
{
    public class Food : Pixel
    {
        public Food(
            Screen screen,
            ConsoleColor colorPixel)
        : base(
            Utils.RandomNumber(0, screen.Width - 2),
            Utils.RandomNumber(0, screen.Height - 2),
            colorPixel) =>
        this.screen = screen;

        public void Respaw()
        {
            this.xPos = Utils.RandomNumber(0, screen.Width - 2);
            this.yPos = Utils.RandomNumber(0, screen.Height - 2);
        }
        public void Appear() => base.Draw();
        readonly Screen screen;
    }
}
