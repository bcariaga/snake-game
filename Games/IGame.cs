using System;
using snake_game.Configuration;
using snake_game.IO;

namespace snake_game.Games
{
    /// <summary>
    /// Contrato que deben cumplir los juegos dentro de la sln
    /// </summary>
    public interface IGame
    {
        void Play(Action<Status, ScreenManager> onEndGame);
        void SetUp();
    }
}