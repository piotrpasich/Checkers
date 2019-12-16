using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Board {
    public class BoardFiller : BuilderBase {
        public BoardFiller(Field[,] boardFields, BoardConfiguration boardConfiguration) : base(boardFields, boardConfiguration) {
        }

        public Field[,] Build() {
            FillBoard();

            return BoardFields;
        }

        private void FillBoard() {
            for (int y = 0; y < BoardConfiguration.BoardSize; y++) {
                for (int x = 0; x < BoardConfiguration.BoardSize; x++) {
                    Field newField = new Field(
                        BoardConfiguration,
                        new Point(x, y));

                    BoardFields[x, y] = newField;
                }
            }
        }
    }

}
