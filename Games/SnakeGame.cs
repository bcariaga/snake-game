using snake_game.Configuration;
using snake_game.GameObjects;
using snake_game.IO;
using snake_game.Objects;
using System;
using System.Linq;

namespace snake_game.Games
{
    //Façade
    public class SnakeGame : IGame
    {
        /*game management*/
        Settings settings;
        Joystick joystick;
        ScreenManager screenManager;
        Status status = Status.GetStatus();
        bool isReady = false;

        /*UI*/
        Snake snake;
        Food food;

        public SnakeGame() => Init();

        private void Init()
        {
            (settings, joystick, screenManager) = GetConfiguration();
            (snake, food) = GetUIElements(settings);
        }

        public void SetUp()
        {
            Console.CursorVisible = false;
            screenManager.DrawBorder();
            isReady = true;
        }

        public void Play(Action<Status, ScreenManager> onEndGame)
        {
            if (!isReady)
                throw new ApplicationException("The game isn't ready!");

            while (status.Continue)
            {
                screenManager.ClearConsole();


                status.Continue = SnakeLive();

                AppearElements(snake, food);
                ReadKey();
            }

            onEndGame(status, screenManager);
        }

        #region private
        private (Settings settings, Joystick joystick, ScreenManager screenManager) GetConfiguration()
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
        private (Snake snake, Food food) GetUIElements(Settings settings) => (
                new Snake(
                   settings.Screen.Width / 2,
                   settings.Screen.Height / 2,
                   settings.SnakeBodyColor),
                new Food(settings.Screen, settings.FoodColor));
        private void AppearElements(params IShowable[] elements )
        {
            foreach (var element in elements)
                element.Appear();
        }
        private void ReadKey()
        {
            //TODO: ¿esto no es responsabilidad del joystick?
            status.BeginCoolDown();
            while (joystick.KeyCoolDown(status.EndTime, status.BeginTime, settings.KeyCooldown))
            {
                status.Tick();
                status.UpdateMove(joystick.ReadMovement(status.CurrentMove));
            }


        }
        private bool SnakeLive()
        {
            //TODO: ¿esto no es responsabilidad de la clase Snake?
            snake.TryEat(food, () => {
                status.AddScore();
                food.Respaw();
            });
            snake.Grow();
            snake.Move(status.CurrentMove);

            if (snake.xTale.Count() > status.Score)
                snake.Reduce();

            return !snake.DidCrash(settings.Screen) && !snake.IsCanibal;

        }
        #endregion
    }
}
