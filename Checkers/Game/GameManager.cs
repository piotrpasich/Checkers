using Checkers.Game;
using Checkers.Game.Board;
using Checkers.Game.Configuration;
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

        readonly private PlayerManager PlayerManager;

        private List<ICheckerMove> CheckerMoves = new List<ICheckerMove> { };

        private readonly GameConfiguration GameConfiguration;

        private readonly GlowPossibleMoves GlowPossibleMoves;

        public GameManager(Field[,] boardFields, PlayerManager playerManager, GameConfiguration gameConfiguration) {
            GameConfiguration = gameConfiguration;
            BoardFields = boardFields;
            BoardSize = boardFields.GetLength(0);
            PlayerManager = playerManager;
            RegisterDelegates();

            MakeMove MakeMove = new MakeMove(BoardFields, PlayerManager, GameConfiguration);
            MakeMove.FieldClicked += FieldClickedSecondTimeHandler;

            CheckerMoves.Add(new UnclickRedundantFields(BoardFields));
            CheckerMoves.Add(new SelectCurrentField(BoardFields, PlayerManager));
            CheckerMoves.Add(new ShowPossibleMoves(BoardFields, PlayerManager, GameConfiguration));
            CheckerMoves.Add(new IsSelectedAsPossibleMove());
            CheckerMoves.Add(MakeMove);
            CheckerMoves.Add(new CheckWinner(BoardFields, PlayerManager, GameConfiguration));
            CheckerMoves.Add(new MakeQueen(BoardSize));

            GlowPossibleMoves = new GlowPossibleMoves(BoardFields, GameConfiguration);
            GlowPossibleMoves.MarkPossibleMoves(PlayerManager.GetCurrentPlayer());
            PlayerManager.PlayerChanged += GlowPossibleMoves.PlayerChangedHandler;
        }

        private void RegisterDelegates() {
            for (int x = 0; x < BoardFields.GetLength(0); x++) {
                for (int y = 0; y < BoardFields.GetLength(1); y++) {
                    (BoardFields[x, y]).FieldClicked += FieldClickedHandler;
                }
            }
        }

        private void PlayerWonHandler(object sender, EventArgs e) {
            Player player = (Player) sender;
        }

        private void FieldClickedSecondTimeHandler(object sender, EventArgs e) {
            FieldClickedHandler(sender, e);
        }

        private void FieldClickedHandler(object sender, EventArgs e) {
            ClickField((Field)sender);
        }

        private void ClickField(Field field) {
            foreach (ICheckerMove PossibleMove in CheckerMoves) {
                if (!PossibleMove.Perform(field)) {
                    return;
                }
            }
        }     

        public void Dispose() {
            foreach (Field field in BoardFields) {
                field.Dispose();
            }
        }
    }
}
