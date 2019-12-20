namespace Checkers
{
    partial class Board
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.Player = new System.Windows.Forms.Label();
            this.Winner = new System.Windows.Forms.Label();
            this.NewGame = new System.Windows.Forms.Button();
            this.GameConfigurationsPicker = new System.Windows.Forms.ComboBox();
            this.ConfigurationInformationTitles = new System.Windows.Forms.Label();
            this.ConfigurationInformationValues = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.Font = new System.Drawing.Font("Tw Cen MT Condensed", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Player.Location = new System.Drawing.Point(1, 8);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(664, 50);
            this.Player.TabIndex = 1;
            this.Player.Text = "Player";
            this.Player.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Winner
            // 
            this.Winner.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Winner.Font = new System.Drawing.Font("Tw Cen MT Condensed", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Winner.Location = new System.Drawing.Point(1, 245);
            this.Winner.Name = "Winner";
            this.Winner.Size = new System.Drawing.Size(664, 59);
            this.Winner.TabIndex = 2;
            this.Winner.Text = "Winner";
            this.Winner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewGame
            // 
            this.NewGame.Location = new System.Drawing.Point(715, 100);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(343, 42);
            this.NewGame.TabIndex = 3;
            this.NewGame.Text = "Start a new game";
            this.NewGame.UseVisualStyleBackColor = true;
            this.NewGame.Click += new System.EventHandler(this.NewGame_Click);
            // 
            // GameConfigurationsPicker
            // 
            this.GameConfigurationsPicker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GameConfigurationsPicker.FormattingEnabled = true;
            this.GameConfigurationsPicker.Location = new System.Drawing.Point(715, 63);
            this.GameConfigurationsPicker.Name = "GameConfigurationsPicker";
            this.GameConfigurationsPicker.Size = new System.Drawing.Size(343, 24);
            this.GameConfigurationsPicker.TabIndex = 4;
            // 
            // ConfigurationInformationTitles
            // 
            this.ConfigurationInformationTitles.AutoSize = true;
            this.ConfigurationInformationTitles.Location = new System.Drawing.Point(715, 153);
            this.ConfigurationInformationTitles.Name = "ConfigurationInformationTitles";
            this.ConfigurationInformationTitles.Size = new System.Drawing.Size(196, 17);
            this.ConfigurationInformationTitles.TabIndex = 5;
            this.ConfigurationInformationTitles.Text = "ConfigurationInformationTitles";
            // 
            // ConfigurationInformationValues
            // 
            this.ConfigurationInformationValues.AutoSize = true;
            this.ConfigurationInformationValues.Location = new System.Drawing.Point(917, 153);
            this.ConfigurationInformationValues.Name = "ConfigurationInformationValues";
            this.ConfigurationInformationValues.Size = new System.Drawing.Size(205, 17);
            this.ConfigurationInformationValues.TabIndex = 6;
            this.ConfigurationInformationValues.Text = "ConfigurationInformationValues";
            // 
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 867);
            this.Controls.Add(this.ConfigurationInformationValues);
            this.Controls.Add(this.ConfigurationInformationTitles);
            this.Controls.Add(this.GameConfigurationsPicker);
            this.Controls.Add(this.NewGame);
            this.Controls.Add(this.Winner);
            this.Controls.Add(this.Player);
            this.MaximizeBox = false;
            this.Name = "Board";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Checkers by Piotr Pasich";
            this.Load += new System.EventHandler(this.Board_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Player;
        private System.Windows.Forms.Label Winner;
        private System.Windows.Forms.Button NewGame;
        private System.Windows.Forms.ComboBox GameConfigurationsPicker;
        private System.Windows.Forms.Label ConfigurationInformationTitles;
        private System.Windows.Forms.Label ConfigurationInformationValues;
    }
}

