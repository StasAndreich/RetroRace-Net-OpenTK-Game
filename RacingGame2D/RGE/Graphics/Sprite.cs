using System;
using OpenTK;

namespace RGEngine.Graphics
{
    /// <summary>
    /// Represents a Sprite object for use in 2D.
    /// </summary>
    public class Sprite : IComparable<Sprite>
    {
        /// <summary>
        /// Stores a loaded 2D texture for sprite existence.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Sprite width.
        /// </summary>
        public int Width { get; }
        /// <summary>
        /// Sprite height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Sptire order in scene layers.
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// Scale of a sprite.
        /// </summary>
        public Vector2 Scale { get; }
        /// <summary>
        /// Position of a sprite.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Offset over position of a sprite.
        /// </summary>
        public Vector2 Offset { get; set; }

        /// <summary>
        /// Sprite rotation.
        /// </summary>
        public float Rotation { get; set; }
        /// <summary>
        /// Sprite's point of rotation.
        /// </summary>
        public Vector2 PointOfRotation { get; set; }

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="texture"></param>
        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;

            this.Scale = new Vector2(0.3f, 0.3f);
            this.Offset = Vector2.Zero;
            this.ZIndex = 0;
        }

        /// <summary>
        /// Ctor that tunes the sprite in more details.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        /// <param name="offset"></param>
        /// <param name="zIndex"></param>
        public Sprite(Texture2D texture, Vector2 position, Vector2 scale, Vector2 offset, int zIndex)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;

            this.Position = position;
            this.Scale = scale;
            this.Offset = offset;
            this.ZIndex = zIndex;
        }

        /// <summary>
        /// Compares Sprites by Z-index.
        /// </summary>
        /// <param name="otherSprite"></param>
        /// <returns></returns>
        public int CompareTo(Sprite otherSprite)
        {
            if (this.ZIndex > otherSprite.ZIndex)
                return 1;
            else if (this.ZIndex < otherSprite.ZIndex)
                return -1;

            return 0;
        }
    }
}
