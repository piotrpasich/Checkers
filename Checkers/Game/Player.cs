using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Checkers.Game {
    public class Player {
        public string Name { get; }
        public Color Color { get; }
        public Player (string name, Color color) {
            Name = name;
            Color = color;
        }
    }
}
