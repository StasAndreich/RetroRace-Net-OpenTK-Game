using OpenTK;
using System;
using System.Collections.Generic;


namespace RGEngine.Physics
{
    public abstract class Collider
    {
        protected RigidBody2D rigidBody;
        protected BoundingPoly boundingPoly;

        internal AABB aabbSurrounding;
        internal static readonly List<Collider> sceneColliders = new List<Collider>();

        public Collider(int width, int height)
        {
            // Position of a bounding poly is set to default (0; 0).
            this.boundingPoly = new BoundingPoly(new Vector2(0f, 0f), width, height);
            sceneColliders.Add(this);
        }
        public bool IsTriggered { get; set; }
        public bool IsNonMovable { get; set; }

        /// <summary>
        /// Assignes this collider to rigidbody owner.
        /// </summary>
        /// <param name="rigidBody"></param>
        internal void RegisterToComponent(RigidBody2D rigidBody)
        {
            this.rigidBody = rigidBody;
            this.boundingPoly.Position = rigidBody.attachedTo.Position;
        }

        public void Update(float angleInDegrees)
        {
            boundingPoly.Position = rigidBody.attachedTo.Position;
            Rotate(angleInDegrees);
        }

        internal abstract bool DetectCollision(Collider other);

        internal abstract void ResolveCollision(Collider other);

        internal abstract void Draw();

        protected abstract void Rotate(float angleInDegrees);
    }


    public interface ICollidable { }


    public class CollisionEventArgs : EventArgs
    {
        public readonly Collider other;

        public CollisionEventArgs(Collider other)
        {
            this.other = other;
        }
    }
}
