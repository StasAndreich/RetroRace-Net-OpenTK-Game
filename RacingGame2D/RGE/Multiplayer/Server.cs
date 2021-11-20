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
        private const string WelcomeMessage = "Welcome. You're connected to the Server.";
        
        private static int _port;
        private static int _maxPlayers;
        private static UdpClient _serverUdp;
        private static IPEndPoint _serverEndpoint;
        private static List<IPEndPoint> _remoteClients = new List<IPEndPoint>();
        private static object _lock = new object();
        
        /// <summary>Starts the server.</summary>
        /// <param name="maxPlayers">The maximum players that can be connected simultaneously.</param>
        /// <param name="port">The port to start the server on.</param>
        public static void Start(int port, int maxPlayers = 2)
        {
            _port = port;
            _maxPlayers = maxPlayers;
            _serverEndpoint = new IPEndPoint(IPAddress.Any, port);
            _serverUdp = new UdpClient(_serverEndpoint);

            while (_remoteClients.Count < _maxPlayers)
            {
                Debug.WriteLine("++++");
                var remoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                _ = _serverUdp.Receive(ref remoteIpEndPoint);
                _remoteClients.Add(remoteIpEndPoint);
            }

            lock (_lock)
            {
                EngineCore.IsReadyToStart = true;
            }

            //foreach (var client in _clients)
            //{
            //    SendToClient(Encoding.UTF8.GetBytes(WelcomeMessage), client);
            //}

            ServerLoop();
        }

        private static void ServerLoop()
        {
            while (true)
            {
                ReceiveFromClient(_remoteClients[0]);

                // Retranslate messages to another client.
                //SendClientData(ReceiveClientData(_clients[0]), _clients[1]);
                //SendClientData(ReceiveClientData(_clients[1]), _clients[0]);
            }
        }

        private static void SendToClient(byte[] data, IPEndPoint endPoint)
        {
            if (data != null)
            {
                _serverUdp.Send(data, data.Length, endPoint);
                Debug.WriteLine("Server Send");
            }
        }

        private static byte[] ReceiveFromClient(IPEndPoint endPoint)
        {
            if (_serverUdp.Available > 0)
            {
                var data = _serverUdp.Receive(ref endPoint);
                Debug.WriteLine("Server Receive");
                return data;
            }

            return null;
        }
    }
}
