using Checkers.Game.Board;
using Checkers.Game.Board.Filters;
using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    public class ShowPossibleMoves : ICheckerMove {
        readonly private Field[,] BoardFields;
        readonly private PlayerManager PlayerManager;
        readonly private BoardFilter BoardFilter;
        readonly private int BoardSize;
        readonly private GameConfiguration GameConfiguration;

        public ShowPossibleMoves(
            Field[,] boardFields,
            PlayerManager playerManager,
            GameConfiguration gameConfiguration) {
            GameConfiguration = gameConfiguration;
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
            BoardFilter = new BoardFilter(BoardFields, GameConfiguration);
        }

        public bool Perform(Field field) {
            if (!field.HasChecker() || !field.IsSelected()) {
                return true;
            }

            List<Point> possibleLocations  = BoardFilter.GetPossibleMoves(field);
            foreach (Point possibleLocation in possibleLocations) {
                (BoardFields[possibleLocation.X, possibleLocation.Y]).MarkAsPossibleMove();
            }

            return true;
        }
    }
}
