using RGEngine.BaseClasses;
using RGEngine.Graphics;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace RGEngine.Multiplayer
{
    public class UdpHandlerObject : GameObject, INonRenderable
    {
        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;

        public UdpHandlerObject(int port)
        {
            _udpClient = new UdpClient(new IPEndPoint(IPAddress.Loopback, port));
            //temp
            _remoteEndPoint = new IPEndPoint(IPAddress.Loopback, EngineCore.RemotePort);
        }

        public static Message ReceivedMessage { get; set; } = new Message();

        public static Message MessageToSend { get; set; } = new Message();

        // handshake

        public override void FixedUpdate(double fixedDeltaTime)
        {
            SendMessage();
            ReceiveMessage();
        }

        private void SendMessage()
        {
            var formatter = new BinaryFormatter();
            using var memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, MessageToSend);
            var data = memoryStream.ToArray();

            _udpClient.Send(data, data.Length, _remoteEndPoint);
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
            }
        }
    }
}
