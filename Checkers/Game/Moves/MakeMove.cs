using Checkers.Game.Board;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class MakeMove: CheckerMove {
        readonly private Field[,] BoardFields;
        readonly private PlayerManager PlayerManager;
        readonly private int BoardSize;

        public MakeMove(Field[,] boardFields, PlayerManager playerManager) {
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
        }

        public bool Perform (Field field) {
            Field currentlySelectedField = GetCurrentlySelectedChecker();
            field.PlaceChecker(currentlySelectedField.PlacedChecker);
            currentlySelectedField.RemoveChecker();

            bool hasJumpedOverEnemy = BeatChecker(field, currentlySelectedField);

            UnclickAll();

            if (!hasJumpedOverEnemy || !CanMakeAMove(field)) {
                PlayerManager.SwitchPlayer();
            } else {
                Console.WriteLine("Click Field");
                // ClickField(field);
            }

            return true;
        }

        private Field GetCurrentlySelectedChecker() {
            List<Field> fields = (BoardFields.Cast<Field>()).ToList();

            return fields.Find(field => {
                return field.IsSelected();
            });
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

        private bool CanMakeAMove(Field selectedField) {
            return GetPossibleLocationsOfNextMove(selectedField, selectedField.PlacedChecker.MoveDirection)
                .Where(point => (
                point.X != 0 &&
                point.Y != 0 &&
                point.X != BoardSize - 1 &&
                point.Y != BoardSize - 1 &&
                GetPossibleMovesOverEnemies(selectedField).Count > 0
                ))
                .ToList().Count() > 0;
        }

        private List<Point> GetPossibleMovesOverEnemies(Field field) {
            List<Point> possibleMoves = new List<Point> { };
            List<Point> possibleLocations = (new List<Point> {
                    new Point(field.Localization.X - 1, field.Localization.Y - 1),
                    new Point(field.Localization.X + 1, field.Localization.Y - 1),
                    new Point(field.Localization.X - 1, field.Localization.Y + 1),
                    new Point(field.Localization.X + 1, field.Localization.Y + 1),
                }).Where(point => (
                   point.X >= 0 &&
                   point.X < BoardSize &&
                   point.Y >= 0 &&
                   point.Y < BoardSize
                   )).ToList();

            List<Point> locationsWithEnemies = possibleLocations.Where(point => (
                   (BoardFields[point.X, point.Y]).HasChecker() &&
                   !((BoardFields[point.X, point.Y]).PlacedChecker.BelongsToPlayer(PlayerManager.GetCurrentPlayer()))
                   )).ToList();

            foreach (Point possibleLocation in locationsWithEnemies) {
                possibleMoves.AddRange(GetPossibleMovesForNextStep((BoardFields[possibleLocation.X, possibleLocation.Y]), field));
            }

            return possibleMoves;
        }

        private List<Point> GetPossibleLocationsOfNextMove(Field field, int moveDirection) {
            Point location = field.Localization;
            return (new List<Point> {
                    new Point(location.X - 1, location.Y - moveDirection),
                    new Point(location.X + 1, location.Y - moveDirection),
                }).Where(point => (
                   point.X >= 0 &&
                   point.X < BoardSize &&
                   point.Y >= 0 &&
                   point.Y < BoardSize
                   )).ToList();
        }

        private List<Point> GetPossibleMovesForNextStep(Field currentField, Field initialField) {
            List<Point> possibleLocations = GetPossibleLocationsOfNextMove(currentField, (initialField.Localization.Y > currentField.Localization.Y ? 1 : -1));
            List<Point> locationsUserCanMove = possibleLocations.Where(point => (
                   !((BoardFields[point.X, point.Y]).HasChecker())
                   )).Where(point => (
                    point.X != initialField.Localization.X
                   )).ToList();

            return locationsUserCanMove;
        }
    }
}
