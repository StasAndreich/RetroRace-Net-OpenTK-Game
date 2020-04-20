using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;

namespace Racing.Objects
{
    public class OuterFinishLine : GameObject, INonRenderable, ICollidable, INonResolveable
    {
        public OuterFinishLine()
        {
            base.Position = new Vector2(290f, 380f);
            base.collider = new PolyCollider(this, new Vector2(15f, 250f));
        }
    }
}
