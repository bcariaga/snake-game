namespace snake_game
{
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
