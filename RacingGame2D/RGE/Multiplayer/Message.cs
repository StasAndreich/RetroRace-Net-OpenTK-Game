using OpenTK;
using System;

namespace RGEngine.Multiplayer
{
    [Serializable]
    public class Message
    {
        public Vector2 CarPosition { get; set; }

        public float CarRotation { get; set; }

        public override string ToString()
        {
            return $"{CarPosition} -- {CarRotation}";
        }
    }
}
