using System;


namespace snake_game.IO
{
    /// <summary>
    /// Emula un joystick, leyendo las teclas ingresadas y manejando el cooldown de las mismas
    /// </summary>
    public class Joystick
    {
        public bool KeyCoolDown(DateTime endTime, DateTime beginTime, int keyCooldownMs) =>
               endTime.Subtract(beginTime).TotalMilliseconds < keyCooldownMs;

        //TODO: Mepa que viene bien un observer
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
