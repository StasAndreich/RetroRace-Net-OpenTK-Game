using OpenTK;
using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;
using System;

namespace Racing.Objects
{
    /// <summary>
    /// Defines an environment class that holds a background
    /// and collidable bounds.
    /// </summary>
    public class Environment : GameObject, ICollidable
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="backgroundPath"></param>
        public Environment(string backgroundPath)
        {
            spriteRenderer = (SpriteRenderer)AddComponent("SpriteRenderer");

            base.Position = new Vector2(0f, 0f);

            var bgTexture = ContentLoader.LoadTexture(backgroundPath);
            var bgSprite = new Sprite(bgTexture, Position, new Vector2(1f, 1f),
                new Vector2(0f, 0f), -1);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(bgSprite);

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

    /// <summary>
    /// Defines game event args.
    /// </summary>
    public class GameEventArgs : EventArgs
    {
        /// <summary>
        /// GameObject member.
        /// </summary>
        public readonly GameObject @object;

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="object"></param>
        public GameEventArgs(GameObject @object)
        {
            this.@object = @object;
        }
    }
}
