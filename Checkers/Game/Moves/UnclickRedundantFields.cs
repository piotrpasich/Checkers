using Checkers.Game.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Game.Moves {
    class UnclickRedundantFields: ICheckerMove {
        readonly private Field[,] BoardFields;

        public UnclickRedundantFields(Field[,] boardFields) {
            BoardFields = boardFields;
        }

        public bool Perform(Field field) {
            foreach (Field iteratedField in BoardFields) {
                if ((iteratedField != field || (!iteratedField.IsSelected())) && !field.IsSelectedAsPossibleMove()) {
                    iteratedField.Unclick();
                }
            }
            return true;
        }
    }
}
