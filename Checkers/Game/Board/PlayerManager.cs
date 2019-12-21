using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Game.Configuration;
using Checkers.Game.Entity;

namespace Checkers.Game.Board {
    public class PlayerManager {
        public event EventHandler PlayerChanged;
        public event EventHandler PlayerWon;

        public List<Player> Players { get; private set; }

        private Player CurrentPlayer;

        public PlayerManager() {
            Players = (new Players()).DefinedPlayers;
            CurrentPlayer = Players[1];
        }

        public Player GetCurrentPlayer() {
            return CurrentPlayer;
        }

        public void SwitchPlayer() {
            CurrentPlayer = Players.Where(player => player != CurrentPlayer).First();
            OnPlayerChanged(EventArgs.Empty);
        }

        protected virtual void OnPlayerChanged(EventArgs e) {
            PlayerChanged?.Invoke(CurrentPlayer, e);
        }

        public void CallAWinner(Player player) {
            PlayerWon?.Invoke(player, EventArgs.Empty);
        }
    }
}
