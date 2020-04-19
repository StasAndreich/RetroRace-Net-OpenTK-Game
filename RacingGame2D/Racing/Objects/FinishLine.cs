using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;
using System;

namespace Racing.Objects
{
    public class FinishLine : GameObject, INonRenderable, ICollidable, INonResolveable
    {
        public FinishLine()
        {
            base.Position = new Vector2(160f, 380f);
            base.collider = new PolyCollider(this, new Vector2(35f, 250f));
        }
    }
}
