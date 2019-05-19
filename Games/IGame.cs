using System;
using snake_game.Configuration;
using snake_game.IO;

namespace snake_game.Games
{
    public interface IGame
    {
        void Play(Action<Status, ScreenManager> onEndGame);
        void SetUp();
    }
}