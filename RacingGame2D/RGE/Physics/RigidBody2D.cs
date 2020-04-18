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
        }

        public float velocity;

        public float mass;
        public float frictionConst;
        public float engineForce;
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
