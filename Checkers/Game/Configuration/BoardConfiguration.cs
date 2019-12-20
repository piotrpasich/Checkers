using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Configuration {
    public class BoardConfiguration {
        public int BoardSize { get; private set; } = 8;
        public int FieldSize { get; } = 50;

        public BoardConfiguration (GameConfiguration GameConfiguration) {
            BoardSize = GameConfiguration.GetBoardSize();
        }
    }
}
