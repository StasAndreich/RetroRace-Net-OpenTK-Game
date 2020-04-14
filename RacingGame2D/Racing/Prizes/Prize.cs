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

            Position = new Vector2(0f, 0f);
            rigidBody.colliders = ColliderBatch.CreateColliderBatch(new BoxCollider(120, 124));
            rigidBody.colliders[0].IsNonMovable = true;
            var tex = ContentLoader.LoadTexture(prizeTexturePath);
            var spr = new Sprite(tex, new Vector2(0.3f, 0.3f), new Vector2(0f, 0f), 5);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(spr);

            
        }

        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody;
        protected Animator animator;
    }
}
