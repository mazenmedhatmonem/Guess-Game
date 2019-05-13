using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Client
{
    public partial class Login : Form
    {
        Socket ClientSocket;
        IPAddress address;
        int intervalFlag;
		static public string UserName { set; get; }
		public Login()
        {
            InitializeComponent();
            intervalFlag = 1;
        }

        /// <summary>
        /// Function to check if user entered his name to call StartConnection()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (txtBoxUserName.Text != "")
            {
                StartConnection();
            }
            else
            {
                txtBoxUserName.Text = "Enter your name";
            }
        }

        /// <summary>
        /// Changes text in greeting in a 1 second interval 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerGreeting_Tick(object sender, EventArgs e)
        {
            if (intervalFlag % 2 != 0)
            {
                label1.Text = "Welcome to Guess The Name!";
                intervalFlag++;
            }
            else
            {
                label1.Text = "W_lcome to Gu_ss Th_ Nam_!";
                intervalFlag++;
            }
        }
        
        /// <summary>
        /// Defines socket and IP infos and start connection with the socket.
        /// Sends users' name to the server and opens Home form
        /// </summary>

        private void StartConnection()
        {
            ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            address = IPAddress.Parse("127.0.0.1");
            try
            {
                ClientSocket.Connect(address, 11000);
                UserName = txtBoxUserName.Text;
                ClientSocket.Send(Encoding.ASCII.GetBytes("name;" + UserName), 0, 5 + UserName.Length, SocketFlags.None);
                Home form1 = new Home(ClientSocket, UserName);
                form1.Show();
                this.Hide();
            }
            catch (SocketException)
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
            timerGreeting.Enabled = false;
        }

        /// <summary>
        /// Invokes StartConnection() if user presses Enter key on his keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBoxUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((char)Keys.Enter == e.KeyChar)
            {
                StartConnection();
            }
        }
    }
}
