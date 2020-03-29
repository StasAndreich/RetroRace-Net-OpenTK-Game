﻿using System;
using System.Drawing;
using RGEngine.BaseClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL;


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

        public Color Color { get; set; }

        public int Width { get; }

        public int Height { get; }

        public int OrderInLayer { get; set; }

        public Vector2 Scale { get; }

        public Vector2 Position { get; set; }

        public Vector2 Offset { get; set; }

        public float Rotation { get; set; }

        public Vector2 RotationPoint { get; }


        public Sprite(Texture2D texture)
        {
            this.Texture = texture;
            this.Width = texture.Width;
            this.Height = texture.Height;
            this.Scale = new Vector2(0.3f, 0.3f);
            this.Offset = Vector2.Zero;
            this.OrderInLayer = 0;
        }


        /// <summary>
        /// Compares Sprites by Z-index.
        /// </summary>
        /// <param name="otherSprite"></param>
        /// <returns></returns>
        public int CompareTo(Sprite otherSprite)
        {
            if (this.OrderInLayer > otherSprite.OrderInLayer)
                return 1;
            else if (this.OrderInLayer < otherSprite.OrderInLayer)
                return -1;

            return 0;
        }
    }
}
