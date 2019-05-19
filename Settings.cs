using System;

namespace snake_game
{
    public class Settings
    {
        public Screen Screen { get; set; }
        public MoveTo InitialMove { get; set; }
        public int KeyCooldown { get; set; }
        public ConsoleColor SnakeBodyColor { get; set; }
        public ConsoleColor FoodColor { get; set; }
    }
}
