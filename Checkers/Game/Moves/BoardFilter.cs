using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Board {
    public class BoardFilter {
        readonly private Field[,] BoardFields;
        readonly private int BoardSize;

        public BoardFilter(Field[,] boardFields) {
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
        }

        public List<Point> GetPossibleLocationsForQueen(Field selectedField) {
            List<Point> possiblePointsToMove = new List<Point> { };

            for (int i = 1; i < BoardSize; i++) {
                possiblePointsToMove.Add(new Point(selectedField.Localization.X - i, selectedField.Localization.Y - i));
                possiblePointsToMove.Add(new Point(selectedField.Localization.X - i, selectedField.Localization.Y + i));
                possiblePointsToMove.Add(new Point(selectedField.Localization.X + i, selectedField.Localization.Y + i));
                possiblePointsToMove.Add(new Point(selectedField.Localization.X + i, selectedField.Localization.Y - i));
            }

            bool[] metPawn = { false, false, false, false };

            return possiblePointsToMove
                .Where((point, index) => {
                    if (metPawn[index % 4] == true) {
                        return false;
                    }
                    try { BoardFields.GetValue(point.X, point.Y); } catch { return true; }
                    if (BoardFields[point.X, point.Y].HasChecker()) {
                        metPawn[index % 4] = true;
                        return false;
                    }
                    return true;
                })
                .Where(point => (
                   point.X >= 0 &&
                   point.X < BoardSize &&
                   point.Y >= 0 &&
                   point.Y < BoardSize
                   ))
                .ToList();
        }

        public List<Point> GetPossibleLocations(Field selectedField) {
            return (new List<Point> {
                    new Point(selectedField.Localization.X - 1, selectedField.Localization.Y - selectedField.PlacedChecker.MoveDirection),
                    new Point(selectedField.Localization.X + 1, selectedField.Localization.Y - selectedField.PlacedChecker.MoveDirection),
                }).Where(point => (
                   point.X >= 0 &&
                   point.X < BoardSize &&
                   point.Y >= 0 &&
                   point.Y < BoardSize
                   )).ToList();
        }

        public List<Point> GetPossibleMovesOverEnemies(Field field, Player currentPlayer) {
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
                   !((BoardFields[point.X, point.Y]).PlacedChecker.BelongsToPlayer(currentPlayer))
                   )).ToList();

            foreach (Point possibleLocation in locationsWithEnemies) {
                possibleMoves.AddRange(GetPossibleMovesForNextStep((BoardFields[possibleLocation.X, possibleLocation.Y]), field));
            }

            return possibleMoves;
        }

        public List<Point> GetPossibleLocationsOfNextMove(Field field, int moveDirection) {
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

        public List<Point> FilterFieldsWithoutPawn(List<Point> locations) {
            return locations.Where(point => (
                   !((BoardFields[point.X, point.Y]).HasChecker())
                   )).ToList();
        }

        public List<Point> GetPossibleMovesForNextStep(Field currentField, Field initialField) {
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
