namespace snake_game.IO
{
    /// <summary>
    /// Representacion del canvas donde se va a desarrollar el juego
    /// </summary>
    public class Screen
    {
        public Screen(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
    }
}
