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
        /// Enables blending textures with alpha channel.
        /// </summary>
        static SpriteRenderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="gameObject"></param>
        public SpriteRenderer(GameObject gameObject) :
            base(gameObject)
        {
            RenderQueue = new SpriteBatch();
        }

        /// <summary>
        /// Holds the queue of Sprite objects to render.
        /// </summary>
        public SpriteBatch RenderQueue { get; set; }


        // Renders a single sprite object.
        internal static void RenderSprite(Sprite sprite)
        {
            // Define and init the 4 vertices of a rect sprite.
            var vertices = new Vector2[4]
            {
                new Vector2(0f, 0f),
                new Vector2(1f, 0f),
                new Vector2(1f, 1f),
                new Vector2(0f, 1f)
            };

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            var X = sprite.Position.X + sprite.PointOfRotation.X + sprite.Offset.X * sprite.Scale.X;
            var Y = sprite.Position.Y + sprite.PointOfRotation.Y + sprite.Offset.Y * sprite.Scale.Y;

            GL.Translate(X, Y, 0f);
            GL.Rotate(sprite.Rotation, 0.0f, 0f, 1f);
            GL.Translate(-X, -Y, 0f);          

            GL.BindTexture(TextureTarget.Texture2D, sprite.Texture.Id);
            GL.Begin(PrimitiveType.Quads);

            // Process each texture vertex.
            for (int i = 0; i < vertices.Length; i++)
            {
                // Set current texture coords.
                GL.TexCoord2(vertices[i]);

                // 'Normalize' position.
                vertices[i].X = sprite.Width * (vertices[i].X - 0.5f);
                vertices[i].Y = sprite.Height * (vertices[i].Y - 0.5f);
                vertices[i] *= sprite.Scale;
                // Translation of vertex.
                vertices[i] += sprite.Position + sprite.Offset * sprite.Scale;

                GL.Vertex2(vertices[i]);
            }
            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// Renders a list of all the sprites from the scene.
        /// </summary>
        /// <param name="batch"></param>
        internal static void RenderSpritesQueue(SpriteBatch batch)
        {
            for (int i = 0; i < batch.Quantity; i++)
            {
                SpriteRenderer.RenderSprite(batch[i]);
            }
        }

        /// <summary>
        /// Renders all objects from the scene.
        /// </summary>
        /// <param name="gameObjects"></param>
        internal static void RenderEntireFrame(List<GameObject> gameObjects)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (var gameObject in gameObjects)
            {
                if (!(gameObject is INonRenderable))
                    SpriteRenderer.RenderSpritesQueue(gameObject.GetComponent<SpriteRenderer>().RenderQueue);
            }
        }

        /// <summary>
        /// Updates sprite position and rotation state.
        /// </summary>
        /// <param name="deltaTime"></param>
        internal override void PerformComponent(double deltaTime)
        {
            foreach (var sprite in RenderQueue)
            {
                sprite.Position = base.attachedTo.Position + sprite.Offset;
                sprite.Rotation = base.attachedTo.Rotation;
            }
        }
    }

    /// <summary>
    /// Interface that disables rendering for an object.
    /// </summary>
    public interface INonRenderable { }
}
