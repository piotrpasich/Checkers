using Checkers.Game;
using Checkers.Game.Entity;
using Checkers.Game.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Checkers.Game.Board {
    public interface Builder {
        Field[,] Build();
    }
}
