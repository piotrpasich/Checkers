using Checkers.Game.Board;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class MakeMove: ICheckerMove {

        public event EventHandler FieldClicked;

        readonly private Field[,] BoardFields;
        readonly private PlayerManager PlayerManager;
        readonly private int BoardSize;
        readonly private BoardFilter BoardFilter;

        public MakeMove(
            Field[,] boardFields,
            PlayerManager playerManager) {

            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
            BoardFilter = new BoardFilter(boardFields);
            
        }

        public bool Perform(Field field) {
            Field currentlySelectedField = BoardFilter.GetCurrentlySelectedChecker();
            field.PlaceChecker(currentlySelectedField.PlacedChecker);
            currentlySelectedField.RemoveChecker();

            bool hasJumpedOverEnemy = BeatChecker(field, currentlySelectedField);

            UnclickAll();

            if (!hasJumpedOverEnemy || !BoardFilter.CanMakeAMove(field, PlayerManager.GetCurrentPlayer())) {
                PlayerManager.SwitchPlayer();
            } else {
                FieldClicked?.Invoke(field, EventArgs.Empty);
            }

            return true;
        }

        private bool BeatChecker(Field selectedField, Field currentlySelectedField) {
            bool hasJumpedOverEnemy = false;
            if (Math.Abs(currentlySelectedField.Localization.X - selectedField.Localization.X) == 2 &&
                (BoardFields[
                    (int)((currentlySelectedField.Localization.X + selectedField.Localization.X) / 2),
                    (int)((currentlySelectedField.Localization.Y + selectedField.Localization.Y) / 2)]).HasChecker()
                ) {
                (BoardFields[
                    (int)((currentlySelectedField.Localization.X + selectedField.Localization.X) / 2),
                    (int)((currentlySelectedField.Localization.Y + selectedField.Localization.Y) / 2)]).RemoveChecker();
                hasJumpedOverEnemy = true;
            }

            return hasJumpedOverEnemy;
        }

        private void UnclickAll() {
            foreach (Field field in BoardFields) {
                field.Unclick();
            }
        }
    }
}
