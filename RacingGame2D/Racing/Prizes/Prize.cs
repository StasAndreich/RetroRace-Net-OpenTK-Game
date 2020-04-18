using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using OpenTK;
using RGEngine;
using System.Linq;

namespace Racing.Prizes
{
    public abstract class Prize : GameObject, ICollidable, INonResolveable
    {
        public Prize(params string[] texturesPath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            animator = AddComponent<Animator>();

            // Set default position.
            Position = new Vector2(0f, 0f);

            for (int i = 0; i < texturesPath.Length; i++)
            {
                var tex = ContentLoader.LoadTexture(texturesPath[i]);
                var sprite = new Sprite(tex, new Vector2(0.3f, 0.3f), new Vector2(0f, 0f), 3);
                animator.AnimationSprites.AddSprite(sprite);
            }
            animator.FPS = 3;

            base.collider = new PolyCollider(this, new Vector2(200f, 200f));
        }

        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody;
        protected Animator animator;


        public override void FixedUpdate(double fixedDeltaTime)
        {
            if (base.IsTriggered)
            {
                ApplyDecorator();
                RemovePrize();
            }
            base.FixedUpdate(fixedDeltaTime);
        }

        protected void RemovePrize()
        {
            PolyCollider.allCollidersAttached.Remove(this);

            var list = EngineCore.gameObjects.ToList<GameObject>();
            foreach (var @object in list)
            {
                if (ReferenceEquals(@object, this))
                    EngineCore.RemoveGameObject(this);
            }
        }

        protected abstract void ApplyDecorator();
    }

    public enum Prizes
    {
        Fuel = 1,
        Boost = 2,
        Slowdown = 3
    }
}
