using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Configuration {
    public class BoardConfiguration {
        public int BoardSize { get; private set; } = 8;
        public int FieldSize { get; } = 50;
        public List<int> RowsWithCheckers { get; private set; }
        public Color ColorForTopLeftCorner { get; private set; }
        public List<Color> FieldsColors { get; private set; }

        public BoardConfiguration (GameConfiguration GameConfiguration) {
            BoardSize = GameConfiguration.GetBoardSize();
            ColorForTopLeftCorner = GameConfiguration.GetColorForTopLeftCorner();
            RowsWithCheckers = GameConfiguration.GetRowsWithCheckers();
            FieldsColors = GameConfiguration.GetFieldsColors();
        }
    }
}
