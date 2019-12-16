using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Board {
    public class BoardBuilder : BuilderBase {
        readonly private List<Player> Players;

        public BoardBuilder(List<Player> players, BoardConfiguration boardConfiguration) : base(null, boardConfiguration) {
            Players = players;

            BuildGameBoard();
        }

        public Field[,] Build() {
            BoardFields = (new BoardFiller(BoardFields, BoardConfiguration)).Build();
            BoardFields = (new CheckersPlacer(BoardFields, Players, BoardConfiguration)).Build();

            return BoardFields;
        }

        private void BuildGameBoard() {
            BoardFields = new Field[BoardConfiguration.BoardSize, BoardConfiguration.BoardSize];
        }
    }
}
