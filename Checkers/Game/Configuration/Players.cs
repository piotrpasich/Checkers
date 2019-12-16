using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Configuration {
    class Players {
        public List<Player> DefinedPlayers { get; } = new List<Player> {
            new Player("Blue Player", Color.Blue),
            new Player("Red Player", Color.Red),
        };
    }
}
