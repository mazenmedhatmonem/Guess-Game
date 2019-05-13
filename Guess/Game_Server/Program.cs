using System.Net;

namespace Game_Server
{
	class Program
	{
		static void Main(string[] args)
		{
			// Establish the local endpoint for the socket.
			// The DNS name of the computer
			// running the listener is "host.contoso.com".
			IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
			//IPAddress ipAddress = ipHostInfo.AddressList[0]; // ipv6 if enabled or ipv4 if not
			IPAddress ipAddress = ipHostInfo.AddressList[1]; // ipv4 if ipv6 enabled
			//Server s = new Server(new IPEndPoint(ipAddress, 11000));
			Server s = new Server(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000));
			s.Init();
		}
	}
}
