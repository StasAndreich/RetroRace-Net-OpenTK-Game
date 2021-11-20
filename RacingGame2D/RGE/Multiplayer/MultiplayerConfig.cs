using System.Net;

namespace RGEngine.Multiplayer
{
    public class MultiplayerConfig
    {
        public int LocalPort { get; set; }

        public int RemotePort { get; set; }

        public IPAddress RemoteIPAddress { get; set; }
    }
}
