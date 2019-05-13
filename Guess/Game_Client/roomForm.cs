using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Game_Client
{
    public enum State {Playing,Idle,Watching,Waiting,Winner,Loser}
    public partial class RoomForm : Form
    {
        Socket SocketClient;
        int EndConnectionFlag;
        int RoomID;
        string MsgToSend;
        string RecievedMsg;
        string WordToGuess;
        char[] WordToGuessChar;
		char[] WordToGuessSymbols;
        string Player2Name;
        State State;
        Thread RecieveThread;

		/// <summary>
		/// check state of the player
		/// </summary>
		/// <param name="temp_state"></param>
		public void CheckState(String temp_state)
        {
            switch (temp_state)
            {
                case "Playing":
                    State = State.Playing;
                    break;
                case "Idle":
                    State = State.Idle;
                    break;
                case "Watching":
                    State = State.Watching;
                    break;
                case "Waiting":
                    State = State.Waiting;
                    break;
                case "Winner":
                    State = State.Winner;
                    break;
                case "Loser":
                    State = State.Loser;
                    break;
            }
        }
		/// <summary>
		/// construtor for the form to recieve the word from server and show words and symbols and manage watcher and players states
		/// </summary>
		/// <param name="client"></param>
		/// <param name="msg"></param>
		/// <param name="category"></param>
		/// <param name="difficulty"></param>
		public RoomForm(Socket client, string msg, string category, string difficulty)
        {
            InitializeComponent();
			lblCategory.Text = category;
			lblDifficulty.Text = difficulty;
            RecieveThread = new Thread(RecieveFromServer);
            RecieveThread.Start();
            RecievedMsg = msg;
            string[] RecievedMsgArr = RecievedMsg.Split(';');
            EndConnectionFlag = 0; 
            try
            {
                SocketClient = client;
                RoomID = int.Parse(RecievedMsgArr[1]);
                if ((RecievedMsgArr.Length>2) && (RecievedMsgArr[2] == "Watching"))
                {
                    btnStartGame.Hide();
					labelPlayer1.Text = "Player 1";
                    State = State.Watching;
					WordToGuess = RecievedMsgArr[7];
                    grpBoxKeyBoard.Enabled = true;
                    if(WordToGuess.IndexOf("_")==-1&&(RecievedMsgArr[4]!="Winner"||RecievedMsgArr[4]!="Loser"))
                    {
                        ShowSymbols();
                    }
                    else
                    {
                        WordToGuessSymbols = WordToGuess.ToCharArray();
                        ShowWord();
                    }
					labelGamer1Name.Text = RecievedMsgArr[3];
                    labelGamer2Name.Text = RecievedMsgArr[5];
                    if(RecievedMsgArr[4]=="Playing")
                    {
                        labelGamer2Name.BackColor = Color.PaleGreen;
                        labelGamer1Name.BackColor = Color.Tomato;
                    }
                    else
                    {
                        labelGamer1Name.BackColor = Color.PaleGreen;
                        labelGamer2Name.BackColor = Color.Tomato;
                    }
                }
                else
                {
					labelWordToGuess.Text = "Waiting for second player...";
				}
                DisplayButtons();
            }
            catch (SocketException )
            {
                MessageBox.Show("Connetion time out.");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Invalid operation.");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Argument exception.");
            }
        }

		/// <summary>
		/// recieve messages from server to manage the game if player wants to play with the owner and handle cases when game finished if players want to play again together 
		/// </summary>
		private void RecieveFromServer()
        {
            while (EndConnectionFlag == 0)
            {
                CheckForIllegalCrossThreadCalls = false;
                try
                {
                    byte[] MsgBT = new byte[1024];
                    int size = SocketClient.Receive(MsgBT);
                    RecievedMsg = Encoding.ASCII.GetString(MsgBT, 0, size);
                    string[] RecievedMsgArr = RecievedMsg.Split(';');
					//someone wants to join
					if (RecievedMsgArr[0] == "request")
                    {
                        Player2Name = RecievedMsgArr[1];
                        string RequestMsg = Player2Name + " wants to play with you.";
                        PopUpMessage request = new PopUpMessage(RequestMsg);
                        request.ShowDialog();
						//accept 
						if (request.DialogResult == DialogResult.OK)
                        {
							labelGamer2Name.Text = RecievedMsgArr[1];
                            btnStartGame.Show();
                            MsgToSend = "accept;" + RoomID;
                            SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                        }
						else //refuse
						{
                            MsgToSend = "refuse;" + RoomID;
                            SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                        }
                    }
					// start the game
					if (RecievedMsgArr[0] == "start")
                    {
                        WordToGuess = RecievedMsgArr[1];
                        if(RecievedMsgArr[2] == "Playing")
                        {
                            State = State.Playing;
                        }
						else if (RecievedMsgArr[2] == "Waiting")
						{
                            State = State.Waiting;
						}
                        grpBoxKeyBoard.Enabled = true;
                        EnableButtons();
                        grpBoxKeyBoard.Enabled = false;
                        ShowSymbols();
                    }
					// game
					if (RecievedMsgArr[0] == "game")
                    {
						CheckState(RecievedMsgArr[2]);
						DisableButton(RecievedMsgArr[3][0]);
                        WordToGuessSymbols = RecievedMsgArr[4].ToCharArray();
                        ShowWord();
                        if (State == State.Loser)
                        {
                            PopUpMessage Message = new PopUpMessage("Hard Luck. You lost. Do you want to play again?");
                            Message.ShowDialog();

                            if (Message.DialogResult == DialogResult.OK)
                            {
                                MsgToSend = "rematch;" + RoomID;
                                SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                                labelWinner.Text = "";
                                labelGamer2Name.Text = "";
                            }
                            else
                            {
                                MsgToSend = "quit;" + RoomID;
                                SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                                this.FormClosing -= new FormClosingEventHandler(RoomForm_FormClosing);
                                EndConnectionFlag = 1;
                                this.Close();
                            }
                        }
                    }
					// play again
					if (RecievedMsgArr[0] == "rematch")
                    {
                        if (RecievedMsgArr.Length == 3)
                        {
                            btnStartGame.Show();
                        }
                        labelGamer2Name.Text = RecievedMsgArr[1];
                    }
					// watcher
					if (RecievedMsg.IndexOf("watch") > -1)
                    {
						labelWinner.Text = "";
						WordToGuessSymbols = RecievedMsgArr[2].ToCharArray();
                        ShowWord();
                        DisableButton(RecievedMsgArr[3][0]);
                        if (RecievedMsgArr[1] == "Playing")
                        {
                            labelGamer1Name.BackColor = Color.PaleGreen;
                            labelGamer2Name.BackColor = Color.Tomato;
                        }
                        else
                        {
                            labelGamer2Name.BackColor = Color.PaleGreen;
                            labelGamer1Name.BackColor = Color.Tomato;
                        }
                    }
					// player left the room
					if (RecievedMsgArr[0] == "left")
                    {
                        labelWordToGuess.Text = "";
                        grpBoxKeyBoard.Enabled = false;
                        PopUpMessage Message = new PopUpMessage(labelGamer2Name.Text + " left the room. Do you want to play again?");
                        Message.ShowDialog();
						// play again with other player
						if (Message.DialogResult == DialogResult.OK)
                        {
                            MsgToSend = "rematch;" + RoomID;
                            SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                            labelWinner.Text = "";
                            labelGamer2Name.Text = "";
                        }
						else // leave the room
						{
                            MsgToSend = "quit;" + RoomID;
                            SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                            this.FormClosing -= new FormClosingEventHandler(RoomForm_FormClosing);
                            EndConnectionFlag = 1;
                            this.Close();
                        }
                    }
                }
                catch (EndOfStreamException)
                {
                    MessageBox.Show("Connection ended. Server is down.");
                    EndConnectionFlag = 1;
                }
                catch (IOException)
                {
                    MessageBox.Show("Connection ended. Server is down.");
                    EndConnectionFlag = 1;
                }
            }
        }
		//end connection
		private void Room_FormClosing(object sender, FormClosingEventArgs e)
        {
            EndConnectionFlag = 1;
        }
		/// <summary>
		/// display the buttons on the groupbox with the colours and background
		/// </summary>
		private void DisplayButtons()
        {
			string[] KeyBoardLetters = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            Point ButtonLocation = new Point();
            int yAxes = 0;
            for (int i = 0; i < 26; i++)
            {
                Button LetterButton = new Button()
                {
                    BackColor = Color.Indigo,
                    ForeColor = Color.LavenderBlush,
                    FlatStyle = FlatStyle.Flat,
                    
				};
				Font ButtonFont = new Font("Kristen ITC",18) ;
                
                LetterButton.Font = ButtonFont;
                if (State != State.Watching)
                {
                    LetterButton.Click += LetterButtonClick;
                }
                LetterButton.Size = new Size(50, 50);
                grpBoxKeyBoard.Controls.Add(LetterButton);
               
                if (i < 13)
                {
                    ButtonLocation.X = 30 + (i * 60);
                    ButtonLocation.Y = 20;
                    LetterButton.Text = KeyBoardLetters[i];
                }
                else
                {
                    ButtonLocation.X = 30 + (yAxes * 60);
                    ButtonLocation.Y = 100;
                    yAxes++;
                    LetterButton.Text = KeyBoardLetters[i];
                }
                 LetterButton.Location = ButtonLocation;
            }
        }
		//call game func when button clicked
		private void LetterButtonClick(object sender, EventArgs e)
        {
            Button LetterButton = (Button)sender;
            LetterButton.Enabled = false;
            Game(LetterButton.Text.ToLower()[0]);
        }

		/// <summary>
		/// show _ symbol with word length
		/// </summary>
		private void ShowSymbols()
        {
            WordToGuessChar = WordToGuess.ToCharArray();
            WordToGuessSymbols = new char[WordToGuessChar.Length];
            for (int i = 0; i<WordToGuessChar.Length;i++)
            {
                if (WordToGuessChar[i] != '-')
                {
                    WordToGuessSymbols[i] = '_';
                }
                else
                {
                    WordToGuessSymbols[i] = '-';
                }
            }
            ShowWord();
        }

        private void ShowWord()
        {
            if (State == State.Waiting)
            {
				grpBoxKeyBoard.Enabled = false;
				labelGamer1Name.BackColor = Color.Tomato;
				labelGamer2Name.BackColor = Color.PaleGreen;
			}
            if(State==State.Playing)
            {
				grpBoxKeyBoard.Enabled = true;
				labelGamer1Name.BackColor = Color.PaleGreen;
				labelGamer2Name.BackColor = Color.Tomato;
			}

			int EndGameFlag = 0;
            labelWordToGuess.Text = " ";
            for (int i =0;i<WordToGuessSymbols.Length;i++)
            {
                if (WordToGuessSymbols[i]=='-')
                {
                    labelWordToGuess.Text += "   ";
                }
                else
				{
                    labelWordToGuess.Text += (WordToGuessSymbols[i] + " ");
					if (WordToGuessSymbols[i] == '_')
					{
						EndGameFlag++;
					}
                }
            }
			// display winner name when game finished
			if (EndGameFlag == 0)
			{
				if (labelGamer1Name.BackColor == Color.Tomato)
				{
					labelWinner.Text = "WINNER "+ labelGamer2Name.Text;
				}
				else
				{
					labelWinner.Text = "WINNER " + labelGamer1Name.Text;
				}
			}
        }
		/// <summary>
		/// send start message to server when button clicked
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnStartGame_Click(object sender, EventArgs e)
        {
			btnStartGame.Hide();
			grpBoxKeyBoard.Enabled = true;
            MsgToSend = "start;" + RoomID ;
            SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
        }
		/// <summary>
		/// display the letter if player pressed the right one 
		/// </summary>
		/// <param name="ClickedLetter"></param>
		private void Game (char ClickedLetter)
        {
            State=State.Waiting;
            int WinnerFlag = 0;
            for(int i=0;i<WordToGuessChar.Length;i++)
            {
                if(WordToGuessChar[i]==ClickedLetter)
                {
                    WordToGuessSymbols[i] = ClickedLetter;
                    State = State.Playing;
                }
                if(WordToGuessSymbols[i]== '_')
                {
                    WinnerFlag++;
                }
            }
            if (WinnerFlag == 0)
            {
                State = State.Winner;
            }
            
            ShowWord();
            string ChangedWordToGuess = "";
            for (int z = 0; z < WordToGuessSymbols.Length; z++)
            {
				ChangedWordToGuess += WordToGuessSymbols[z];
            }
            MsgToSend = "game"+";"+RoomID + ";" + State.ToString() + ";" + ClickedLetter + ";" + ChangedWordToGuess;

            SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
            if (WinnerFlag == 0)
            {
				// display winner and ask if wants to play again
				PopUpMessage CongratMsg = new PopUpMessage("Congratulations! You won! Do you want to play again?");
                CongratMsg.ShowDialog();
				//accept playing again
				if (CongratMsg.DialogResult == DialogResult.OK)
                {
                    MsgToSend = "rematch;" + RoomID;
                    SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
					labelWinner.Text = "";
                    labelGamer2Name.Text = "";
                }
				else // refuse playing again
				{
                    MsgToSend = "quit;" + RoomID;
                    SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
					this.FormClosing -= new FormClosingEventHandler(RoomForm_FormClosing);
					EndConnectionFlag = 1;
                    this.Close();
                }
            }
        }
		// disable letter after it is clicked
		private void DisableButton(char ClickedLetter)
		{
			foreach (Button LetterButton in grpBoxKeyBoard.Controls)
			{
				if (LetterButton.Text == ClickedLetter.ToString().ToUpper())
				{
					LetterButton.Enabled = false;
				}
			}
		}
		// enable the groupbox buttons
		private void EnableButtons()
        {
            foreach (Button LetterButton in grpBoxKeyBoard.Controls)
            {
                LetterButton.Enabled = true;
            }
        }
		//show player names in form
		private void RoomForm_Load(object sender, EventArgs e)
        {
            if (State != State.Watching)
            {
                btnStartGame.Hide();
                labelGamer1Name.Text = Login.UserName;
                labelGamer2Name.Text = Home.gamer2;
            }
		}
		//close the room 
		private void RoomForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			EndConnectionFlag = 1;
			if (State == State.Watching)
			{
				MsgToSend = "leave;" + RoomID;
				SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
			}
			else
			{
				MsgToSend = "quit;" + RoomID;
				SocketClient.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
			}
		}
	}
}
