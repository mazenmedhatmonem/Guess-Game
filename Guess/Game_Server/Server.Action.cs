using System.IO;
using System.Text;

namespace Game_Server
{
	partial class Server
	{
		/// <summary>
		/// Set player name.
		/// </summary>
		/// <param name="player">Game player</param>
		/// <param name="name">The name</param>
		private void SetPlayerName(Player player, string name)
		{
			player.Name = name;
        }

		/// <summary>
		/// Create room and set it's owner.
		/// </summary>
		/// <param name="owner">Room owner</param>
		/// <param name="Category">Room Category</param>
		/// <param name="Difficulty">Room Difficulty</param>
		/// <returns>created room ID</returns>
		private int CreateRoom(Player owner, string Category, string Difficulty)
		{
			Room room = new Room(owner);
			rooms.Add(room);
			room.Player1 = owner;
            room.Category = Category;
            switch (Difficulty)
            {
                case "Easy":
                    room.Difficulty = Game_Server.Difficulty.Easy;
                   break;
                case "Medium":
                    room.Difficulty = Game_Server.Difficulty.Medium;
                    break;
                case "Hard":
                    room.Difficulty = Game_Server.Difficulty.Hard;
                    break;
            }
            owner.State = State.Waiting;
			// Send the data to the client.
			Send(Connections[room.Player1], "room;" + room.ID);
			return room.ID;
		}

		/// <summary>
		/// Send word to players
		/// </summary>
		/// <param name="roomID">Room ID</param>
		private void SendWord(int roomID)
        {
            Room r = Room.Search(roomID, rooms);
			r.Word = SelectWord(r.Difficulty.ToString(), r.Category);
			if (r!= null)
            {
                r.Player1.State = State.Playing;
				Send(Connections[r.Player1], "start" + ";" + r.Word + ";" + r.Player1.State.ToString());
				Send(Connections[r.Player2], "start" + ";" + r.Word + ";" + r.Player2.State.ToString());
            }
        }

		/// <summary>
		/// Playing game.
		/// </summary>
		/// <param name="roomID">Room ID</param>
		/// <param name="player">Player who has turn</param>
		/// <param name="content">Content send to aonther player</param>       
		private void Game(int roomID, Player player, string content)
		{
			foreach (Room item in rooms)
			{
				if (item.ID == roomID)
				{
					if (player == item.Player1)
					{
						Send(Connections[item.Player2], content);
					}
					else
					{
						Send(Connections[item.Player1], content);
					}
				}
			}
		}

		/// <summary>
		/// Switch players state
		/// </summary>
		/// <param name="player1">Room player 1</param>
		/// <param name="player2">Room player 2</param>
		/// <param name="content">Content</param>
		private void SwitchState(Player player1,Player player2,string[] content)
        {
            int roomid = int.Parse(content[1]);
            Room r = Room.Search(roomid, rooms);
            r.Word = content[4];
            if (content[2] == (State.Waiting).ToString())
            {
                player1.State = State.Waiting;
                player2.State = State.Playing;
            }
			else if(content[2] == (State.Winner).ToString())
			{
				player1.State = State.Winner;
				player2.State = State.Loser;
				r.Player1 = player1;
				r.Player2 = player2;
			}
            content[2] = player2.State.ToString();
            string str = "";
            foreach (string s in content)
            {
                str += s + ";";
            }
            Send(Connections[player2], str);
            string watch_str = "watch;" + r.Player1.State + ";" + content[4] + ";" + content[3];
            if (r.Watchers.Count != 0)
            {
                foreach (Player p in r.Watchers)
                {
                    Send(Connections[p], watch_str);
                }
            }
        }

