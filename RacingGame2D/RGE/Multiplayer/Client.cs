﻿using System.Diagnostics;
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

        public Client(string connectToIp, int connectToPort)
        {
            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(connectToIp), connectToPort);
            var tcpClient = new TcpClient();
            tcpClient.Connect(_remoteEndPoint);

            _udpClient = new UdpClient((IPEndPoint)tcpClient.Client.LocalEndPoint);
            //_udpClient.Connect(_remoteEndPoint);
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