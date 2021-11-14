using OpenTK;
using System;

namespace RGEngine.Multiplayer
{
    [Serializable]
    public class Message
    {
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }
    }
}
