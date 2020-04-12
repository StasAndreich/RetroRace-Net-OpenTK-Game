using RGEngine.BaseClasses;
using System;


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

        public ColliderBatch colliders = new ColliderBatch();

        public event EventHandler<CollisionEventArgs> OnTriggered;

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

            // Process collisions.
            // Pick each collider for this object.
            foreach (var thisCollider in this.colliders)
            {
                // Pick another collider in the whole scene.
                foreach (var otherCollider in Collider.sceneColliders)
                {
                    if (!ReferenceEquals(thisCollider, otherCollider))
                    {
                        thisCollider.DetectCollision(otherCollider);
                        thisCollider.ResolveCollision(otherCollider);
                    }
                }
            }
        }

        internal void IsTriggeredNotify(Collider other)
        {
            // THIS object invokes the event and
            // pass the OTHER object of collision as args.
            OnTriggered?.Invoke(this, new CollisionEventArgs(other));
        }

        /// <summary>
        /// Fully initializes a component.
        /// </summary>
        internal override void InitializeComponent()
        {
            foreach (var collider in colliders)
                collider.RegisterToComponent(this);
        }
    }
}
