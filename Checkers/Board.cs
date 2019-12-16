using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers.Game;
using Checkers.Game.Board;
using Checkers.Game.Entity;
using Checkers.Game.Configuration;

namespace Checkers {
    // dekoratory na gameBoard
    // sprawdzanie po polach, a nie kolorach 
    // testy 
    // podmienic grafiki na pionkach
    // dodac jakis tytul czy cos
    // kto wygral


    public partial class Board : Form {
        private GameManager gameBoard;
        private PlayerManager PlayerManager;

        public Board() {
            InitializeComponent();
        }
        
        private void Board_Load(object sender, EventArgs e) {
            DrawBoard();
        }

        private void DrawBoard() {
            PlayerManager = new PlayerManager();
            PlayerManager.PlayerChanged += PlayerChangedHandler;

            BoardBuilder gameBoardBuilder = new BoardBuilder((new Players()).DefinedPlayers, new BoardConfiguration());
            gameBoard = new GameManager(gameBoardBuilder.Build(), PlayerManager);
            
            foreach (Field field in gameBoard.BoardFields) {
                Controls.Add(field);
            }
            this.Player.Text = PlayerManager.GetCurrentPlayer().Name;
        }

        private void PlayerChangedHandler (object sender, EventArgs e) {
            Player newPlayer = (Player)sender;
            this.Player.Text = newPlayer.Name;
        }
    }
}
