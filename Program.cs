using System;
using System.Collections.Generic;
using System.Linq;

namespace snake_game
{
    class Program
    {
        static void Main(string[] args)
        {
            int screenWidth = 32;

            int screenHeight = 16;

            SnakeBody snake = new SnakeBody(screenWidth / 2, screenHeight / 2, ConsoleColor.Red);

            Pixel food = new Pixel(RandomNumber(0, screenWidth), RandomNumber(0, screenHeight), ConsoleColor.Cyan);

            string currentMove = Movement.right;

            int score = 5;

            bool continueGame = true;

            DateTime beginTime;

            var endTime = DateTime.Now;
            
            DrawBorder(screenWidth, screenHeight);

            while (continueGame)
            {
                ClearConsole(screenWidth, screenHeight);

                Console.ForegroundColor = ConsoleColor.Green;

                if (snake.xPos == screenWidth - 1 || snake.xPos == 0 || snake.yPos == screenHeight - 1 || snake.yPos == 0)
                {
                    continueGame = false;
                }

                if (food.xPos == snake.xPos && food.yPos == snake.yPos)
                {
                    score++;
                    food.xPos = RandomNumber(1, screenWidth - 2);
                    food.yPos = RandomNumber(1, screenHeight - 2);
                }

                for (int i = 0; i < snake.xTale.Count(); i++)
                {
                    Console.SetCursorPosition(snake.xTale[i], snake.yTale[i]);
                    Console.Write("¦");
                    if (snake.xTale[i] == snake.xPos && snake.yTale[i] == snake.yPos)
                    {
                        continueGame = false;
                    }
                }

                DrawPixels(snake.xPos, snake.yPos, snake.color);

                DrawPixels(food.xPos, food.yPos, food.color);

                Console.CursorVisible = false;

                beginTime = DateTime.Now;

                while (endTime.Subtract(beginTime).TotalMilliseconds < 500)
                {
                    endTime = DateTime.Now;
                    currentMove = ReadMovement(currentMove);
                }

                snake.xTale.Add(snake.xPos);
                snake.yTale.Add(snake.yPos);

                switch (currentMove)
                {
                    case Movement.up:
                        snake.yPos--;
                        break;
                    case Movement.down:
                        snake.yPos++;
                        break;
                    case Movement.left:
                        snake.xPos--;
                        break;
                    case Movement.right:
                        snake.xPos++;
                        break;
                }

                if (snake.xTale.Count() > score)
                {
                    snake.xTale.RemoveAt(0);
                    snake.yTale.RemoveAt(0);
                }
            }

            GameOverMessage(score, screenWidth, screenHeight);
        }


        #region Public Classes 
        public class Pixel
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
        }

        public class SnakeBody : Pixel
        {
            public SnakeBody(int xPosition, int yPosition, ConsoleColor colorPixel) : base(xPosition, yPosition, colorPixel)
            {
                xTale = new List<int>();
                yTale = new List<int>();
            }
            public List<int> xTale { get; set; }
            public List<int> yTale { get; set; }
        }

        public class Movement
        {
            public const string right = "RIGHT";
            public const string left = "LEFT";
            public const string up = "UP";
            public const string down = "DOWN";
        }

        public static int RandomNumber(int min, int max)
        {
            Random randomNummer = new Random();
            return (randomNummer.Next(min, max));
        }

        public static string ReadMovement(string currentMove)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo toets = Console.ReadKey(true);
                if (toets.Key.Equals(ConsoleKey.UpArrow) && currentMove != Movement.down )
                {
                    currentMove = Movement.up;
                }
                if (toets.Key.Equals(ConsoleKey.DownArrow) && currentMove != Movement.up)
                {
                    currentMove = Movement.down;
                }
                if (toets.Key.Equals(ConsoleKey.LeftArrow) && currentMove != Movement.right)
                {
                    currentMove = Movement.left;
                }
                if (toets.Key.Equals(ConsoleKey.RightArrow) && currentMove != Movement.left)
                {
                    currentMove = Movement.right;
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

        #endregion 
    }
}
