using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Game_Server
{
	partial class Server
	{
		IPEndPoint localEndPoint;
		List<Room> rooms = new List<Room>();
		Dictionary<Player, Socket> Connections = new Dictionary<Player, Socket>();
		Dictionary<Player, Queue<Player>> Requests = new Dictionary<Player, Queue<Player>>();
		// Thread signal.
		public ManualResetEvent allDone = new ManualResetEvent(false);
		public Server(IPEndPoint LocalEndPoint)
		{
			localEndPoint = LocalEndPoint;
		}
		private void StartListening()
		{
            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// Bind the socket to the local endpoint and listen for incoming connections.
			try
			{
				listener.Bind(localEndPoint);
				listener.Listen(100);
				while (true)
				{
					// Set the event to nonsignaled state.
					allDone.Reset();
					// Start an asynchronous socket to listen for connections.
					Console.WriteLine("Waiting for a connection...");
					listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
					// Wait until a connection is made before continuing.
					allDone.WaitOne();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			Console.WriteLine("\nPress ENTER to continue...");
			Console.Read();
		}

		private void AcceptCallback(IAsyncResult ar)
		{
			// Signal the main thread to continue.
			allDone.Set();

			// Get the socket that handles the client request.
			Socket listener = (Socket)ar.AsyncState;
			Socket handler = listener.EndAccept(ar);

			// Create the player object.
			Player player = new Player();

			//player.WorkSocket = handler;
			Connections.Add(player, handler);
			
			handler.BeginReceive(player.Buffer, 0, Player.BufferSize, 0,
			new AsyncCallback(ReadCallback), player);
		}


		private void ReadCallback(IAsyncResult ar)
		{
			String content = String.Empty;

			// Retrieve the player object and the handler socket
			// from the asynchronous player object.
			Player player = (Player)ar.AsyncState;
			Socket handler = Connections[player];

			// Read data from the player socket. 
			int bytesRead = handler.EndReceive(ar);

			if (bytesRead > 0)
			{
				// Clear previous player data sent.
				player.Data.Clear();
				// There  might be more data, so store the data received so far.
				player.Data.Append(Encoding.ASCII.GetString(player.Buffer, 0, bytesRead));
				// Check for end-of-file tag. If it is not there, read 
				// more data.
				content = player.Data.ToString();
                string[] ContentSplited = content.Split(';');
				Console.WriteLine("Read {0} bytes from socket. \n Data : {1}", content.Length, content);
				switch (ContentSplited[0])
				{
					case "name": // Add player name.
						SetPlayerName(player, ContentSplited[1]);
						break;
					case "rooms": // Send rooms list to client
						Send(Connections[player], "refresh;" + rooms.Serialize());
						break;
					case "cat": // Send category list to client
						Send(Connections[player], CategoryString);
						break;
					case "room": // Create room request.
						CreateRoom(player, ContentSplited[1], ContentSplited[2]);
						break;
					case "start": // Start game.
						SendWord(int.Parse(ContentSplited[1]));
						break;
					case "accept": // accept player request
						JoinRoom(int.Parse(ContentSplited[1]), Requests[player].Dequeue());
						break;
					case "refuse":
						Refuse(int.Parse(ContentSplited[1]));
						break;
					case "game": // Playing game.
						Room r = Room.Search(int.Parse(ContentSplited[1]), rooms);
						if (r.Player1.State == State.Playing)
						{
							SwitchState(r.Player1, r.Player2, ContentSplited);
						}
						else
						{
							SwitchState(r.Player2, r.Player1, ContentSplited);
						}
						break;
					case "join": // Join room Request.
						int roomid = int.Parse(ContentSplited[1]);
						JoinRequest(roomid, player);
						break;
					case "watch": // Watch a room.
						roomid = int.Parse(ContentSplited[1]);
						WatchRequest(roomid, player);
						break;
					case "rematch": // Play again.
						roomid = int.Parse(ContentSplited[1]);
						Rematch(roomid, player);
						break;
					case "quit": // Leave the room.
						roomid = int.Parse(ContentSplited[1]);
						Quit(roomid, player);
						break;
					case "leave":
						roomid = int.Parse(ContentSplited[1]);
						r = Room.Search(roomid, rooms);
						if (r != null)
						{
							r.Watchers.Remove(player);
						}						
						break;
				}
				if (content.IndexOf("<EOF>") > -1)
				{
					// All the data has been read from the 
					// client. Display it on the console.
					handler.Shutdown(SocketShutdown.Both);
					handler.Close();
					Connections.Remove(player);
				}
				else
				{
					// Not all data received. Get more.
					handler.BeginReceive(player.Buffer, 0, Player.BufferSize, 0, new AsyncCallback(ReadCallback), player);
				}
			}
		}

		private void Send(Socket handler, String data)
		{
			// Convert the string data to byte data using ASCII encoding.
			byte[] byteData = Encoding.ASCII.GetBytes(data);

			// Begin sending the data to the remote device.
			handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
		}

		private void SendCallback(IAsyncResult ar)
		{
			try
			{
				// Retrieve the socket from the state object.
				Socket handler = (Socket)ar.AsyncState;

				// Complete sending the data to the remote device.
				int bytesSent = handler.EndSend(ar);
				Console.WriteLine("Sent {0} bytes to client.", bytesSent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		public void Init()
		{
			Category();
			StartListening();
		}
	}
}
