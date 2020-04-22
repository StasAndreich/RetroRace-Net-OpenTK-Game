using RGEngine.BaseClasses;

namespace RGEngine.Physics
{
    /// <summary>
    /// A physics component for 2D sprites.
    /// </summary>
    public sealed class RigidBody2D : Component, IFixedUpdatable
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="gameObject"></param>
        public RigidBody2D(GameObject gameObject)
            : base(gameObject)
        {
        }

        /// <summary>
        /// Responsible for object velocity.
        /// </summary>
        public float velocity;

        /// <summary>
        /// Defines object mass.
        /// </summary>
        public float mass;
        /// <summary>
        /// Defines a friction coefficient.
        /// </summary>
        public float frictionConst;
        /// <summary>
        /// Defines engine force.
        /// </summary>
        public float engineForce;
        /// <summary>
        /// Defines breaking force.
        /// </summary>
        public float breakingForce;


        internal override void PerformComponent(double deltaTime)
        {
            var frictionForce = frictionConst * velocity;
            var attachedForce = engineForce - breakingForce - frictionForce;
            float acceleration = attachedForce / mass;

            velocity += acceleration * (float)deltaTime;
        }
    }
}
