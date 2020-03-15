﻿using RGEngine.Graphics;
using System.Drawing;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Support
{
    public class ContentLoader
    {
        /// <summary>
        /// Loads a texture from an image.
        /// </summary>
        /// <param name="texturePath"></param>
        /// <returns></returns>
        public static Texture2D LoadTexture(string texturePath)
        {
            // Genereate a new texture type and
            // bind it to ID.
            int textureId = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureId);

            // Load a texture to a bitmap.
            Bitmap bitmap = new Bitmap(texturePath);
            // Convert texture bitmap to a bits data.
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Specify a texture 2D image.
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            // TextureWrapS applies wrapping mode for S-axis.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Clamp);
            // TextureWrapS applies wrapping mode for T-axis.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Clamp);

            // Minimizing filtering setup.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            // Magnifying filtering setup.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            return new Texture2D(textureId, bitmap.Width, bitmap.Height);
        }
    }
}
