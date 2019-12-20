using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Entity {
    public class GameModeConfiguration {
        public string Name { get; set; }
        public int CanCheckerBeatQueen { get; set; }
        public int CanQueenMoveOverMoreFields { get; set; }
        public int ShouldCheckerMakeTheBestMoveFirst { get; set; }
        public int CanCheckerMakeReverseBeat { get; set; }
        public int ColorForTopLeftCorner { get; set; }
        public int BoardSize { get; set; }
    }
}
