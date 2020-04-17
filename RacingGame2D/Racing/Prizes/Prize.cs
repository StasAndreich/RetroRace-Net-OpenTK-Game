using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using OpenTK;

namespace Racing.Prizes
{
    public abstract class Prize : GameObject, IPrize, ICollidable
    {
        public Prize(params string[] texturesPath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            animator = AddComponent<Animator>();

            // Set default position.
            Position = new Vector2(0f, 0f);
            //rigidBody.colliders[0].IsNonMovable = true;


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
            base.FixedUpdate(fixedDeltaTime);
        }

        // !!!
        //protected abstract void ApplyDecorator();
    }

    public enum Prizes
    {
        Fuel = 1,
        Boost = 2,
        Slowdown = 3
    }
}
