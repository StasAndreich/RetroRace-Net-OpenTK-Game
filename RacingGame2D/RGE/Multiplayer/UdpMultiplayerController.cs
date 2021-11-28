using RGEngine.BaseClasses;
using RGEngine.Graphics;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Timers;

namespace RGEngine.Multiplayer
{
    /// <summary>
    /// A UDP controller of all game networking actions.
    /// </summary>
    public class UdpMultiplayerController : GameObject, INonRenderable
    {
        // Server.
        private bool _isWaitingForClient;

        // Client.
        private bool _isHandshaking;
        private static bool _isReconnecting;

        // Shared.
        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;

        /// <summary>
        /// Initializes a local UDP-client and a remote endpoint.
        /// </summary>
        /// <param name="localPort"></param>
        /// <param name="remoteIp"></param>
        /// <param name="remotePort"></param>
        /// <exception cref="SocketException">
        /// Can throw exception while trying to create a client on unavailable port.
        /// </exception>
        public UdpMultiplayerController(int localPort, IPAddress remoteIp, int remotePort)
        {
            _udpClient = new UdpClient(localPort);
            //temp
            _remoteEndPoint = new IPEndPoint(remoteIp, remotePort);
        }

        public static Message ReceivedMessage { get; set; } = new Message();

        public static Message MessageToSend { get; set; } = new Message();

        public bool TryConnectByHandshake()
        {
            _isHandshaking = true;
            _isReconnecting = true;
            var reconnectTimer = new System.Timers.Timer(3000);
            reconnectTimer.Elapsed += OnTimedEvent;
            reconnectTimer.Enabled = true;

            while (_isHandshaking && _isReconnecting)
            {
                //reconnectAttempts++;

                var data = Encoding.ASCII.GetBytes("PING");
                try
                {
                    _udpClient.Send(data, data.Length, _remoteEndPoint);
                    Debug.WriteLine($"Client made PING");
                }
                catch (SocketException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                if (_udpClient.Available > 0)
                {
                    byte[] response;
                    try
                    {
                        response = _udpClient.Receive(ref _remoteEndPoint);
                        var responseFromServer = Encoding.ASCII.GetString(response, 0, response.Length);
                        Debug.WriteLine($"Client Received: {responseFromServer}");

                        if (_isHandshaking && responseFromServer == "PONG")
                        {
                            _isHandshaking = false;
                        }
                    }
                    catch (SocketException e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
            }

            if (_isHandshaking)
            {
                _udpClient.Close();
            }

            return !_isHandshaking;
        }

        public bool TryAcceptHandshake()
        {
            _isWaitingForClient = true;

            while (_isWaitingForClient)
            {
                var clientRequest = _udpClient.Receive(ref _remoteEndPoint);
                var clientData = Encoding.ASCII.GetString(clientRequest, 0, clientRequest.Length);

                if (clientData == "PING")
                {
                    _isWaitingForClient = false;
                    Debug.WriteLine("PING from client received by host");
                    var serverResponse = Encoding.ASCII.GetBytes("PONG");
                    _udpClient.Send(serverResponse, serverResponse.Length, _remoteEndPoint);
                }
            }

            return !_isWaitingForClient;
        }

        public override void FixedUpdate(double fixedDeltaTime)
        {
            SendMessage();
            SetDefaultBufferState();
            ReceiveMessage();
        }

        private void SendMessage()
        {
            try
            {
                var formatter = new BinaryFormatter();
                using var memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, MessageToSend);
                var data = memoryStream.ToArray();

                _udpClient.Send(data, data.Length, _remoteEndPoint);
            }
            catch (System.Exception)
            {
                Debug.WriteLine("sd");
            }
            
        }

        private void ReceiveMessage()
        {
            try
            {
                var data = _udpClient.Receive(ref _remoteEndPoint);

                var formatter = new BinaryFormatter();
                using var memoryStream = new MemoryStream();
                memoryStream.Write(data, 0, data.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                if (memoryStream.Length < 5)
                {
                    return;
                }

                ReceivedMessage = (Message)formatter.Deserialize(memoryStream);
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e.Message);
                if (e.Message.Contains("An existing connection was forcibly closed by the remote host"))
                {
                    _udpClient.Close();
                    Process.GetCurrentProcess().Close();
                }
            }
        }

        private void SetDefaultBufferState()
        {
            MessageToSend.PrizeType = 0;
            MessageToSend.PrizeId = 0;
        }

        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _isReconnecting = false;
        }
    }
}
