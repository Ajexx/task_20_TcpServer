using System.Net;
using System.Net.Sockets;

int port = 5000;

using Socket server = new Socket
    (
    AddressFamily.InterNetwork,
    SocketType.Stream,
    ProtocolType.Tcp
    );

var IpEndpoint = new IPEndPoint(IPAddress.Loopback, port);

server.Bind(IpEndpoint);

server.Listen(10);

using Socket client = server.Accept();

byte[] buffer = new byte[1024];
var collected = new List<byte>();
Console.WriteLine("Server is connecting...");
while (true)
{
    int received;
    try
    {
        received = client.Receive(buffer);
    }
    catch (Exception)
    {
        break;
    }

    if (received == 0)
    {
        break;
    }

    for (int i = 0; i < received; i++)
    {
        collected.Add(buffer[i]);
    }
}

if (collected.Count > 0)
{
    client.Send(collected.ToArray());
}

