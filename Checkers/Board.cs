using System;
using System.Configuration;
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
    // testy 
    // drugi ruch - ukryc wszystko co nie jest biciem
    // uporzadkowac filtry
    // zegar szachowy ? 

    public partial class Board : Form {
        private GameManager GameManager;
        private PlayerManager PlayerManager;
        private GameConfiguration GameConfiguration;
        private BoardConfiguration BoardConfiguration;
        private List<GameModeConfiguration> GameModeConfigurations;

        public Board() {
            InitializeComponent();
        }
        
        private void Board_Load(object sender, EventArgs e) {
            DrawBoard();
        }

        private void DrawBoard(string gameMode = null) {
            PlayerManager = new PlayerManager();
            GameModeConfigurations = (new GameModesReader()).GameModeConfigurations;
            string gameModeName = gameMode ?? GameModeConfigurations.FirstOrDefault().Name;
            GameConfiguration = new GameConfiguration(GameModeConfigurations, gameModeName);
            BoardConfiguration = new BoardConfiguration(GameConfiguration);
            PlayerManager.PlayerChanged += PlayerChangedHandler;
            PlayerManager.PlayerWon += PlayerWonHandler;

            BoardBuilder gameBoardBuilder = new BoardBuilder((new Players()).DefinedPlayers, BoardConfiguration);
            GameManager = new GameManager(gameBoardBuilder.Build(), PlayerManager, GameConfiguration);
            
            foreach (Field field in GameManager.BoardFields) {
                Controls.Add(field);
            }
            
            SetTheDesignElements();
            ShowGameConfigurationsPicker(gameModeName);
            AdjustElements();
        }

        private void SetTheDesignElements() {
            this.Player.Text = PlayerManager.GetCurrentPlayer().Name;
            this.Winner.Visible = false;
            this.Winner.Width = BoardConfiguration.BoardSize * BoardConfiguration.FieldSize + 100;
            this.Player.Width = BoardConfiguration.BoardSize * BoardConfiguration.FieldSize + 100;
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

        private void NewGame_Click(object sender, EventArgs e) {
            string selectedGameMode = GameConfigurationsPicker.SelectedItem.ToString();
            GameManager.Dispose();
            DrawBoard(selectedGameMode);
        }


        private void ShowGameConfigurationsPicker(string selectedMode) {
            GameConfigurationsPicker.Items.Clear();
            GameModeConfigurations.ForEach(delegate (GameModeConfiguration gameModeConfiguration) {
                GameConfigurationsPicker.Items.Add(gameModeConfiguration.Name);
            });
            GameConfigurationsPicker.SelectedItem = selectedMode;
            GameConfigurationsPicker.SelectedIndexChanged += new System.EventHandler(GameConfigurationChanged);
            ShowGameModeOptions(selectedMode);
        }

        private void GameConfigurationChanged(object sender, System.EventArgs e) {
            ShowGameModeOptions(((ComboBox)sender).SelectedItem.ToString());
        }

        private void ShowGameModeOptions(string gameModeName) {
            ConfigurationInformationTitles.Text = "Can Checker Beat The Queen" + Environment.NewLine +
                "Can The Queen Move Over More Fields" + Environment.NewLine +
                "Should Checker Make The Best Move First" + Environment.NewLine +
                "Can Checker Make A Reverse Beat" + Environment.NewLine +
                "Color For Top Left Corner" + Environment.NewLine +
                "Board Size";
            GameConfiguration pickedGameModeConfiguration = new GameConfiguration(GameModeConfigurations, gameModeName);
            ConfigurationInformationValues.Text =
                pickedGameModeConfiguration.CanCheckerBeatQueen().ToString() + Environment.NewLine +
                pickedGameModeConfiguration.CanQueenMoveOverMoreFields().ToString() + Environment.NewLine +
                pickedGameModeConfiguration.ShouldCheckerMakeTheBestMoveFirst().ToString() + Environment.NewLine +
                pickedGameModeConfiguration.CanCheckerMakeReverseBeat().ToString() + Environment.NewLine +
                pickedGameModeConfiguration.GetColorForTopLeftCorner().ToString() + Environment.NewLine +
                pickedGameModeConfiguration.GetBoardSize().ToString() + "x" + pickedGameModeConfiguration.GetBoardSize().ToString();
        }

        private void AdjustElements() {
            int BaseLeftPosition = BoardConfiguration.BoardSize * BoardConfiguration.FieldSize + 70;
            Width = BaseLeftPosition + 310;
            Height = BaseLeftPosition + 50;
            NewGame.Left = BaseLeftPosition;
            GameConfigurationsPicker.Left = BaseLeftPosition;
            ConfigurationInformationTitles.Left = BaseLeftPosition;
            ConfigurationInformationValues.Left = BaseLeftPosition + 210;
        }
    }
}
