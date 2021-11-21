using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// Game UDP server.
/// </summary>
public class Server : IDisposable
{
    private readonly UdpClient _udp;
    private readonly List<IPEndPoint> _remoteClients = new();

    public Server(int serverPort)
    {
        _udp = new UdpClient(new IPEndPoint(IPAddress.Loopback, serverPort));
        //_udp.Client.SendTimeout = 50;
        //_udp.Client.ReceiveTimeout = 50;
    }

    /// <summary>Starts the server.</summary>
    /// <param name="maxPlayers">
    /// The maximum players that can be connected simultaneously.
    /// </param>
    public void Start(int maxPlayers = 2)
    {
        Console.WriteLine($"Server start runnning...");

        while (_remoteClients.Count < maxPlayers)
        {
            TryHandshakeAndAddClient(_remoteClients);
        }

        //_udp.BeginReceive(UdpReceiveCallback, null);
    }

    public void StartMessagingLoop()
    {
        var buffer = new byte[1024];
        while (true)
        {
            ReceiveFromClient(_remoteClients[0], ref buffer);
            SendToClient(buffer, _remoteClients[1]);

            ReceiveFromClient(_remoteClients[1], ref buffer);
            SendToClient(buffer, _remoteClients[0]);

            //Thread.Sleep(10);
        }
    }

    private void UdpReceiveCallback(IAsyncResult result)
    {
        try
        {
            var endPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = _udp.EndReceive(result, ref endPoint);
            _udp.BeginReceive(UdpReceiveCallback, null);

            //if (data.Length < 4)
            //{
            //    return;
            //}

            if (_remoteClients.Any(x => x.Port == endPoint.Port))
            {
                var anotherClient = _remoteClients.Where(x => x.Port != endPoint.Port).First();
                //SendUdpData(anotherClient, data);
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine(e);
        }

        //while (true)
        //{
        //    ////if (_totalTimeElapsed >= DeltaTimeFixedUpdate)
        //    ////{
        //    ////    foreach (var gameObject in GameObjects.ToList())
        //    ////    {
        //    ////        gameObject.PerformFixedUpdate(_totalTimeElapsed);
        //    ////    }
        //    ////}
        //    ////_totalTimeElapsed = 0f;
        //    //ReceiveFromClient(_remoteClients[0]);

        //    // Retranslate messages to another client.
        //    ////SendToClient(ReceiveFromClient(_remoteClients[0]), _remoteClients[1]);
        //    ////SendToClient(ReceiveFromClient(_remoteClients[1]), _remoteClients[0]);
        //    ///

        //    var d1 = ReceiveFromClient(_remoteClients[0]);
        //    var d2 = ReceiveFromClient(_remoteClients[1]);
        //    SendToClient(d2, _remoteClients[0]);
        //    SendToClient(d1, _remoteClients[1]);
            
        //    Thread.Sleep(10);
        //}
    }

    private void SendUdpData(IPEndPoint clientEndPoint, ref byte[] data)
    {
        try
        {
            if (clientEndPoint != null)
            {
                _udp.BeginSend(data, data.Length, clientEndPoint, null, null);
            }
        }
        catch (Exception _ex)
        {
            Console.WriteLine($"Error sending data");
        }
    }

    private void SendToClient(byte[] data, IPEndPoint endPoint)
    {
        if (data != null)
        {
            _udp.Send(data, data.Length, endPoint);
            //Console.WriteLine($"Server Send to port {endPoint.Port}");
        }
    }

    private void ReceiveFromClient(IPEndPoint endPoint, ref byte[] buf)
    {
        //if (_serverUdp.Available > 0)
        //{
        //    try
        //    {
        //        var data = _serverUdp.Receive(ref endPoint);
        //        //Console.WriteLine($"Server Receive from {endPoint.Port}");
        //        return data;
        //    }
        //    catch (System.Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        try
        {
            buf = _udp.Receive(ref endPoint);
            //Console.WriteLine($"Server Receive from {endPoint.Port}");
            //return data;
        }
        catch (System.Exception e)
        {
            //Console.WriteLine(e.ToString());
        }

        //return null;
    }

    private void TryHandshakeAndAddClient(List<IPEndPoint> endPoints)
    {
        var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
        try
        {
            var bytes = _udp.Receive(ref remoteEndPoint);
            var handshakeMessage = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

            if (handshakeMessage == "pong" && !endPoints.Any(x => x.Port == remoteEndPoint.Port))
            {
                endPoints.Add(remoteEndPoint);
                Console.WriteLine($"[Client Connected: remote port {remoteEndPoint.Port}]");

                var handshakeResponse = Encoding.ASCII.GetBytes("ping");
                _udp.Send(handshakeResponse, handshakeResponse.Length, remoteEndPoint);
            }
        }
        catch (SocketException)
        {
            Thread.Sleep(500);
        }
    }

    public void Dispose()
    {
        _udp.Dispose();
    }
}
