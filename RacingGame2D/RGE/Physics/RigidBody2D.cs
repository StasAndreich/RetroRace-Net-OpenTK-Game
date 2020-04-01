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
        public float dragCoefficient;
        public float engineForce;
        public float breakingForce;


        internal override void PerformComponent(double deltaTime)
        {
            float dragForce = dragCoefficient * velocity;
            float attachedForce = engineForce - breakingForce - dragForce;
            float acceleration = attachedForce / mass;
              
            velocity += acceleration * (float)deltaTime;
        }
    }
}
