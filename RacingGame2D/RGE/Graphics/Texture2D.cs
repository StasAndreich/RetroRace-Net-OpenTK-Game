using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGEngine.Graphics
{
    /// <summary>
    /// Describes a 2D texture.
    /// </summary>
    public class Texture2D
    {
        /// <summary>
        /// Specifies a texture identifier.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Texture's width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Texture's height.
        /// </summary>
        public int Height { get; }


        /// <summary>
        /// Initializes a new instance of a Texture2D.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Texture2D(int id, int width, int height)
        {
            this.Id = id;
            this.Width = width;
            this.Height = height;
        }
    }
}
