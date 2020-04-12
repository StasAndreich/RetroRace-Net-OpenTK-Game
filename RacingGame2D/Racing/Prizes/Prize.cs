using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using OpenTK;
using System;


namespace Racing.Prizes
{
    public abstract class Prize : GameObject
    {
        public Prize(string prizeTexturePath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidBody = AddComponent<RigidBody2D>();
            animator = AddComponent<Animator>();

            rigidBody.colliders = ColliderBatch.CreateColliderBatch(new BoxCollider(50, 50));
            animator.FramesGrid = SpriteBatch.CreateSpriteBatch(ContentLoader.LoadTexture(prizeTexturePath));

            Position = new Vector2(100f, -150f);
        }

        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody;
        protected Animator animator;
    }
}
