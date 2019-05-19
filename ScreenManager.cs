using System;
using System.Linq;

namespace snake_game
{
    public class ScreenManager
    {
        private Screen Screen { get; set; }
        private ConsoleColor borderColor;
        private string borderChars { get; set; }
        private string blackline => string.Join("", new byte[Screen.Width-2].Select(b => " ").ToArray());
        private string horizontalBar => string.Join("", new byte[Screen.Width].Select(b => borderChars).ToArray());

        public ScreenManager(Screen screen, ConsoleColor borderColor, string borderChars)
        {
            this.Screen = screen;
            this.borderColor = borderColor;
            this.borderChars = borderChars;
        }

        public void DrawBorder()
        {
            Console.ForegroundColor = borderColor;

            Console.SetCursorPosition(0, 0);
            Console.Write(horizontalBar);
            Console.SetCursorPosition(0, Screen.Height - 1);
            Console.Write(horizontalBar);

            for (int i = 0; i < Screen.Height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(borderChars);
                Console.SetCursorPosition(Screen.Width - 1, i);
                Console.Write(borderChars);
            }
        }
        public void ClearConsole()
        {
            Console.ForegroundColor = ConsoleColor.Black; //black is blank?
            for (int i = 1; i < Screen.Height - 1; i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write(blackline);
            }
        }

        public void Print(string msg, bool waitReadKey = true)
        {
            Console.SetCursorPosition(Screen.Width / 5, Screen.Height / 2);
            Console.WriteLine(msg);
            Console.SetCursorPosition(Screen.Width / 5, Screen.Height / 2 + 1);

            if (waitReadKey) Console.ReadKey();
        }
    }
}
