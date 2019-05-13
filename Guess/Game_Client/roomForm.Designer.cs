namespace Game_Client
{
    partial class RoomForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoomForm));
			this.labelWordToGuess = new System.Windows.Forms.Label();
			this.btnStartGame = new System.Windows.Forms.Button();
			this.grpBoxKeyBoard = new System.Windows.Forms.GroupBox();
			this.labelGamer1Name = new System.Windows.Forms.Label();
			this.labelGamer2Name = new System.Windows.Forms.Label();
			this.labelPlayer1 = new System.Windows.Forms.Label();
			this.labelPlayer2 = new System.Windows.Forms.Label();
			this.labelWinner = new System.Windows.Forms.Label();
			this.lblCategory = new System.Windows.Forms.Label();
			this.lblDifficulty = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelWordToGuess
			// 
			this.labelWordToGuess.AutoSize = true;
			this.labelWordToGuess.Font = new System.Drawing.Font("Kristen ITC", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWordToGuess.Location = new System.Drawing.Point(186, 163);
			this.labelWordToGuess.Name = "labelWordToGuess";
			this.labelWordToGuess.Size = new System.Drawing.Size(102, 40);
			this.labelWordToGuess.TabIndex = 1;
			this.labelWordToGuess.Text = "label1";
			// 
			// btnStartGame
			// 
			this.btnStartGame.BackColor = System.Drawing.Color.Indigo;
			this.btnStartGame.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
			this.btnStartGame.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSlateBlue;
			this.btnStartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnStartGame.Font = new System.Drawing.Font("Kristen ITC", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStartGame.ForeColor = System.Drawing.Color.LavenderBlush;
			this.btnStartGame.Location = new System.Drawing.Point(12, 145);
			this.btnStartGame.Name = "btnStartGame";
			this.btnStartGame.Size = new System.Drawing.Size(128, 80);
			this.btnStartGame.TabIndex = 4;
			this.btnStartGame.Text = "Start Game";
			this.btnStartGame.UseVisualStyleBackColor = false;
			this.btnStartGame.Click += new System.EventHandler(this.BtnStartGame_Click);
			// 
			// grpBoxKeyBoard
			// 
			this.grpBoxKeyBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.grpBoxKeyBoard.Enabled = false;
			this.grpBoxKeyBoard.Location = new System.Drawing.Point(12, 297);
			this.grpBoxKeyBoard.Name = "grpBoxKeyBoard";
			this.grpBoxKeyBoard.Size = new System.Drawing.Size(810, 161);
			this.grpBoxKeyBoard.TabIndex = 5;
			this.grpBoxKeyBoard.TabStop = false;
			// 
			// labelGamer1Name
			// 
			this.labelGamer1Name.AutoSize = true;
			this.labelGamer1Name.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelGamer1Name.Location = new System.Drawing.Point(188, 43);
			this.labelGamer1Name.Name = "labelGamer1Name";
			this.labelGamer1Name.Size = new System.Drawing.Size(0, 27);
			this.labelGamer1Name.TabIndex = 6;
			// 
			// labelGamer2Name
			// 
			this.labelGamer2Name.AutoSize = true;
			this.labelGamer2Name.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelGamer2Name.Location = new System.Drawing.Point(655, 43);
			this.labelGamer2Name.Name = "labelGamer2Name";
			this.labelGamer2Name.Size = new System.Drawing.Size(0, 27);
			this.labelGamer2Name.TabIndex = 7;
			// 
			// labelPlayer1
			// 
			this.labelPlayer1.AutoSize = true;
			this.labelPlayer1.BackColor = System.Drawing.Color.Transparent;
			this.labelPlayer1.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayer1.Location = new System.Drawing.Point(64, 43);
			this.labelPlayer1.Name = "labelPlayer1";
			this.labelPlayer1.Size = new System.Drawing.Size(73, 27);
			this.labelPlayer1.TabIndex = 8;
			this.labelPlayer1.Text = " You :";
			// 
			// labelPlayer2
			// 
			this.labelPlayer2.AutoSize = true;
			this.labelPlayer2.BackColor = System.Drawing.Color.Transparent;
			this.labelPlayer2.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelPlayer2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.labelPlayer2.Location = new System.Drawing.Point(544, 43);
			this.labelPlayer2.Name = "labelPlayer2";
			this.labelPlayer2.Size = new System.Drawing.Size(105, 27);
			this.labelPlayer2.TabIndex = 9;
			this.labelPlayer2.Text = "Player 2 :";
			// 
			// labelWinner
			// 
			this.labelWinner.AutoSize = true;
			this.labelWinner.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelWinner.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.labelWinner.Location = new System.Drawing.Point(313, 92);
			this.labelWinner.Name = "labelWinner";
			this.labelWinner.Size = new System.Drawing.Size(0, 27);
			this.labelWinner.TabIndex = 10;
			// 
			// lblCategory
			// 
			this.lblCategory.AutoSize = true;
			this.lblCategory.BackColor = System.Drawing.Color.Transparent;
			this.lblCategory.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCategory.Location = new System.Drawing.Point(290, 9);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(0, 27);
			this.lblCategory.TabIndex = 11;
			// 
			// lblDifficulty
			// 
			this.lblDifficulty.AutoSize = true;
			this.lblDifficulty.BackColor = System.Drawing.Color.Transparent;
			this.lblDifficulty.Font = new System.Drawing.Font("Kristen ITC", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDifficulty.Location = new System.Drawing.Point(411, 9);
			this.lblDifficulty.Name = "lblDifficulty";
			this.lblDifficulty.Size = new System.Drawing.Size(0, 27);
			this.lblDifficulty.TabIndex = 12;
			// 
			// RoomForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkViolet;
			this.ClientSize = new System.Drawing.Size(833, 467);
			this.Controls.Add(this.lblDifficulty);
			this.Controls.Add(this.lblCategory);
			this.Controls.Add(this.labelWinner);
			this.Controls.Add(this.labelPlayer2);
			this.Controls.Add(this.labelPlayer1);
			this.Controls.Add(this.labelGamer2Name);
			this.Controls.Add(this.labelGamer1Name);
			this.Controls.Add(this.grpBoxKeyBoard);
			this.Controls.Add(this.btnStartGame);
			this.Controls.Add(this.labelWordToGuess);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RoomForm";
			this.Text = "Guess the Name";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RoomForm_FormClosing);
			this.Load += new System.EventHandler(this.RoomForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelWordToGuess;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.GroupBox grpBoxKeyBoard;
        private System.Windows.Forms.Label labelGamer1Name;
        private System.Windows.Forms.Label labelGamer2Name;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
		private System.Windows.Forms.Label labelWinner;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Label lblDifficulty;
	}
}