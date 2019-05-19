using System;

namespace snake_game
{
    public class Settings
    {
        public Screen Screen { get; set; }
        public int KeyCooldown { get; set; }
        public ConsoleColor SnakeBodyColor { get; set; }
        public ConsoleColor FoodColor { get; set; }
        public ConsoleColor ScreenBorderColor { get; set; }
        public string ScreenBorderChar { get; set; }
    }
}
