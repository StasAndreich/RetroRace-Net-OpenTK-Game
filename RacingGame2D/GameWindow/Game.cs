using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace MainGameWindow
{
    public class Game : GameWindow
    {
        float[] vertices = {
            -0.5f, -0.5f, //Bottom-left vertex
            0.5f, -0.5f, //Bottom-right vertex
            0.0f,  0.5f //Top vertex
        };

        int VertexBufferObject;
        int VertexArrayObject;
        int texture;
        Shader shader;

        public Game(int width, int height, string title) :
            base(width, height, GraphicsMode.Default, title)
        {
            GL.Enable(EnableCap.Texture2D);
        }


        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.4f, 0.7f, 0.1f, 1f);

            //VertexBufferObject = GL.GenBuffer();
            //VertexArrayObject = GL.GenVertexArray();

            //GL.BindVertexArray(VertexArrayObject);
            //GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            //shader = new Shader(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\GameWindow\fragment.glsl",
            //    @"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\GameWindow\vertex.glsl");

            //GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);
            //GL.EnableVertexAttribArray(0);

            //shader.Use();
            //// draw func
            ///

            texture = LoadTexture(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\low-poly-texture.jpg");

            base.OnLoad(e);
        }

        // temp loader func.
        public int LoadTexture(string path)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bitmap = new Bitmap(path);
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            // TextureWrapS is for S axis.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int) TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
                (int)TextureWrapMode.Clamp);

            // minimize
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);
            // magnify
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            return id;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //shader.Use();
            //GL.BindVertexArray(VertexArrayObject);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 2);

            // bind and draw in all the primitivw a custom texture
            GL.BindTexture(TextureTarget.Texture2D, texture);

            // delimit the vertices of a primitive or a group of like primitives
            GL.Begin(PrimitiveType.Quads);
            
            //GL.Color3(Color.Red);
            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);

            //GL.Color3(Color.Green);
            GL.TexCoord2(1, 0);
            GL.Vertex2(1, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex2(1, -1);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0.4f, -0.7f);
            GL.End();

            SwapBuffers();
            
            base.OnRenderFrame(e);
        }


        // exit from keyboard key
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }


        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);

            //shader.Dispose();
            base.OnUnload(e);
        }
    }
}
