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

            BackColor = ((x + y) % 2 == 1) ? Color.White : Color.Black;
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
            if (checkerToPlace.Color == Color.White) {
                Image = Image.FromFile(@"..\Images\white" + queenPostfix + ".png");
            } else {
                Image = Image.FromFile(@"..\Images\black" + queenPostfix + ".png");
            }
        }

        public void RemoveChecker() {
            PlacedChecker = null;
            Image = null;
            BackColor = InitialColor;
        }

        public void MarkAsPossibleMove() {
            BackColor = PossibleMoveColor;
        }

        public bool IsSelected() {
            return BackColor == SelectionColor;
        }

        public bool IsSelectedAsPossibleMove() {
            return BackColor == PossibleMoveColor;
        }

        public void Unclick() {
            BackColor = InitialColor;
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
