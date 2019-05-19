using System;

namespace snake_game.Objects
{
    /// <summary>
    /// Template Method, para los elementos de "UI" que necesitan moverse en la pantalla y dibujarse
    /// </summary>
    public abstract class Pixel
    {
        public Pixel(int xPosition, int yPosition, ConsoleColor colorPixel)
        {
            xPos = xPosition;
            yPos = yPosition;
            color = colorPixel;
        }
        public int xPos { get; set; }
        public int yPos { get; set; }
        public ConsoleColor color { get; set; }

        protected virtual void Draw(int? x = null, int? y = null, string chars = "■")
        {
            Console.SetCursorPosition(x ?? xPos, y ?? yPos);
            Console.ForegroundColor = color;
            Console.Write(chars);
        }
    }
}
