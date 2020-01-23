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
            new Player("Black Checkers", Color.Black),
            new Player("Red Checkers", Color.White),
        };
    }
}
