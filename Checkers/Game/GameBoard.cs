using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers.Game;

namespace Checkers.Game {
    class GameBoard {

        protected virtual void OnPlayerChanged (EventArgs e) {
            PlayerChanged?.Invoke(CurrentPlayer, e);
            /*
             * EventHandler handler = ThresholdReached;
             * if (handler != null) 
             * handler(this, e)
             */
        }

        public event EventHandler PlayerChanged;

        private int BoardSize { get; }

        public Field[,] BoardFields { get; private set; }

        private List<Player> Players = new List<Player> {
            new Player("Player 1", Color.Blue),
            new Player("Player 2", Color.Red),
        };

        private Player CurrentPlayer;

        public GameBoard(int boardSize, int fieldSize) {
            this.BoardSize = boardSize;
            CurrentPlayer = Players[0];
            BoardFields = new Field[boardSize, boardSize];
            FillBoard(fieldSize);
            PlacePawns();
        }

        public Player GetCurrentPlayer() {
            return CurrentPlayer;
        }

        public void SwitchPlayer() {
            CurrentPlayer = Players.Where(player => player != CurrentPlayer).First();
            OnPlayerChanged(EventArgs.Empty);
        }

        private void FillBoard(int fieldSize) {
            for (int y = 0; y < this.BoardSize; y++) {
                for (int x = 0; x < this.BoardSize; x++) {
                    Field newField = new Field(fieldSize, BoardSize, new Point(x, y));
                    newField.MouseClick += ClickField;
                    BoardFields[x, y] = newField;
                }
            }
        }

        private void PlacePawns() {
            List<int> rowsWithPawns = new List<int> { 0, 1, BoardSize - 1, BoardSize - 2 };
            foreach (int y in rowsWithPawns) {
                for (int i = 0; i < BoardFields.GetLength(0); i++) {
                    if (BoardFields[i, y].IsBlack()) {
                        Color pawnColor = (rowsWithPawns.Take(2)).Contains(y) ? Players[0].Color : Players[1].Color;
                        int moveDirection = (rowsWithPawns.Take(2)).Contains(y) ? -1 : 1;
                        (BoardFields[i, y]).PlacePawn(new Pawn(pawnColor, moveDirection));
                    }
                }
            }
        }

        private void ClickField(object sender, MouseEventArgs e) {
            ClickField((Field)sender);
        }

        private void ClickField(Field field) {
            UnclickAll(field);
            SelectField(field);
            ShowPossibleMoves(field);
            if (field.IsSelectedAsPossibleMove()) {
                MakeMove(field);
                ChangeDirection(field);
            }
        }

        private void ChangeDirection(Field field) {
            if (field.HasPawn() &&
                ((field.PlacedPawn.MoveDirection == -1 && field.Localization.Y == BoardSize - 1) ||
                 (field.PlacedPawn.MoveDirection == 1 && field.Localization.Y == 0)
                )) {
                field.PlacedPawn.ChangeDirection();
            }
        }

        private void SelectField(Field field) {
            if (field.HasPawn() && !field.PlacedPawn.BelongsToPlayer(CurrentPlayer)) {
                return;
            }
            if (field.HasPawn() && field.BackColor == field.PlacedPawn.Color) {
                field.Select();
            }
        }

        private void MakeMove(Field selectedField) {
            Field currentlySelectedField = GetCurrentlySelectedPawn();
            selectedField.PlacePawn(currentlySelectedField.PlacedPawn);
            currentlySelectedField.RemovePawn();
           
            bool hasJumpedOverEnemy = BeatChecker(selectedField, currentlySelectedField);

            UnclickAll();

            if (!hasJumpedOverEnemy || !CanMakeAMove(selectedField)) {
                SwitchPlayer();
            } else {
                ClickField(selectedField);
            }
        }

