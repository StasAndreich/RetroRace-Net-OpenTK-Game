using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace RGEngine.Multiplayer
{
    /// <summary>
    /// Represents a UDP game client.
    /// </summary>
    public class Client
    {
        private readonly UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;

        /// <summary>
        /// Creates a client to send and receive game props.
        /// </summary>
        /// <param name="localPort"></param>
        /// <param name="remoteIp"></param>
        /// <param name="remotePort"></param>
        public Client(int localPort, string remoteIp, int remotePort)
        {
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);

            var localEndPoint = new IPEndPoint(IPAddress.Any, localPort);
            _udpClient = new UdpClient(localEndPoint);

            SendDataToServer(new Message());
        }

        /// <summary>
        /// Receives data from server and parses to Message object.
        /// </summary>
        /// <returns>Message</returns>
        /// <remarks>Can return null if nothing to receive.</remarks>
        public Message ReceiveDataFromServer()
        {
            if (_udpClient.Available > 0)
            {
                var data = _udpClient.Receive(ref _remoteEndPoint);

                var formatter = new BinaryFormatter();
                using var memoryStream = new MemoryStream();
                memoryStream.Write(data, 0, data.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);

                Message message = (Message) formatter.Deserialize(memoryStream);
                Debug.WriteLine($"Client Receive {message}");

                return message;
            }

            return null;
        }

        /// <summary>
        /// Send Message object to Server.
        /// </summary>
        /// <param name="message"></param>
        public void SendDataToServer(Message message)
        {
            if (message == null)
            {
                return;
            }

            var formatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, message);
            var data = memoryStream.ToArray();

            _udpClient.Send(data, data.Length, _remoteEndPoint);
            Debug.WriteLine($"Client Send {message}");
        }
    }
}
