using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Board.Filters {
    public class BoardFilter {
        readonly private Field[,] BoardFields;
        readonly private int BoardSize;
        readonly private GameConfiguration GameConfiguration;

        public BoardFilter(Field[,] boardFields, GameConfiguration gameConfiguration) {
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            GameConfiguration = gameConfiguration;
        }

        public List<Point> GetPossibleMoves(Field field) {
            List<Point> possibleLocations = GetAllPossibleLocations(field);
            possibleLocations = LimitToBoard(possibleLocations);
            possibleLocations = LimitToNearestEnemies(field, possibleLocations);
            
            if (!field.PlacedChecker.IsQueen) {
                possibleLocations = LimitToDirection(possibleLocations, field);
            }
            if (!field.PlacedChecker.IsQueen || !GameConfiguration.CanQueenMoveOverMoreFields()) {
                possibleLocations = LimitToMaxJumps(possibleLocations, field);
            }
            possibleLocations.AddRange(GetPossibleMovesOverEnemies(field));

            possibleLocations = LimitToBoard(possibleLocations);
            
            return possibleLocations;
        }

        public List<Point> GetPossibleMovesOverEnemies(Field field) {
            List<Point> possibleLocations = new List<Point> { };
            List<Point> enemies = GetNearestEnemiest(field);
            if (!field.PlacedChecker.IsQueen && !GameConfiguration.CanCheckerBeatQueen()) {
                enemies = RemoveQueens(enemies, field);
            }

            foreach (Point enemyPoint in enemies) {
                Field enemy = BoardFields[enemyPoint.X, enemyPoint.Y];
                possibleLocations.AddRange(GetPossibleMovesOverEnemy(enemy, field));
            }

            possibleLocations = LimitToBoard(possibleLocations);

            return possibleLocations;
        }

        public bool CanMakeAMove(Field field, Player currentPlayer) {
            List<Point> possibleLocations = GetPossibleMoves(field);

            return possibleLocations.ToList().Count() > 0;
        }

        private int GetQuarter (Field currentField, Point point) {
            int x1 = currentField.Localization.X;
            int x2 = point.X;
            int y1 = currentField.Localization.Y;
            int y2 = point.Y;

            if (x2 >= x1 && y2 >= y1) {
                return 0;
            } else if (x2 >= x1 && y2 < y1) {
                return 1;
            } else if (x2 <= x1 && y2 <= y1) {
                return 2;
            } else {
                return 3;
            }
        }

        public List<Point> LimitToNearestEnemies(Field currentField, List<Point> possiblePointsToMove) {
            bool[] metChecker = { false, false, false, false };

            return possiblePointsToMove
                .Where((point, index) => {
                    if (metChecker[GetQuarter(currentField, point)] == true) {
                        return false;
                    }
                    try { BoardFields.GetValue(point.X, point.Y); } catch { return true; }
                    if (BoardFields[point.X, point.Y].HasChecker()) {
                        metChecker[GetQuarter(currentField, point)] = true;
                        return false;
                    }
                    return true;
                }).ToList();
        }

        public List<Point> LimitToBoard(List<Point> possibleLocations) {
            return possibleLocations.Where(point => (
                   point.X >= 0 &&
                   point.X < BoardSize &&
                   point.Y >= 0 &&
                   point.Y < BoardSize
                   )).ToList();
        }

        public List<Point> LimitToMaxJumps(List<Point> possibleLocations, Field currentlySelectedField) {
            return possibleLocations.Where(point => (
                Math.Abs(point.X - currentlySelectedField.Localization.X) < 2 &&
                Math.Abs(point.Y - currentlySelectedField.Localization.Y) < 2
            )).ToList();
        }

        public List<Point> LimitToDirection(List<Point> possibleLocations, Field currentlySelectedField) {
            int moveDirection = currentlySelectedField.PlacedChecker.MoveDirection;
            return possibleLocations.Where(point => (
                Math.Sign(currentlySelectedField.Localization.Y - point.Y) == Math.Sign(moveDirection)
            )).ToList();
        }

        public List<Point> LimitToHorizontalDirection(List<Point> possibleLocations, Field currentlySelectedField, Field enemyField) {
            int moveDirection = Math.Sign(currentlySelectedField.Localization.X - enemyField.Localization.X);
            return possibleLocations.Where(point => (
                Math.Sign(currentlySelectedField.Localization.X - point.X) == Math.Sign(moveDirection) &&
                currentlySelectedField.Localization.Y != point.Y
            )).ToList();
        }

        public List<Point> LimitToOnlyEnemies(List<Point> possibleLocations, Field currentlySelectedField) {
            return possibleLocations.Where(point => (
                   ((BoardFields[point.X, point.Y]).HasChecker()) &&
                   !((BoardFields[point.X, point.Y]).PlacedChecker.BelongsToPlayer(currentlySelectedField.PlacedChecker.Player))
                   )).ToList();
        }

        public List<Point> RemoveQueens(List<Point> possibleLocations, Field currentlySelectedField) {
            return possibleLocations.Where(point => (
                   ((BoardFields[point.X, point.Y]).HasChecker()) &&
                   !(BoardFields[point.X, point.Y]).PlacedChecker.IsQueen
                   )).ToList();
        }

        public List<Point> GetAllPossibleLocations(Field selectedField) {
            List<Point> possiblePointsToMove = new List<Point> { };

            for (int i = 1; i < BoardSize; i++) {
                possiblePointsToMove.Add(new Point(selectedField.Localization.X - i, selectedField.Localization.Y - i));
                possiblePointsToMove.Add(new Point(selectedField.Localization.X - i, selectedField.Localization.Y + i));
                possiblePointsToMove.Add(new Point(selectedField.Localization.X + i, selectedField.Localization.Y + i));
                possiblePointsToMove.Add(new Point(selectedField.Localization.X + i, selectedField.Localization.Y - i));
            }

            return possiblePointsToMove;
        }
        //@papi stare
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

        public Field GetCurrentlySelectedChecker() {
            List<Field> fields = (BoardFields.Cast<Field>()).ToList();

            return fields.Find(field => {
                return field.IsSelected();
            });
        }

        public List<Point> GetNearestEnemiest(Field field) {
            List<Point> possibleLocations = GetAllPossibleLocations(field);
            possibleLocations = LimitToBoard(possibleLocations);
            possibleLocations = LimitToMaxJumps(possibleLocations, field);
            return LimitToOnlyEnemies(possibleLocations, field);
        }

        public List<Point> GetPossibleMovesOverEnemy(Field enemy, Field currentlySelectedField) {
            List<Point> possibleEnemyLocations = GetAllPossibleLocations(enemy);
            possibleEnemyLocations = LimitToBoard(possibleEnemyLocations);
            possibleEnemyLocations = LimitToMaxJumps(possibleEnemyLocations, enemy);
            if (!currentlySelectedField.PlacedChecker.IsQueen && !GameConfiguration.CanCheckerMakeReverseBeat()) {
                possibleEnemyLocations = LimitToDirection(possibleEnemyLocations, currentlySelectedField);
            }
            possibleEnemyLocations = LimitToNearestEnemies(currentlySelectedField, possibleEnemyLocations);
            possibleEnemyLocations = LimitToHorizontalDirection(possibleEnemyLocations, currentlySelectedField, enemy);

            return possibleEnemyLocations;
        }

        public bool CanMakeAMoveOverAnEnemy(Field selectedField, Player currentPlayer) {
            return GetPossibleLocationsOfNextMove(selectedField, selectedField.PlacedChecker.MoveDirection)
                .Where(point => (
                point.X != 0 &&
                point.Y != 0 &&
                point.X != BoardSize - 1 &&
                point.Y != BoardSize - 1 &&
                GetPossibleMovesOverEnemies(selectedField, currentPlayer).Count > 0
                ))
                .ToList().Count() > 0;
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
