using Checkers.Game.Board;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class ShowPossibleMoves: CheckerMove {
        readonly private Field[,] BoardFields;
        readonly private PlayerManager PlayerManager;
        readonly private BoardFilter BoardFilter;
        readonly private int BoardSize;

        public ShowPossibleMoves(Field[,] boardFields, PlayerManager playerManager) {
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
            BoardFilter = new BoardFilter(BoardFields);
        }

        public bool Perform(Field field) {
            if (field.HasChecker() && field.IsSelected()) {
                List<Point> possibleLocations = null;
                if (field.PlacedChecker.IsQueen) {
                    possibleLocations = BoardFilter.GetPossibleLocationsForQueen(field);
                } else {
                    possibleLocations = BoardFilter.GetPossibleLocations(field);
                }

                List<Point> locationsUserCanMove = BoardFilter.FilterFieldsWithoutPawn(possibleLocations);

                locationsUserCanMove.AddRange(BoardFilter.GetPossibleMovesOverEnemies(field, PlayerManager.GetCurrentPlayer()));

                foreach (Point possibleLocation in locationsUserCanMove) {
                    (BoardFields[possibleLocation.X, possibleLocation.Y]).MarkAsPossibleMove();
                }
            }

            return true;
        }
    }
}
