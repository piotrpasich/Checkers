using System;
using System.Collections.Generic;
using System.Drawing;
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
        public string RowsWithCheckers { get;  set; } 
        public string FieldsColors { get; set; }

        public List<int> GetRowsWithCheckers () {
            return RowsWithCheckers.Split(',').ToList().Select( row => int.Parse(row)).ToList();
        }

        public List<Color> GetFieldsColors() {
            return FieldsColors.Split(',').ToList().Select(row => Color.FromName(row)).ToList();
        }
    }
}
