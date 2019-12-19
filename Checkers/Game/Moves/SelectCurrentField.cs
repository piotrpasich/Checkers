using Checkers.Game.Board;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class SelectCurrentField: ICheckerMove {
        readonly Field[,] BoardFields;
        readonly PlayerManager PlayerManager;

        public SelectCurrentField(Field[,] boardFields, PlayerManager playerManager) {
            BoardFields = boardFields;
            PlayerManager = playerManager;
        }

        public bool Perform(Field field) {
            if (field.HasChecker() && !field.PlacedChecker.BelongsToPlayer(PlayerManager.GetCurrentPlayer())) {
                return true;
            }
            if (field.HasChecker() && field.BackColor == Color.Black) {
                field.Select();
            }

            return true;
        }
    }
}
