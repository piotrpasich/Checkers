﻿using Checkers.Game;
using Checkers.Game.Board;
using Checkers.Game.Entity;
using Checkers.Game.Moves;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers {
    public class GameManager {
        private int BoardSize { get; }

        public Field[,] BoardFields { get; private set; }

        private PlayerManager PlayerManager;

        private List<CheckerMove> CheckerMoves = new List<CheckerMove> { };

        public GameManager(Field[,] boardFields, PlayerManager playerManager) {
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
            RegisterDelegates();

            CheckerMoves.Add(new UnclickAll(BoardFields));
            CheckerMoves.Add(new SelectCurrentField(BoardFields, PlayerManager));
            CheckerMoves.Add(new ShowPossibleMoves(BoardFields, PlayerManager));
            CheckerMoves.Add(new IsSelectedAsPossibleMove());
            CheckerMoves.Add(new MakeMove(BoardFields, PlayerManager));
            CheckerMoves.Add(new ChangeDirection(BoardSize));
        }

        private void RegisterDelegates() {
            for (int x = 0; x < BoardFields.GetLength(0); x++) {
                for (int y = 0; y < BoardFields.GetLength(1); y++) {
                    (BoardFields[x, y]).FieldClicked += FieldClickedHandler;
                }
            }
        }

        private void FieldClickedHandler(object sender, EventArgs e) {
            ClickField((Field)sender);
        }

        private void ClickField(Field field) {
            foreach (CheckerMove PossibleMove in CheckerMoves) {
                if (!PossibleMove.Perform(field)) {
                    return;
                }
            }
        }     
    }
}