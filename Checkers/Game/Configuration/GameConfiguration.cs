using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Configuration {
    public class GameConfiguration {
        public bool CanCheckerBeatQueen() {
            return true;
        }

        public bool CanQueenMoveOverMoreFields() {
            return false;
        }

        public bool ShouldCheckerMakeTheBestMoveFirst() {
            return false;
        }

        public bool CanCheckerMakeReverseBeat() {
            return true;
        }

        public Color GetColorForTopLeftCorner() {
            return Color.White;
        }

        public int GetBoardSize () {
            return 8;
        }
    }
}
