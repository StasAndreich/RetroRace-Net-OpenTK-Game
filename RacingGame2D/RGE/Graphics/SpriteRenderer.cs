﻿using System;
using System.Collections.Generic;
using RGEngine.BaseClasses;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RGEngine.Graphics
{
    /// <summary>
    /// Renders a Sprite for 2D graphics.
    /// </summary>
    public class SpriteRenderer : Component
    {
        /// <summary>
        /// Holds the queue of Sprite objects to render.
        /// </summary>
        public static readonly Queue<Sprite> renderQueue;

        // Renders a single sprite object.
        public static void RenderSprite(Sprite sprite, Vector2 position)
        {
            // Define and init the 4 vertices of a rect sprite.
            var vertices = new Vector2[4]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                new Vector2(0f, 1f)
            };

            // Use a texture stored in a Sprite obj.
            GL.BindTexture(TextureTarget.Texture2D, sprite.Texture.Id);

            // Start rendering.
            GL.Begin(PrimitiveType.Quads);
            // Set basic color for rendering.
            GL.Color3(sprite.Color);

            for (int i = 0; i < vertices.Length; i++)
            {
                // Set current texture coords.
                GL.TexCoord2(vertices[i]);

                // Do Translation with vectors.
                vertices[i].X *= sprite.Texture.Width;
                vertices[i].Y *= sprite.Texture.Height;

                vertices[i] += position;

                // Create a new Vertex based on a vector2.
                GL.Vertex2(vertices[i]);
            }
            // End rendering.
            GL.End();
        }

        // Renders a list of all the sprites from the scene.
        public static void RenderSpritesCollection()
        {

        }
    }
}
