using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;

namespace Racing.Objects
{
    /// <summary>
    /// Defines a finish line outer collider.
    /// </summary>
    public class OuterFinishLine : GameObject, INonRenderable, ICollidable, INonResolveable
    {
        /// <summary>
        /// Default ctor with position and collider settings.
        /// </summary>
        public OuterFinishLine()
        {
            //base.Position = new Vector2(150f, 380f);
            Position = new Vector2(290f, 380f);
            collider = new PolyCollider(this, new Vector2(15f, 250f));
        }
    }
}
