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
    // @papi
    // dekoratory - sprawdzic czy wszedzie sa dobrze porobione
    // sprawdzanie po polach, a nie kolorach 
    // testy 
    // podmienic grafiki na pionkach
    // dodac jakis tytul czy cos
    // kto wygral
    // czy nie ma wiecej ruchow
    // restart gry
    // zegar szachowy ? 


    public partial class Board : Form {
        private GameManager gameBoard;
        private PlayerManager PlayerManager;
        private GameConfiguration GameConfiguration;

        public Board() {
            InitializeComponent();
        }
        
        private void Board_Load(object sender, EventArgs e) {
            DrawBoard();
        }

        private void DrawBoard() {
            this.Winner.Visible = false;

            PlayerManager = new PlayerManager();
            GameConfiguration = new GameConfiguration();
            PlayerManager.PlayerChanged += PlayerChangedHandler;
            PlayerManager.PlayerWon += PlayerWonHandler;

            BoardBuilder gameBoardBuilder = new BoardBuilder((new Players()).DefinedPlayers, new BoardConfiguration(GameConfiguration));
            gameBoard = new GameManager(gameBoardBuilder.Build(), PlayerManager, GameConfiguration);
            
            foreach (Field field in gameBoard.BoardFields) {
                Controls.Add(field);
            }
            PlayerManager.SwitchPlayer();
            this.Player.Text = PlayerManager.GetCurrentPlayer().Name;
        }

        private void PlayerChangedHandler (object sender, EventArgs e) {
            Player newPlayer = (Player)sender;
            this.Player.Text = newPlayer.Name;
        }

        private void PlayerWonHandler(object sender, EventArgs e) {
            Player newPlayer = (Player)sender;
            this.Winner.Visible = true;
            this.Winner.Text = newPlayer.Name + " won";
        }
    }
}
