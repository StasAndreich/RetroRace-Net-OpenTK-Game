using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RGEngine.Multiplayer
{
    /// <summary>
    /// Represents a UDP game client.
    /// </summary>
    public class Client
    {
        private readonly UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;

        public Client(string connectToIp, int connectToPort)
        {
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(connectToIp), connectToPort);
            var tcpClient = new TcpClient();
            tcpClient.Connect(_remoteEndPoint);

            _udpClient = new UdpClient((IPEndPoint)tcpClient.Client.LocalEndPoint);
            //_udpClient.Connect(_remoteEndPoint);
        }

        // Change to Message
        public string ReceiveDataFromServer()
        {
            if (_udpClient.Available > 0)
            {
                var data = _udpClient.Receive(ref _remoteEndPoint);
                return Encoding.UTF8.GetString(data, 0, data.Length);
            }

            Debug.WriteLine($"Client Receive");
            return string.Empty;
        }

        public void SendDataToServer(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            _udpClient.Send(data, data.Length, _remoteEndPoint);
            Debug.WriteLine($"Client Send {message}");
        }
    }
}
