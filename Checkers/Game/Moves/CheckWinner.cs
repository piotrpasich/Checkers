using Checkers.Game.Board;
using Checkers.Game.Board.Filters;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    public class CheckWinner: ICheckerMove {
        readonly private Field[,] BoardFields;
        readonly private BoardFilter BoardFilter;
        readonly private PlayerManager PlayerManager;

        public CheckWinner(Field[,] boardFields, PlayerManager playerManager) {
            BoardFields = boardFields;
            BoardFilter = new BoardFilter(BoardFields);
            PlayerManager = playerManager;
        }

        public bool Perform(Field field) {
            List<Checker> checkers = new List<Checker> { };
            foreach (Field iteratedField in BoardFields) {
                if (iteratedField.HasChecker()) {
                    checkers.Add(iteratedField.PlacedChecker);
                }
            }

            if (checkers.GroupBy(checker => checker.Player.Color).ToList().Count > 1) {
                return true;
            }

            PlayerManager.CallAWinner(checkers.First().Player);

            return false;
        }
    }
}
