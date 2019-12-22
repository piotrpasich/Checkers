using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Configuration {
    public class GameConfiguration {
        private GameModeConfiguration GameModeConfiguration;

        readonly private string ConfigurationName;

        public GameConfiguration(List<GameModeConfiguration> gameModeConfigurations, string configurationName) {
            GameModeConfiguration = gameModeConfigurations.Where(configuration => configuration.Name == configurationName).First();
        }

        public bool CanCheckerBeatQueen() {
            return Convert.ToBoolean(GameModeConfiguration.CanCheckerBeatQueen);
        }

        public bool CanQueenMoveOverMoreFields() {
            return Convert.ToBoolean(GameModeConfiguration.CanQueenMoveOverMoreFields);
        }

        public bool ShouldCheckerMakeTheBestMoveFirst() {
            return Convert.ToBoolean(GameModeConfiguration.ShouldCheckerMakeTheBestMoveFirst);
        }

        public bool CanCheckerMakeReverseBeat() {
            return Convert.ToBoolean(GameModeConfiguration.CanCheckerMakeReverseBeat);
        }

        public Color GetColorForTopLeftCorner() {
            return GameModeConfiguration.ColorForTopLeftCorner == 1 ? Color.White : Color.Black;
        }

        public List<Color> GetFieldsColors () {
            return GameModeConfiguration.GetFieldsColors();
        }

        public int GetBoardSize () {
            return GameModeConfiguration.BoardSize;
        }

        public List<int> GetRowsWithCheckers() {
            return GameModeConfiguration.GetRowsWithCheckers();
        }
    }
}
