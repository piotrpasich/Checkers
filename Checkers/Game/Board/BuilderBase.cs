using Checkers.Game.Configuration;
using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Board {
    public abstract class BuilderBase {
        public Builder Builder;
        public Field[,] BoardFields { get; protected set; }
        public BoardConfiguration BoardConfiguration { get; protected set; }

        public BuilderBase(Field[,] boardFields, BoardConfiguration boardConfiguration) {
            BoardFields = boardFields;
            BoardConfiguration = boardConfiguration;
        }
    }
}
