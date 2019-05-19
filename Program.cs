using System;
using System.Linq;

namespace snake_game
{
    class Program
    {
        /*game management*/
        static Settings settings;
        static Controller controller;
        static ScreenManager screenManager;
        static Status status = Status.GetStatus();

        /*UI*/
        static Snake snake;
        static Food food;

        static void Main(string[] args)
        {
            /*Init*/
            (settings, controller, screenManager) = GetConfiguration();
            (snake, food) = GetUIElements(settings);
            SetUp();

            /*let's play a game*/
            Play();

            /*end*/
            controller.GameOverMessage(status.Score, settings.Screen);
        }

        #region Init
        private static (Settings settings, Controller controller, ScreenManager screenManager) GetConfiguration()
        {

            var settings = new Settings()
            {
                Screen = new Screen(32, 16),
                FoodColor = ConsoleColor.Cyan,
                SnakeBodyColor = ConsoleColor.Red,
                KeyCooldown = 500,
                ScreenBorderChar = "■",
                ScreenBorderColor = ConsoleColor.Green

            };

            var controller = new Controller();

            var screenManager = new ScreenManager(
                settings.Screen,
                settings.ScreenBorderColor,
                settings.ScreenBorderChar);


            return (settings, controller, screenManager);
        }
        private static (Snake snake, Food food) GetUIElements(Settings settings) => (
                new Snake(
                   settings.Screen.Width / 2,
                   settings.Screen.Height / 2,
                   settings.SnakeBodyColor),
                new Food(settings.Screen, settings.FoodColor));
        private static void SetUp()
        {
            Console.CursorVisible = false;
            screenManager.DrawBorder();
        } 
        #endregion

       
        private static void Play()
        {
           
            while (status.Continue)
            {
                screenManager.ClearConsole();
                

                status.Continue = !snake.DidCrash(settings.Screen) && !snake.IsCanibal;

                snake.TryEat(food, () =>{
                    status.AddScore();
                    food.Respaw();
                });

                snake.Appear();

                food.Appear();


                status.BeginCoolDown();
                while (controller.KeyCoolDown(status.EndTime, status.BeginTime, settings.KeyCooldown))
                {
                    status.Tick();
                    status.UpdateMove(controller.ReadMovement(status.CurrentMove));
                }

                snake.Grow();
                snake.Move(status.CurrentMove);

                if (snake.xTale.Count() > status.Score)
                    snake.Reduce();

            }
        }
    }
}