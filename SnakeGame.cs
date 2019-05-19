using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace snake_game
{
    public class SnakeGame
    {
        /*game management*/
        Settings settings;
        Joystick controller;
        ScreenManager screenManager;
        Status status = Status.GetStatus();

        /*UI*/
        Snake snake;
        Food food;

        public SnakeGame() => Init();

        private void Init()
        {
            (settings, controller, screenManager) = GetConfiguration();
            (snake, food) = GetUIElements(settings);
        }

        public void SetUp()
        {
            Console.CursorVisible = false;
            screenManager.DrawBorder();
        }

        public void Play(Action<Status, ScreenManager> onEndGame)
        {

            while (status.Continue)
            {
                screenManager.ClearConsole();


                status.Continue = !snake.DidCrash(settings.Screen) && !snake.IsCanibal;

                snake.TryEat(food, () => {
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

            onEndGame(status, screenManager);
        }

        #region private
        private static (Settings settings, Joystick controller, ScreenManager screenManager) GetConfiguration()
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

            var controller = new Joystick();

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
        #endregion
    }
}
