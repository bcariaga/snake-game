using System;
using System.Linq;

namespace snake_game
{
    class Program
    {
        static void Main(string[] args)
        {
            Play(new Settings()
            {
                Screen = new Screen(32, 16),
                FoodColor = ConsoleColor.Cyan,
                SnakeBodyColor = ConsoleColor.Red,
                KeyCooldown = 500
            });

            //GameOverMessage(score, screenWidth, screenHeight);
        }

        #region Public Classes 
      
        public static int RandomNumber(int min, int max)
        {
            Random randomNummer = new Random();
            return (randomNummer.Next(min, max));
        }

        public static MoveTo ReadMovement(MoveTo currentMove)
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

        #endregion


        #region Private Classes  
        private static void DrawPixels(int x, int y, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write("■");

        }
        private static void GameOverMessage(int score, int screenWidth, int screenHeight)
        {
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);

        }
        //TODO: Refactor
        private static void ClearConsole(Screen screen)
        {
            var screenWidth = screen.Width;
            var screenHeight = screen.Height;
            var blackLine = string.Join("", new byte[screenWidth - 2].Select(b => " ").ToArray());
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < screenHeight - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackLine);
            }
        }

        //TODO: refactor
        private static void DrawBorder(Screen screen)
        {
            var screenWidth = screen.Width;
            var screenHeight = screen.Height;

            Console.ForegroundColor = ConsoleColor.Green;

            var horizontalBar = string.Join("", new byte[screenWidth].Select(b => "■").ToArray());

            Console.SetCursorPosition(0, 0);
            Console.Write(horizontalBar);
            Console.SetCursorPosition(0, screenHeight - 1);
            Console.Write(horizontalBar);

            for (int i = 0; i < screenHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(screenWidth - 1, i);
                Console.Write("■");
            }
        }

        #endregion 

        #region new stuff

        public static class Helper
        {
            public static bool DidCrash(Snake snake, Screen screen) =>
                (snake.xPos == screen.Width - 1 || snake.xPos == 0 || snake.yPos == screen.Height - 1 || snake.yPos == 0);
            public static bool DidEat(Snake snake, Food food) =>
                (food.xPos == snake.xPos && food.yPos == snake.yPos);
            public static bool KeyCoolDown(DateTime endTime, DateTime beginTime, int keyCooldownMs) =>
                endTime.Subtract(beginTime).TotalMilliseconds < keyCooldownMs;
        }
     
        private static void Play(Settings settings)
        {
            //on the middle of the screen
            var snake = new Snake(
                settings.Screen.Width / 2,
                settings.Screen.Height / 2,
                settings.SnakeBodyColor);

            var food = new Food(settings.Screen, settings.FoodColor);
            DrawBorder(settings.Screen);
            var status = Status.GetStatus();
            while (status.Continue)
            {
                ClearConsole(settings.Screen);

                Console.ForegroundColor = ConsoleColor.Green;

                if (Helper.DidCrash(snake, settings.Screen))
                    status.End();

                if (Helper.DidEat(snake, food))
                {
                    status.AddScore();
                    food.Respaw();
                }

                snake.Appear();

                if (snake.IsCanibal)
                    status.End();

                food.Appear();

                Console.CursorVisible = false;

                
                status.BeginCoolDown();
                while (Helper.KeyCoolDown(status.EndTime, status.BeginTime, settings.KeyCooldown))
                {
                    status.Tick();
                    status.UpdateMove(ReadMovement(status.CurrentMove));
                }


                snake.Grow();

                snake.Move(status.CurrentMove);

                if (snake.xTale.Count() > status.Score)
                    snake.Reduce();
            }
        }
        #endregion
    }
}