using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGEngine.Physics
{
    public abstract class Collider
    {
        protected RigidBody2D rigidBody;
        protected BoundingPoly boundingPoly;

        internal static readonly List<Collider> sceneColliders = new List<Collider>();

        public Collider(int width, int height)
        {
            // Position of a bounding poly is set to default (0; 0).
            this.boundingPoly = new BoundingPoly(new OpenTK.Vector2(0f, 0f), width, height);
            sceneColliders.Add(this);
        }
        public bool IsTriggered { get; set; }


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
            boundingPoly.Rotate(angleInDegrees);
        }

        internal abstract void Draw();
    }
}
