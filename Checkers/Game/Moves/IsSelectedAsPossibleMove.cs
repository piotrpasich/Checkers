using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class IsSelectedAsPossibleMove: ICheckerMove {
        public bool Perform(Field field) {
            return field.IsSelectedAsPossibleMove();
        }
    }
}
