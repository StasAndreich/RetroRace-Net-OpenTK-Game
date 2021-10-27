using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;

namespace Racing.Objects
{
    /// <summary>
    /// Defines a finish line object.
    /// </summary>
    public class FinishLine : GameObject, INonRenderable, ICollidable, INonResolveable
    {
        /// <summary>
        /// Default ctor with position and collider settings.
        /// </summary>
        public FinishLine()
        {
            //base.Position = new Vector2(10f, 380f);
            Position = new Vector2(150f, 380f);
            collider = new PolyCollider(this, new Vector2(15f, 250f));
        }
    }
}
