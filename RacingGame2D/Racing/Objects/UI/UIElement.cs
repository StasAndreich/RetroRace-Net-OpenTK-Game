using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Support;

namespace Racing.Objects.UI
{
    public class UIElement : GameObject, IUIElement
    {
        private SpriteRenderer spriteRenderer;

        public UIElement(string uiTexturePath, Vector2 position)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();

            var uiTex = ContentLoader.LoadTexture(uiTexturePath);
            var uiSprite = new Sprite(uiTex, new Vector2(1f, 1f),
                new Vector2(0f, 0f), 10);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(uiSprite);

            base.Position = position;
        }
    }

    public interface IUIElement { }
}
