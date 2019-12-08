using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Checkers.Game {
    public class Field : PictureBox {

        private Color InitialColor { get; }

        public Point Localization { get; }

        public Pawn PlacedPawn { get; private set; }

        readonly private Color SelectionColor = Color.Lime;

        readonly private Color PossibleMoveColor = Color.Gray;

        public Field(int fieldSize, int boardSize, Point localization) {
            this.Localization = localization;

            ClientSize = new Size(fieldSize, fieldSize);

            int x = localization.X;
            int y = localization.Y;

            Location = new Point(
                50 + (50 * x) % (fieldSize * boardSize),
                50 + (50 * y) % (fieldSize * boardSize)
                );

            BackColor = ((x + y) % 2 == 1) ? Color.White : Color.Black;
            Name = String.Concat("Field_", y, "_", x);
            InitialColor = BackColor;
        }

        public void PlacePawn(Pawn pawnToPlace) {
            PlacedPawn = pawnToPlace;
            BackColor = pawnToPlace.Color;
        }

        public void RemovePawn() {
            PlacedPawn = null;
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
            BackColor = HasPawn() ? PlacedPawn.Color : InitialColor;
        }

        public bool IsBlack() {
            return InitialColor == Color.Black;
        }

        public bool HasPawn() {
            return PlacedPawn != null;
        }

        public void Select() {
            BackColor = SelectionColor;
        }
    }
}
