using snake_game.IO;
using System;

namespace snake_game.Configuration
{
    /// <summary>
    /// Status, va a contener los datos del juego mientras el mismo este en ejecucion
    /// lo hago singleton para asegurarme de manejar el mismo estado en todo el tiempo de vida de una "session" de juego
    /// </summary>
    public class Status
    {
        public bool Continue { get; set; }
        public int Score { get; private set; }
        public MoveTo CurrentMove { get; private set; }
        public DateTime BeginTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public void UpdateMove(MoveTo nextMove) => this.CurrentMove = nextMove;
        public void BeginCoolDown() => this.BeginTime = DateTime.Now;
        public void Tick() => this.EndTime = DateTime.Now;
        private static Status status;

        protected Status()
        {
            //estado inicial
            this.Continue = true;
            this.CurrentMove = MoveTo.Right;
            this.Score = 5;
            this.EndTime = DateTime.Now;
        }

        public static Status GetStatus()
        {
            if (status == null)
                status = new Status();

            return status;
        }

       

        public void AddScore(int points = 1) =>
            this.Score += points;
    }
}
