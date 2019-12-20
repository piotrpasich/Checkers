using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Board {
    public class CheckersPlacer : BuilderBase {
        private List<Player> Players;

        public CheckersPlacer(Field[,] boardFields, List<Player> players, BoardConfiguration boardConfiguration) : base(boardFields, boardConfiguration) {
            Players = players;
        }

        public Field[,] Build() {
            PlaceCheckers();

            return BoardFields;
        }

        private void PlaceCheckers() {
            List<int> rowsWithCheckers = new List<int> { 0, 1, BoardConfiguration.BoardSize - 1, BoardConfiguration.BoardSize - 2 };
            foreach (int y in rowsWithCheckers) {
                for (int i = 0; i < BoardFields.GetLength(0); i++) {
                    if (BoardFields[i, y].IsBlack()) {
                        Player checkerPlayer = (rowsWithCheckers.Take(2)).Contains(y) ? Players[0] : Players[1];
                        int moveDirection = (rowsWithCheckers.Take(2)).Contains(y) ? -1 : 1;
                        (BoardFields[i, y]).PlaceChecker(new Checker(checkerPlayer, moveDirection));
                    }
                }
            }
        }
    }
}
