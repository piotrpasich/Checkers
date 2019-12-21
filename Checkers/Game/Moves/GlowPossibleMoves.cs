using Checkers.Game.Board.Filters;
using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    public class GlowPossibleMoves {
        readonly private Field[,] BoardFields;
        readonly private BoardFilter BoardFilter;
        readonly private GameConfiguration GameConfiguration;

        public GlowPossibleMoves(Field[,] boardFields, GameConfiguration gameConfiguration) {
            BoardFields = boardFields;
            GameConfiguration = gameConfiguration;
            BoardFilter = new BoardFilter(boardFields, GameConfiguration);
        }

        public void PlayerChangedHandler(object sender, EventArgs e) {
            MarkPossibleMoves((Player)sender);
        }

        public bool MarkPossibleMoves (Player currentPlayer) {
            bool hasPossibleMoves = false;
            int i = 0;
            foreach (Field field in BoardFields) {
                if (field.HasChecker()) {
                    field.MarkAsImossibleToMove();
                    i++;
                    if (field.PlacedChecker.BelongsToPlayer(currentPlayer) && BoardFilter.CanMakeAMove(field, currentPlayer)) {
                        field.MarkAsPossibleToMove();
                        hasPossibleMoves = true;
                    }
                }
            }
            Console.WriteLine(i);

            return hasPossibleMoves;
        }
    }
}
