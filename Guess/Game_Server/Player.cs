using System;
using System.Text;

namespace Game_Server
{
	public enum State {Playing, Idle, Watching, Waiting,Winner,Loser}
	// Player object for reading client data asynchronously
	public class Player
	{
		// Player name.
		public String Name { get; set; }
		// Size of receive buffer.
		public const int BufferSize = 1024;
		public byte[] Buffer { get; set; } = new byte[BufferSize];
		// Received data string.
		public StringBuilder Data { get; set; } = new StringBuilder();
		// State of client.
		public State State { get; set; } = State.Idle;
	}
}
