using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RGEngine.Multiplayer
{
    /// <summary>
    /// Game server.
    /// </summary>
    public static class Server
    {
        private static int _port;
        private static int _maxPlayers;
        private static UdpClient _udpClient;
        private static List<IPEndPoint> _clients = new List<IPEndPoint>();

        /// <summary>Starts the server.</summary>
        /// <param name="maxPlayers">The maximum players that can be connected simultaneously.</param>
        /// <param name="port">The port to start the server on.</param>
        public static void Start(int port, int maxPlayers = 2)
        {
            _port = port;
            _maxPlayers = maxPlayers;

            var tcpListener = new TcpListener(IPAddress.Any, _port);
            _udpClient = new UdpClient((IPEndPoint)tcpListener.LocalEndpoint);
            tcpListener.Start();

            while (_clients.Count < _maxPlayers)
            {
                _clients.Add((IPEndPoint)tcpListener.AcceptTcpClient().Client.RemoteEndPoint);
            }

            tcpListener.Stop();

            foreach (var client in _clients)
            {
                SendClientData("check", client);
            }

        }

        public static void ServerLoop()
        {
            while (true)
            {
                ReceiveClientData(_clients[0]);
                //SendClientData(ReceiveClientData(_clients[0]), _clients[1]);
                //SendClientData(ReceiveClientData(_clients[1]), _clients[0]);
            }
        }

        private static void SendClientData(string message, IPEndPoint endPoint)
        {
            var data = Encoding.UTF8.GetBytes(message);
            _udpClient.Send(data, data.Length, endPoint);
            Debug.WriteLine($"Server Send {message}");
        }

        private static string ReceiveClientData(IPEndPoint endPoint)
        {
            if (_udpClient.Available > 0)
            {
                var data = _udpClient.Receive(ref endPoint);
                var s= Encoding.UTF8.GetString(data, 0, data.Length);
                Debug.WriteLine($"Server Receive: {s}");
                return s;
            }

            return string.Empty;
        }
    }
}
