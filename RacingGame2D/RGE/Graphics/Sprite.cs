using System.Drawing;
using RGEngine.BaseClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Graphics
{
    /// <summary>
    /// Represents a Sprite object for use in 2D.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// Stores the object to which this Sprite is attached.
        /// </summary>
        public GameObject gameObject { get; }

        /// <summary>
        /// Stores a loaded 2D texture.
        /// </summary>
        public Texture2D Texture { get; set; }

        public Color Color { get; set; }

        public int Width { get; }

        public int Height { get; }

        public Vector2 Scale { get; }

        // temporary adding a SET modifier
        //
        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        //public Rectangle RectSpriteFrame { get; set; }


        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            //this.Color = Color.Red;
            this.Width = texture.Width;
            this.Height = texture.Height;
            // Sets a scale of a sprite to 1.
            //this.Scale = Vector2.One;
            this.Scale = new Vector2(0.2f, 0.2f);

            // temp equation
            // !!!
            // нужно ограничить размер нарисованного спрайта. или делать скейл
            // + надо добавить возможность оффсета текстуры по спрайту.
            this.Position = new Vector2(0f, 0f);
        }

        public Sprite(Texture2D texture, GameObject attachedTo)
        {
            this.gameObject = attachedTo;
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;
            this.Scale = new Vector2(0.2f, 0.2f);
            this.Position = gameObject.Position;
        }
    }
}
