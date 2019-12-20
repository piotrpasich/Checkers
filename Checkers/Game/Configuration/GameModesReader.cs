using Checkers.Game.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace Checkers.Game.Configuration {
    public class GameModesReader {
        readonly private string path = @"..\Config\GameModes.yml";

        public List<GameModeConfiguration> GameModeConfigurations { get; private set; }

        public GameModesReader () {
            var r = new StreamReader(@path);
            var deserializer = new DeserializerBuilder().Build();
            GameModeConfigurations = deserializer.Deserialize<List<GameModeConfiguration>>(r);
        }
    }
}
