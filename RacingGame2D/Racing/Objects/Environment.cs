using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Racing.Objects
{
    public class Environment : GameObject
    {
        public Environment(string backgroundPath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();

            var bgTexture = ContentLoader.LoadTexture(backgroundPath);
            var bgSprite = new Sprite(bgTexture, new Vector2(1f, 1f),
                new Vector2(0f, 0f), -1);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(bgSprite);

            base.Position = new Vector2(0f, 0f);
        }

        private SpriteRenderer spriteRenderer;
    }
}
