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
            // Board
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 567);
            this.Controls.Add(this.Winner);
            this.Controls.Add(this.Player);
            this.Name = "Board";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Board_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Player;
        private System.Windows.Forms.Label Winner;
    }
}

