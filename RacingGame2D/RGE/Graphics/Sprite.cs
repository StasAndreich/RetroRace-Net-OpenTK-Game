using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGEngine.Support;


namespace RGEngine.Graphics
{
    /// <summary>
    /// Represents a Sprite object for use in 2D.
    /// </summary>
    public class Sprite
    {
        /// <summary>
        /// Stores a loaded 2D texture.
        /// </summary>
        public Texture2D Texture2D { get; set; }

        public int Width { get; }

        public int Height { get; }

        public float Rotation { get; set; }


        public Sprite(Texture2D texture)
        {
           this.Texture2D = texture;
           this.Width = texture.Width;
           this.Height = texture.Height;
        }
    }
}
