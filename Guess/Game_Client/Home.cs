using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Game_Client
{

	public partial class Home : Form
    {
        Socket ClientSocket;
        int EndConnectionFlag;
        string RecievedMsg;
        Thread RecieveThread;
        string MsgToSend;
        string UserName;
		public static string gamer2 = "";
        PopUpMessage PopUpMessage;

        public Home(Socket loginClient, string loginUserName)
        {
            InitializeComponent();
            EndConnectionFlag = 0;
            this.ClientSocket = loginClient;
            this.UserName = loginUserName;
            labelGreeting.Text = "Welcome " + UserName + "!";
            FillOnLoad();
            AvailableRooms();
            RecieveThread = new Thread(RecieveFromServer);
            RecieveThread.Start();
        }
        
        /// <summary>
        /// Awaits to recieve data from server and invokes convenient action
        /// </summary>
        private void RecieveFromServer()
        {
            while (EndConnectionFlag == 0)
            {
                CheckForIllegalCrossThreadCalls = false;
                try
                {
                    byte[] MsgBt = new byte[1024];
                    int size = ClientSocket.Receive(MsgBt);
                    RecievedMsg = Encoding.ASCII.GetString(MsgBt, 0, size);
					string[] RecievedMsgArray = RecievedMsg.Split(';');
                    //Create a room
					if (RecievedMsg.IndexOf("room") > -1)
                    {
						RoomForm roomForm = new RoomForm(ClientSocket, RecievedMsg, listboxCategory.SelectedItem.ToString(), listboxDifficulty.SelectedItem.ToString());
                        Invoke((MethodInvoker)delegate ()
                        {
                            RecieveThread.Suspend();
							this.Hide();
                            roomForm.Show();
                            roomForm.FormClosed += RoomForm_FormClosed;
                        });
                    }
                    //Joined a room
                    if (RecievedMsg.IndexOf("joined") > -1)
                    {
                        PopUpMessage.Close();
                        RoomForm roomForm = new RoomForm(ClientSocket, RecievedMsg, listViewAllRooms.SelectedItems[0].SubItems[1].Text, listViewAllRooms.SelectedItems[0].SubItems[2].Text);
                        Invoke((MethodInvoker)delegate ()
                        {
							gamer2 = RecievedMsgArray[2];
							RecieveThread.Suspend();
							this.Hide();
							roomForm.Show();
                            roomForm.FormClosed += RoomForm_FormClosed;
                        });
                    }
                    //Watch a game
                    if (RecievedMsg.IndexOf("Watch_game") > -1)
                    {
                        RoomForm roomForm = new RoomForm(ClientSocket, RecievedMsg, listViewAllRooms.SelectedItems[0].SubItems[1].Text, listViewAllRooms.SelectedItems[0].SubItems[2].Text);
                        Invoke((MethodInvoker)delegate ()
                        {
                            RecieveThread.Suspend();
							this.Hide();
							roomForm.Show();
                            roomForm.FormClosed += RoomForm_FormClosed;
                        });
                    }
                    //Show available rooms
                    if (RecievedMsg.IndexOf("refresh") > -1)
                    {
						string AllRoomsSerialized = RecievedMsg.Split(';')[1];
						ShowRooms(AllRoomsSerialized);
                    }
                    //Pressed wrong choice (Join/Watch)
                    if (RecievedMsg.IndexOf("sorry") > -1)
                    {
                        PopUpMessage.Close();
                        PopUpMessage=null;
                        PopUpMessage = new PopUpMessage("Sorry. This room is not available now.");
                        PopUpMessage.DisableButtons();
                        PopUpMessage.ShowDialog();
                        AvailableRooms();
                    }
                    //Owner refused the Join request
                    if (RecievedMsg.IndexOf("refused") > -1)
                    {
                        PopUpMessage.Close();
                        PopUpMessage = null;
                        PopUpMessage = new PopUpMessage("Sorry. Your request was declined.");
                        PopUpMessage.DisableButtons();
                        PopUpMessage.ShowDialog();
                        AvailableRooms();
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

        /// <summary>
        /// Resumes RecieveThread after a room was closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoomForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RecieveThread.Resume();
			this.Show();
        }

        /// <summary>
        /// Terminates RecieveThread and ends connection with server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
			// close connection
            EndConnectionFlag = 1;
            ClientSocket.Send(Encoding.ASCII.GetBytes("<EOF>"), 0, 5, SocketFlags.None);
        }

        /// <summary>
        /// Sends chosen Category and Difficulty to the server to create a room
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateRoom_Click(object sender, EventArgs e)
        {
			MsgToSend = "room" + ";" + listboxCategory.SelectedItem + ";" + listboxDifficulty.SelectedItem;
            ClientSocket.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
        }

        /// <summary>
        /// Sends to the server if the player wants to join or watch a game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewAllRooms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAllRooms.SelectedItems.Count > 0)
            {
                string player2_state = listViewAllRooms.SelectedItems[0].SubItems[4].Text;
                if (player2_state == "")
                {
					MsgToSend = "join;" + listViewAllRooms.SelectedItems[0].SubItems[0].Name;
                    PopUpMessage = new PopUpMessage("Waiting for response...");
                    PopUpMessage.DisableButtons();
                    PopUpMessage.Show();
                    ClientSocket.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                }
                else
                {
					MsgToSend = "watch;" + listViewAllRooms.SelectedItems[0].SubItems[0].Name;
                    ClientSocket.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
                }
            }
        }
        
        /// <summary>
        /// Displays all rooms with details
        /// </summary>
        /// <param name="AllRoomsSerialized"></param>
		private void ShowRooms(string AllRoomsSerialized)
		{
			listViewAllRooms.Items.Clear();
			List<Room> ListOfRooms = AllRoomsSerialized.DeSerialize<List<Room>>();
			foreach (Room Room in ListOfRooms)
			{
                ListViewItem ListItem;
				ListViewItem.ListViewSubItem[] SubItems;
				if (Room.Player2Name == "")
				{
                    ListItem = new ListViewItem("Join");
                    ListItem.Name = Room.ID.ToString();
                    SubItems = new ListViewItem.ListViewSubItem[]
						{new ListViewItem.ListViewSubItem(ListItem,Room.Category),
                            new ListViewItem.ListViewSubItem(ListItem,Room.Difficulty),
					 new ListViewItem.ListViewSubItem(ListItem,
						Room.Player1Name),
					new ListViewItem.ListViewSubItem(ListItem,
						"")};
				}
				else
				{
                    ListItem = new ListViewItem("Watch");
                    ListItem.Name = Room.ID.ToString();
                    SubItems = new ListViewItem.ListViewSubItem[]
					{new ListViewItem.ListViewSubItem(ListItem,Room.Category),
                        new ListViewItem.ListViewSubItem(ListItem,Room.Difficulty),
					 new ListViewItem.ListViewSubItem(ListItem,
						Room.Player1Name),
					new ListViewItem.ListViewSubItem(ListItem,
						Room.Player2Name)};
				}
				ListItem.SubItems.AddRange(SubItems);
				listViewAllRooms.Items.Add(ListItem);
			}
		}

        /// <summary>
        /// Called when the player wants the last updated rooms list from the server
        /// </summary>
        private void AvailableRooms()
        {
            MsgToSend = "rooms;";
            ClientSocket.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);            
        }

        /// <summary>
        /// Fills the list boxes with covenient data from the database when the Home form is loaded
        /// </summary>
        private void FillOnLoad()
        {
            listboxDifficulty.Items.Add("Easy");
            listboxDifficulty.Items.Add("Medium");
            listboxDifficulty.Items.Add("Hard");
			listboxDifficulty.SelectedIndex = 0;
            MsgToSend = "cat;";
            ClientSocket.Send(Encoding.ASCII.GetBytes(MsgToSend), 0, MsgToSend.Length, SocketFlags.None);
            byte[] MsgBt = new byte[1024 * 1024 * 50];
            int size = ClientSocket.Receive(MsgBt);
            RecievedMsg = Encoding.ASCII.GetString(MsgBt, 0, size);
            string[] CategoriesArray = RecievedMsg.DeSerialize<string[]>();

            for (int i = 0; i < CategoriesArray.Length; i++)
            {
                ComboBoxItem CategoryItem = new ComboBoxItem()
                {
                    Text = CategoriesArray[i]
                };
				listboxCategory.Items.Add(CategoryItem);
            }
			listboxCategory.SelectedIndex = 0;
        }

        /// <summary>
        /// Called when the player wants the last updated rooms list from the server. Invokes AvailableRooms().
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
			AvailableRooms();
        }
    }
}
