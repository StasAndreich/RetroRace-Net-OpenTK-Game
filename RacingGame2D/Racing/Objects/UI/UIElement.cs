using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Support;

namespace Racing.Objects.UI
{
    /// <summary>
    /// Defines a simple UI graphical element.
    /// </summary>
    public class UIElement : GameObject, IUIElement
    {
        /// <summary>
        /// Holds SpriteRenderer component.
        /// </summary>
        public SpriteRenderer spriteRenderer;

        /// <summary>
        /// Default ctor with setting a position.
        /// </summary>
        /// <param name="uiTexturePath"></param>
        /// <param name="position"></param>
        public UIElement(string uiTexturePath, Vector2 position)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();

            var uiTex = ContentLoader.LoadTexture(uiTexturePath);
            var uiSprite = new Sprite(uiTex, new Vector2(1f, 1f),
                new Vector2(0f, 0f), 10);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(uiSprite);

            base.Position = position;
        }

        /// <summary>
        /// Ctor for rendering SpriteBatch-ready UI elements.
        /// </summary>
        /// <param name="position"></param>
        public UIElement(Vector2 position)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            base.Position = position;
        }
    }

    /// <summary>
    /// Interface for UI element object.
    /// </summary>
    public interface IUIElement { }
}
