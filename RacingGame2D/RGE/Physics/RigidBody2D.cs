using OpenTK;
using RGEngine.BaseClasses;


namespace RGEngine.Physics
{
    /// <summary>
    /// A physics component for 2D sprites.
    /// </summary>
    public sealed class RigidBody2D : Component
    {
        public RigidBody2D(GameObject gameObject)
            : base(gameObject)
        {

        }

        /// <summary>
        /// The mass of an object.
        /// </summary>
        private float mass;

        /// <summary>
        /// Current velocity of an object for X and Y.
        /// </summary>
        public Vector2 velocity { get; set; }


        // ADD Drag Value
    }
}
