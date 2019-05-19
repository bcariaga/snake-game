using System;
using snake_game.GameObjects;
using snake_game.Helpers;
using snake_game.IO;

namespace snake_game.Objects
{
    /// <summary>
    /// sabe como administrar el elemento food en el canvas del juego
    /// </summary>
    public class Food : Pixel, IShowable
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