        private bool BeatChecker(Field selectedField, Field currentlySelectedField) {
            bool hasJumpedOverEnemy = false;
            if (Math.Abs(currentlySelectedField.Localization.X - selectedField.Localization.X) == 2) {
                (BoardFields[
                    (int)((currentlySelectedField.Localization.X + selectedField.Localization.X) / 2),
                    (int)((currentlySelectedField.Localization.Y + selectedField.Localization.Y) / 2)]).RemovePawn();
                hasJumpedOverEnemy = true;
            }

            return hasJumpedOverEnemy;
        }

        private bool CanMakeAMove(Field selectedField) {
            return GetPossibleLocationsOfNextMove(selectedField, selectedField.PlacedPawn.MoveDirection)
                .Where(point => (
                point.X != 0 &&
                point.Y != 0 &&
                point.X != BoardSize - 1 &&
                point.Y != BoardSize - 1 &&
                GetPossibleMovesOverEnemies(selectedField).Count > 0
                ))
                .ToList().Count() > 0;
        }

        private Field GetCurrentlySelectedPawn() {
            List<Field> fields = (BoardFields.Cast<Field>()).ToList();
          
            return fields.Find(field => {
                return field.IsSelected();
            });
        }

        private List<Point> GetPossibleMovesForNextStep (Field currentField, Field initialField) {
            List<Point> possibleLocations = GetPossibleLocationsOfNextMove(currentField, (initialField.Localization.Y > currentField.Localization.Y ? 1 : -1));
            List<Point> locationsUserCanMove = possibleLocations.Where(point => (
                   !((BoardFields[point.X, point.Y]).HasPawn())
                   )).Where(point => (
                    point.X != initialField.Localization.X
                   )).ToList();

            return locationsUserCanMove;
        }

        private void ShowPossibleMoves(Field selectedField) {
            if (selectedField.HasPawn() && selectedField.IsSelected()) {
                List<Point> possibleLocations = null;
                if (selectedField.PlacedPawn.IsQueen) {
                    possibleLocations = GetPossibleLocationsForQueen(selectedField);
                } else {
                    possibleLocations = GetPossibleLocations(selectedField);
                }
                
                List<Point> locationsUserCanMove = FilterFieldsWithoutPawn(possibleLocations);


                locationsUserCanMove.AddRange(GetPossibleMovesOverEnemies(selectedField));    

                foreach (Point possibleLocation in locationsUserCanMove) {
                    (BoardFields[possibleLocation.X, possibleLocation.Y]).MarkAsPossibleMove();
                }
            }
        }

        private List<Point> FilterFieldsWithoutPawn(List<Point> locations) {
            return locations.Where(point => (
                   !((BoardFields[point.X, point.Y]).HasPawn())
                   )).ToList();
        }


        private List<Point> GetPossibleLocationsForQueen (Field selectedField) {
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
                    if (BoardFields[point.X, point.Y].HasPawn()) {
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
        private List<Point> GetPossibleLocations (Field selectedField) {
            return (new List<Point> {
                    new Point(selectedField.Localization.X - 1, selectedField.Localization.Y - selectedField.PlacedPawn.MoveDirection),
                    new Point(selectedField.Localization.X + 1, selectedField.Localization.Y - selectedField.PlacedPawn.MoveDirection),
                }).Where(point => (
                   point.X >= 0 &&
                   point.X < BoardSize &&
                   point.Y >= 0 &&
                   point.Y < BoardSize
                   )).ToList();
        }

        private List<Point> GetPossibleMovesOverEnemies(Field field) {
            List<Point> possibleMoves = new List<Point> {};
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
                   (BoardFields[point.X, point.Y]).HasPawn() &&
                   !((BoardFields[point.X, point.Y]).PlacedPawn.BelongsToPlayer(CurrentPlayer))
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
        
        private void UnclickAll() {
            foreach (Field field in BoardFields) {
                field.Unclick();
            }
        }

        private void UnclickAll(Field clickedField) {
            foreach (Field field in BoardFields) {
                if ((field != clickedField || (!field.IsSelected())) && !clickedField.IsSelectedAsPossibleMove()) {
                    field.Unclick();
                }
            }
        }
    }
}
