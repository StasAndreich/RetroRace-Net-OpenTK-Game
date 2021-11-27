using OpenTK;
using System;

namespace RGEngine.Multiplayer
{
    /// <summary>
    /// A Message object to pass through the net.
    /// It contains game props.
    /// </summary>
    /// <remarks>Marked as Serializable.</remarks>
    [Serializable]
    public class Message
    {
        public string Id { get; set; }

        public Vector2 CarPosition { get; set; }

        public float CarRotation { get; set; }

        public float Fuel { get; set; }

        public int Laps { get; set; }

        public bool IsGameEnded { get; set; }

        public override string ToString()
        {
            return $"{CarPosition} -- {CarRotation}";
        }
    }
}
