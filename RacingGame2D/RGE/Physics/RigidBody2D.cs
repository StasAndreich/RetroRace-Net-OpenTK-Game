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
        public Vector2 Velocity { get; set; }


        /// <summary>
        /// Current acceleration of an object.
        /// </summary>
        public Vector2 acceleration;


        public override void CallComponent(double deltaTime)
        {
            // Calculate new value of an object Speed.
            Velocity += acceleration * (float)deltaTime;
        }


        public override Component GetComponent()
        {
            return this;
        }

        // ADD Drag Value
    }
}
