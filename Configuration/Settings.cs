using snake_game.IO;
using System;

namespace snake_game.Configuration
{
    /// <summary>
    /// Agrupo toda la configuracion del juego
    /// </summary>
    public class Settings
    {
        public Screen Screen { get; set; }
        /// <summary>
        /// Tiempo en MS para leer una techa
        /// </summary>
        public int KeyCooldown { get; set; }
        public ConsoleColor SnakeBodyColor { get; set; }
        public ConsoleColor FoodColor { get; set; }
        public ConsoleColor ScreenBorderColor { get; set; }
        /// <summary>
        /// caracter usado para dibujar los bordes de la pantalla
        /// </summary>
        public string ScreenBorderChar { get; set; }
    }
}
