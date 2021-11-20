using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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
        private bool _isPingPongInProcess = true;

        /// <summary>
        /// Creates a client to send and receive game props.
        /// </summary>
        /// <param name="localPort"></param>
        /// <param name="remoteIp"></param>
        /// <param name="remotePort"></param>
        public Client(int localPort, IPAddress remoteIp, int remotePort)
        {
            _remoteEndPoint = new IPEndPoint(remoteIp, remotePort);

            var localEndPoint = new IPEndPoint(IPAddress.Any, localPort);
            _udpClient = new UdpClient(localEndPoint);

            PongServer();
            //Thread.Sleep(5000);
            //_udpClient.Connect(_remoteEndPoint);
            //SendDataToServer(new Message());
            Debug.WriteLine("d");
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

                if (memoryStream.Length < 5)
                {
                    return null;
                }

                Message message = (Message) formatter.Deserialize(memoryStream);
                Debug.WriteLine($"Client Receive {message}");
                memoryStream.Flush();

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
            //_udpClient.Send(data, data.Length);
            Debug.WriteLine($"Client Send {message}");
        }

        private void PongServer()
        {
            while(_isPingPongInProcess)
            {
                var data = Encoding.ASCII.GetBytes("pong");

                try
                {
                    _udpClient.Send(data, data.Length, _remoteEndPoint);
                }
                catch (System.Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }

                Debug.WriteLine($"Pong from client");

                if (_udpClient.Available > 0)
                {
                    byte[] response;
                    try
                    {
                        response = _udpClient.Receive(ref _remoteEndPoint);

                        var ping = Encoding.ASCII.GetString(response, 0, response.Length);
                        Debug.WriteLine($"Client Receive {ping}");

                        if (_isPingPongInProcess && ping == "ping")
                        {
                            _isPingPongInProcess = false;
                        }
                    }
                    catch (System.Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
            }
        }
    }
}
