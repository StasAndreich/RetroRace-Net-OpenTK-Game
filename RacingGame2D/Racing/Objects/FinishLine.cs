using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;

namespace Racing.Objects
{
    public class FinishLine : GameObject, INonRenderable, ICollidable, INonResolveable
    {
        public FinishLine()
        {
            base.Position = new Vector2(10f, 380f);
            base.collider = new PolyCollider(this, new Vector2(15f, 250f));
        }
    }
}
