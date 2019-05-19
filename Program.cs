using System;
using System.Collections.Generic;
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
            }
           );

            //GameOverMessage(score, screenWidth, screenHeight);
        }

        #region Public Classes 
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

        public class Snake : Pixel
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
        }

        public enum MoveTo
        {
            Right,
            Left,
            Up,
            Down
        }

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

        private static void ClearConsole(int screenWidth, int screenHeight)
        {
            var blackLine = string.Join("", new byte[screenWidth - 2].Select(b => " ").ToArray());
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 1; i < screenHeight - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackLine);
            }
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

        private static void DrawBorder(int screenWidth, int screenHeight)
        {
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
        public class Settings
        {
            public Screen Screen { get; set; }
            public MoveTo InitialMove { get; set; }
            public int KeyCooldown { get; set; }
            public ConsoleColor SnakeBodyColor { get; set; }
            public ConsoleColor FoodColor { get; set; }
        }

        /// <summary>
        /// Status, va a contener los datos del juego mientras el mismo este en ejecucion
        /// lo hago singleton para asegurarme de manejar el mismo estado en todo el tiempo de vida de una "session" de juego
        /// </summary>
        class Status
        {
            public bool Continue { get; private set; }
            public int Score { get; private set; }
            public MoveTo CurrentMove { get; private set; }
            public DateTime BeginTime { get; private set; }
            public DateTime EndTime { get; private set; }

            public void UpdateMove(MoveTo nextMove) => this.CurrentMove = nextMove;
            public void BeginCoolDown() => this.BeginTime = DateTime.Now;
            public void Tick() => this.EndTime = DateTime.Now;

            private static Status status;

            protected Status()
            {
                //estado inicial
                this.Continue = true;
                this.CurrentMove = MoveTo.Right;
                this.Score = 5;
                this.EndTime = DateTime.Now;
            }

            public static Status GetStatus()
            {
                if (status == null)
                    status = new Status();

                return status;
            }

            public void End()
            {
                this.Continue = false;
            }

            public void AddScore(int points = 1) =>
                this.Score += points;
        }

        public class Food : Pixel
        {
            public Food(
                Screen screen,
                ConsoleColor colorPixel)
            : base(
                RandomNumber(0, screen.Width),
                RandomNumber(0, screen.Height),
                colorPixel) =>
            this.screen = screen;

            public void Respaw()
            {
                this.xPos = RandomNumber(1, screen.Width - 2);
                this.yPos = RandomNumber(1, screen.Height - 2);
            }
            public void Appear() => base.Draw();
            readonly Screen screen;
        }

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