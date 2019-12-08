using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Checkers.Game {
    public class Pawn {
        public Color Color { get; private set; }

        public int MoveDirection { get; private set; }

        public bool IsQueen { get; private set; } = false;

        public Pawn(Color color, int moveDirection) {
            this.Color = color;
            this.MoveDirection = moveDirection;
        }
        public bool BelongsToPlayer(Player player) {
            return player.Color == Color;
        }

        public void ChangeDirection() {
            MoveDirection *= -1;
            IsQueen = true;
        }
    }
}
