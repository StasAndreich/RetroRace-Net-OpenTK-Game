using System;
using System.Drawing;
using OpenTK;


namespace RGEngine.Graphics
{
    /// <summary>
    /// Represents a Sprite object for use in 2D.
    /// </summary>
    public class Sprite : IComparable<Sprite>//, IDisposable
    {
        /// <summary>
        /// Stores a loaded 2D texture for sprite existence.
        /// </summary>
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }

        public int Width { get; }
        public int Height { get; }

        public int ZIndex { get; set; }

        public Vector2 Scale { get; }
        public Vector2 Position { get; set; }
        public Vector2 Offset { get; set; }

        public float Rotation { get; set; }
        public Vector2 PointOfRotation { get; set; }


        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;

            this.Scale = new Vector2(0.3f, 0.3f);
            this.Offset = Vector2.Zero;
            this.ZIndex = 0;
        }

        public Sprite(Texture2D texture, Vector2 scale, Vector2 offset, int zIndex)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;

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


        //private bool isDisposed = false;
        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //public void Dispose(bool disposing)
        //{
        //    if (!isDisposed)
        //    {
        //        if (disposing)
        //        {
        //            Texture.Dispose();
        //        }

        //        isDisposed = true;
        //    }
        //}

        //~Sprite()
        //{
        //    Dispose(false);
        //}
    }
}
