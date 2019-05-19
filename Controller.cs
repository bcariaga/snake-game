using System;


namespace snake_game
{
    public class Controller
    {

        //TODO: (S) from SOLID
        public void GameOverMessage(int score, Screen screen)
        {
            Console.SetCursorPosition(screen.Width / 5, screen.Height / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screen.Width / 5, screen.Height / 2 + 1);
            Console.ReadKey();

        }
        public bool KeyCoolDown(DateTime endTime, DateTime beginTime, int keyCooldownMs) =>
               endTime.Subtract(beginTime).TotalMilliseconds < keyCooldownMs;

        public MoveTo ReadMovement(MoveTo currentMove)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo toets = Console.ReadKey(true);
                if (toets.Key.Equals(ConsoleKey.UpArrow) && currentMove != MoveTo.Down)
                {
                    currentMove = MoveTo.Up;
                }
                if (toets.Key.Equals(ConsoleKey.DownArrow) && currentMove != MoveTo.Up)
                {
                    currentMove = MoveTo.Down;
                }
                if (toets.Key.Equals(ConsoleKey.LeftArrow) && currentMove != MoveTo.Right)
                {
                    currentMove = MoveTo.Left;
                }
                if (toets.Key.Equals(ConsoleKey.RightArrow) && currentMove != MoveTo.Left)
                {
                    currentMove = MoveTo.Right;
                }
            }
            return currentMove;
        }

    }
}
