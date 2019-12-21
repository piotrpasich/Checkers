using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Checkers.Game.Configuration;
using System.IO;

namespace Checkers.Game.Entity {
    public class Field : PictureBox {

        private Color InitialColor { get; }

        public Point Localization { get; }

        public Checker PlacedChecker { get; private set; }

        readonly private Color SelectionColor = Color.Lime;

        readonly private Color PossibleMoveColor = Color.Gray;

        public event EventHandler FieldClicked;

        public bool HasPossibleMoves { get; private set; }

        public Field(BoardConfiguration boardConfiguration, Point localization) {
            this.Localization = localization;

            int fieldSize = boardConfiguration.FieldSize;
            int boardSize = boardConfiguration.BoardSize;

            ClientSize = new Size(fieldSize, fieldSize);

            int x = localization.X;
            int y = localization.Y;

            Location = new Point(
                fieldSize + (fieldSize * x) % (fieldSize * boardSize),
                fieldSize + (fieldSize * y) % (fieldSize * boardSize)
                );

            int moveBy = boardConfiguration.ColorForTopLeftCorner == Color.White ? 1 : 0;
            BackColor = ((x + y + moveBy) % 2 == 1) ? Color.White : Color.Black;
            Name = String.Concat("Field_", y, "_", x);
            InitialColor = BackColor;

            MouseClick += ClickField;
        }

        private void ClickField(Object sender, EventArgs e) {
            FieldClicked?.Invoke(this, e);
        }

        public void PlaceChecker(Checker checkerToPlace) {
            PlacedChecker = checkerToPlace;
            string queenPostfix = PlacedChecker.IsQueen ? "_queen" : "";
            string possibleToMovePostfix = HasPossibleMoves ? "_glow" : "";
            if (checkerToPlace.Color == Color.White) {
                Image = Image.FromFile(@"..\Images\white" + queenPostfix + possibleToMovePostfix + ".png");
            } else {
                Image = Image.FromFile(@"..\Images\black" + queenPostfix + possibleToMovePostfix + ".png");
            }
        }

        public void RemoveChecker() {
            PlacedChecker = null;
            Image = null;
            BackColor = InitialColor;
            HasPossibleMoves = false;
        }

        public void MarkAsPossibleMove() {
            BackColor = PossibleMoveColor;
            
        }

        public void MarkAsPossibleToMove() {
            HasPossibleMoves = true;
            PlaceChecker(PlacedChecker);
        }

        public void MarkAsImossibleToMove() {
            HasPossibleMoves = false;
            PlaceChecker(PlacedChecker);
        }

        public bool IsSelected() {
            return BackColor == SelectionColor;
        }

        public bool IsSelectedAsPossibleMove() {
            return BackColor == PossibleMoveColor;
        }

        public void Unclick() {
            BackColor = InitialColor;
            HasPossibleMoves = false;
        }

        public bool IsBlack() {
            return InitialColor == Color.Black;
        }

        public bool HasChecker() {
            return PlacedChecker != null;
        }

        public void Select() {
            BackColor = SelectionColor;
        }
    }
}
