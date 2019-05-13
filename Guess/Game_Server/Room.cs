using System.Collections.Generic;
using System.Xml.Serialization;

namespace Game_Server
{
	/// <summary>
	/// Difficulty of a room.
	/// </summary>
	public enum Difficulty
	{
		Easy,Medium,Hard
	}
	/// <summary>
	/// Class thats represent room in memory.
	/// </summary>
	public class Room
	{
		public Room()
		{
			Count++;
			ID = Count;
		}
		/// <summary>
		/// Create room with 1 player.
		/// </summary>
		/// <param name="p"></param>
		public Room(Player p)
		{
			Player1 = p;
			Count++;
			ID = Count;
		}
		// First player and owner.
		[XmlIgnore]
		public Player Player1 { get; set; }
		public string Player1Name
		{
			get
			{
				return Player1.Name;
			}
			set { }

		}
		// Second player.
		[XmlIgnore]
		public Player Player2 { get; set; }
		public string Player2Name
		{
			get
			{
				return Player2 == null ? "" : Player2.Name;
			}
			set { }
		}
        // Room Watchers.
        [XmlIgnore]
        public List<Player> Watchers = new List<Player>();
		// Room Difficulty
		public Difficulty Difficulty { get; set; }
		// Room ID.
		public int ID { get; set; }
		// Room category.
		public string Category { get; set; }
        public string Word { get; set; }

        private static int Count = 0;
		/// <summary>
		/// Search for specific room
		/// </summary>
		/// <param name="id">ID search for</param>
		/// <param name="Rooms">Room list search in</param>
		/// <returns>room</returns>
        public static Room Search(int id, List<Room> Rooms)
        {
            Room r = null;
            // Searching for room.
            foreach (Room item in Rooms)
            {
                if (item.ID == id)
                {
                    r = item;
                }
            }
            return r;
        }
   	}
}
