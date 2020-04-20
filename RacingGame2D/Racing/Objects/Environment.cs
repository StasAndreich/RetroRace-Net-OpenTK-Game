using OpenTK;
using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;
using System;

namespace Racing.Objects
{
    public class Environment : GameObject, ICollidable
    {
        public Environment(string backgroundPath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();

            var bgTexture = ContentLoader.LoadTexture(backgroundPath);
            var bgSprite = new Sprite(bgTexture, new Vector2(1f, 1f),
                new Vector2(0f, 0f), -1);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(bgSprite);

            base.Position = new Vector2(0f, 0f);

            // Middle collider.
            base.collider = new PolyCollider(this, new Vector2(1310f, 480f));
            // Add screen bounds.
            EngineCore.AddGameObject(new Bound(new Vector2(-945f, 0f), new Vector2(20f, 1080f)));
            EngineCore.AddGameObject(new Bound(new Vector2(945f, 0f), new Vector2(20f, 1080f)));
            EngineCore.AddGameObject(new Bound(new Vector2(0f, 525f), new Vector2(1920f, 20f)));
            EngineCore.AddGameObject(new Bound(new Vector2(0f, -525f), new Vector2(1920f, 20f)));
        }        

        private class Bound : GameObject, ICollidable, INonRenderable
        {
            public Bound(Vector2 position, Vector2 size)
            {
                base.Position = position;
                base.collider = new PolyCollider(this, size);
            }   
        }

        private SpriteRenderer spriteRenderer;
    }

    public class GameEventArgs : EventArgs
    {
        public readonly GameObject @object;

        public GameEventArgs(GameObject @object)
        {
            this.@object = @object;
        }
    }
}
