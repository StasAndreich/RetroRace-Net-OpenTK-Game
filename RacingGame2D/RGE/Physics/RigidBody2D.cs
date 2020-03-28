using OpenTK;
using RGEngine.BaseClasses;


namespace RGEngine.Physics
{
    /// <summary>
    /// A physics component for 2D sprites.
    /// </summary>
    public sealed class RigidBody2D : Component, IFixedUpdatable
    {
        public RigidBody2D(GameObject gameObject)
            : base(gameObject)
        {
            this.mass = 1f;
            this.Velocity = new Vector2(0f, 0f);
            this.acceleration = new Vector2(0f, 0f);
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


        internal override void PerformComponent(double deltaTime)
        {
            //// Calculate new value of an object Speed.
            //Velocity += acceleration * (float)deltaTime;
        }

        // ADD Drag Value
    }
}
