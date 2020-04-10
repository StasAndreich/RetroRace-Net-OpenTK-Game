using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using OpenTK;


namespace Racing.Prizes
{
    public abstract class Prize : GameObject
    {
        public Prize(string prizeTexturePath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidBody = AddComponent<RigidBody2D>();

            var prizeTexture = ContentLoader.LoadTexture(prizeTexturePath);
            var prizeSprite = new Sprite(prizeTexture, new Vector2(0.2f, 0.2f),
                new Vector2(0f, 0f), 0);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(prizeSprite);
            rigidBody.colliders = ColliderBatch.CreateColliderBatch(new BoxCollider(50, 50));

            Position = new Vector2(100f, -150f);
        }

        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody;
    }
}
