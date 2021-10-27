using OpenTK;
using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Support;

namespace Racing.Objects.UserInterface
{
    /// <summary>
    /// Defines a simple UI graphical element.
    /// </summary>
    public class UserInterfaceElement : GameObject, IUserInterfaceElement
    {
        /// <summary>
        /// Default ctor with setting a position.
        /// </summary>
        /// <param name="uiTexturePath"></param>
        /// <param name="position"></param>
        public UserInterfaceElement(string uiTexturePath, Vector2 position)
        {
            SpriteRenderer = (SpriteRenderer)AddComponent("SpriteRenderer");

            Position = position;

            var texture = ContentLoader.LoadTexture(uiTexturePath);
            var sprite = new Sprite(
                texture,
                Position,
                new Vector2(1f, 1f),
                new Vector2(0f, 0f), 10);
            SpriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(sprite);
        }

        /// <summary>
        /// Ctor for rendering SpriteBatch-ready UI elements.
        /// </summary>
        /// <param name="position"></param>
        public UserInterfaceElement(Vector2 position)
        {
            //spriteRenderer = AddComponent<SpriteRenderer>();
            SpriteRenderer = (SpriteRenderer)AddComponent("SpriteRenderer");
            Position = position;
        }

        /// <summary>
        /// Holds SpriteRenderer component.
        /// </summary>
        public SpriteRenderer SpriteRenderer { get; set; }
    }
}
