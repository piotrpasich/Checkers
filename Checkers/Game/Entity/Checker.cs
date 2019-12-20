using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Checkers.Game.Entity {
    public class Checker {
        public Color Color { get; private set; }

        public int MoveDirection { get; private set; }

        public bool IsQueen { get; private set; } = false;

        public Player Player { get; private set; }

        public Checker(Player player, int moveDirection) {
            Player = player;
            Color = player.Color;
            MoveDirection = moveDirection;
        }
        public bool BelongsToPlayer(Player player) {
            return player.Color == Color;
        }

        public void MakeQueen() {
            IsQueen = true;
        }
    }
}
