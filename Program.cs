namespace snake_game
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new SnakeGame();

            game.SetUp();

            game.Play(onEndGame: (status, screen) => {
                screen.Print($"Game Over, points:{status.Score}");
            });

        }
    }
}