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
            this.frictionCoefficient = 0.3f;
        }


        public float frictionCoefficient;

        public float breakingForceCoefficient;

        private static float gravityAccelerationConstant = 9.8f;

        /// <summary>
        /// Current velocity.
        /// </summary>
        public float velocity;

        public float maxVelocity;

        public float maxEngineForceAcceleration;
        /// <summary>
        /// Current acceleration of an object.
        /// </summary>
        private float acceleration;


        internal override void PerformComponent(double deltaTime)
        {
            if (velocity > maxVelocity)
                velocity = maxVelocity;
            else
            {

            }

            if (velocity > -0.01 && velocity < -0.01 && maxEngineForceAcceleration == 0f)
            {
                velocity = 0f;
            }
            else
            {
                    if (velocity == 0f)
                        maxEngineForceAcceleration = -maxEngineForceAcceleration;

                acceleration = maxEngineForceAcceleration - (frictionCoefficient + breakingForceCoefficient)
                    * gravityAccelerationConstant;
                velocity += acceleration * (float)deltaTime;
            }
        }

        // ADD Drag Value
    }


    public enum DrivingModes
    {
        Reverse = -1,
        Neutral = 0,
        Drive = 1
    }
}