		/// <summary>
		/// Join a room.
		/// </summary>
		/// <param name="roomID">Requested room ID</param>
		/// <param name="player">Player who request it</param>
		private void JoinRoom(int roomID, Player player)
		{
			// Searching for room.
			Room r = Room.Search(roomID, rooms);
			if (r != null)
			{
				r.Player2 = player;
				r.Player2.State = State.Waiting;
				Send(Connections[player], "joined;" + roomID.ToString() + ";" + r.Player1.Name);
				for (int i = 0; i < Requests[r.Player1].Count; i++)
				{
					Refuse(roomID);
				}
			}
			else
			{
				Send(Connections[player], "invalid room id");
			}
		}
		/// <summary>
		/// Refuse join request
		/// </summary>
		/// <param name="roomID"></param>
		private void Refuse(int roomID)
		{
			Room r = Room.Search(roomID, rooms);
			if (r != null)
			{
				Send(Connections[Requests[r.Player1].Dequeue()], "refused;");
			}
		}

		/// <summary>
		/// Request to join a room
		/// </summary>
		/// <param name="roomID">Requested room ID</param>
		/// <param name="player">Player who request it</param>
		private void JoinRequest(int roomID, Player player)
        {
            Room r = Room.Search(roomID, rooms);
			if (r != null)
			{
				if (r.Player2 == null)
				{
					if (!Requests.ContainsKey(r.Player1))
					{
						Requests.Add(r.Player1, new System.Collections.Generic.Queue<Player>());
						Requests[r.Player1].Enqueue(player);
					}
					else
					{
						Requests[r.Player1].Enqueue(player);
					}
					string s = "request;" + player.Name;
					Send(Connections[r.Player1], s);
				}
				else
				{
					string s = "sorry;";
					Send(Connections[player], s);
				}
			}			
		}

		/// <summary>
		/// Player want to play again.
		/// </summary>
		/// <param name="roomID">Room ID</param>
		/// <param name="player">The player</param>
		private void Rematch(int roomID, Player player)
		{
			Room r = Room.Search(roomID, rooms);
			player.State = State.Waiting;
			if (r.Player2 != null)
			{
				if (r.Player1.State == State.Waiting && r.Player2.State == State.Waiting)
				{
                    Send(Connections[r.Player1], "rematch;" + r.Player2.Name + ";owner");
                    Send(Connections[r.Player2], "rematch;" + r.Player1.Name);
                }
			}
		}

		/// <summary>
		/// Player want to leave the room.
		/// </summary>
		/// <param name="roomID">Room ID</param>
		/// <param name="player">The player</param>
		private void Quit(int roomID, Player player)
		{
			Room r = Room.Search(roomID, rooms);
			if (r.Player2 != null)
			{
				// get players name and state
				string players_stats = r.Player1.Name + " " + r.Player1.State.ToString() + ", " + r.Player2.Name + " " + r.Player2.State.ToString() + "\n";
				// save in file
				FileStream file = new FileStream(@"./game.txt", FileMode.OpenOrCreate | FileMode.Append);
				byte[] msgAsByteArray = Encoding.Default.GetBytes(players_stats);
				file.Write(msgAsByteArray, 0, msgAsByteArray.Length);
				//file.Position = 0;
				file.Close();
			}			
			player.State = State.Idle;
			if (r.Player1 == player)
			{
				r.Player1 = r.Player2;
            }
            r.Player2 = null;
            if (r.Player1 != null)
            {
                if (r.Player1.State == State.Waiting || r.Player1.State == State.Playing)
                {
                    Send(Connections[r.Player1], "left");
                }
            }
            if (r.Player1 == r.Player2)
			{
				rooms.Remove(r);
			}
            Send(Connections[player], "removed");
        }

		/// <summary>
		/// Watch a room
		/// </summary>
		/// <param name="roomID">room ID</param>
		/// <param name="player">Player who want to watch</param>
		private void WatchRequest(int roomID, Player player)
		{
			Room r = Room.Search(roomID, rooms);
			player.State = State.Watching;
			r.Watchers.Add(player);
			string s = "Watch_game;" + roomID + ";" + player.State.ToString() + ";" + r.Player1.Name + ";" + r.Player1.State + ";" + r.Player2.Name + ";" + r.Player2.State + ";" + r.Word;
			Send(Connections[player], s);
		}
	}
}
