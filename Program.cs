using snake_game.Games;

namespace snake_game
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new SnakeGame();

            game.SetUp();

            game.Play(onEndGame: (status, screen) => {
                screen.Print($"Game Over, points:{status.Score}");
            });

        }
    }
}