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

namespace Checkers {
    

    public partial class Board : Form {
        private int boardSize = 8;
        private int fieldSize = 50;

        private GameBoard gameBoard;

        public Board() {
            InitializeComponent();
        }
        
        private void Board_Load(object sender, EventArgs e) {
            DrawBoard();
        }

        private void DrawBoard() {
            gameBoard = new GameBoard(boardSize, fieldSize);
            gameBoard.PlayerChanged += c_PlayerChanged;
            foreach (Field field in gameBoard.BoardFields) {
                Controls.Add(field);
            }
            this.Player.Text = gameBoard.GetCurrentPlayer().Name;
        }

        private void c_PlayerChanged(object sender, EventArgs e) {
            Player newPlayer = (Player)sender;
            this.Player.Text = newPlayer.Name;
        }

        private void Player_Click(object sender, EventArgs e) {

        }
    }
}
