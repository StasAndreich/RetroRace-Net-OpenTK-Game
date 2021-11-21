using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

/// <summary>
/// Game server.
/// </summary>
public static class Server
{
    private const string Localhost = "127.0.0.1";
    private static UdpClient _serverUdp;
    private static readonly List<IPEndPoint> _remoteClients = new List<IPEndPoint>();

    /// <summary>Starts the server.</summary>
    /// <param name="maxPlayers">The maximum players that can be connected simultaneously.</param>
    /// <param name="port">The port to start the server on.</param>
    public static void Start(int port, int maxPlayers = 2)
    {
        var serverEndpoint = new IPEndPoint(IPAddress.Parse(Localhost), port);
        _serverUdp = new UdpClient(serverEndpoint);

        while (_remoteClients.Count < maxPlayers)
        {
            //Console.WriteLine($"Waiting for a client...");
            var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            var s = _serverUdp.Receive(ref remoteIpEndPoint);
            var data = Encoding.ASCII.GetString(s, 0, s.Length);

            if (data == "pong" && !_remoteClients.Any(x => x.Port == remoteIpEndPoint.Port))
            {
                _remoteClients.Add(remoteIpEndPoint);
                //Console.WriteLine($"++ [Client {remoteIpEndPoint.Port} Connected]");

                var ping = Encoding.ASCII.GetBytes("ping");
                _serverUdp.Send(ping, ping.Length, remoteIpEndPoint);
            }
        }

        ServerLoop();
    }

    private static void ServerLoop()
    {
        while (true)
        {
            ////if (_totalTimeElapsed >= DeltaTimeFixedUpdate)
            ////{
            ////    foreach (var gameObject in GameObjects.ToList())
            ////    {
            ////        gameObject.PerformFixedUpdate(_totalTimeElapsed);
            ////    }
            ////}
            ////_totalTimeElapsed = 0f;
            //ReceiveFromClient(_remoteClients[0]);

            // Retranslate messages to another client.
            ////SendToClient(ReceiveFromClient(_remoteClients[0]), _remoteClients[1]);
            ////SendToClient(ReceiveFromClient(_remoteClients[1]), _remoteClients[0]);
            ///

            var d1 = ReceiveFromClient(_remoteClients[0]);
            var d2 = ReceiveFromClient(_remoteClients[1]);
            SendToClient(d1, _remoteClients[1]);
            SendToClient(d2, _remoteClients[0]);
        }
    }

    private static void ProcessClient(object fromEndPoint, object toEndPoint)
    {
        var from = fromEndPoint as IPEndPoint;
        var to = toEndPoint as IPEndPoint;

        while (true)
        {
            var data = ReceiveFromClient(from);
            SendToClient(data, to);
        }
    }

    private static void SendToClient(byte[] data, IPEndPoint endPoint)
    {
        if (data != null)
        {
            _serverUdp.Send(data, data.Length, endPoint);
            //Console.WriteLine($"Server Send to port {endPoint.Port}");
        }
    }

    private static byte[] ReceiveFromClient(IPEndPoint endPoint)
    {
        if (_serverUdp.Available > 0)
        {
            try
            {
                var data = _serverUdp.Receive(ref endPoint);
                //Console.WriteLine($"Server Receive from {endPoint.Port}");
                return data;
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        return null;
    }
}
