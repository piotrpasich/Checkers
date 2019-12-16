using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class ChangeDirection: CheckerMove {
        int BoardSize;

        public ChangeDirection (int boardSize) {
            BoardSize = boardSize;
        }

        public bool Perform (Field field) {
            if (field.HasChecker() &&
                ((field.PlacedChecker.MoveDirection == -1 && field.Localization.Y == BoardSize - 1) ||
                 (field.PlacedChecker.MoveDirection == 1 && field.Localization.Y == 0)
                )) {
                field.PlacedChecker.ChangeDirection();

                return true;
            }
            return false;
        }
    }
}
