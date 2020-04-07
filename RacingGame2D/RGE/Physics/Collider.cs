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

        internal static readonly List<Collider> allColliders = new List<Collider>();

        public Collider(int width, int height)
        {
            boundingPoly = new BoundingPoly(rigidBody.attachedTo.Position, width, height);
            allColliders.Add(this);
        }
        public bool IsTriggered { get; set; }


        internal abstract void Draw();
    }
}
