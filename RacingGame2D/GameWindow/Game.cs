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
        Shader shader;

        public Game(int width, int height, string title) :
            base(width, height, GraphicsMode.Default, title)
        {

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

            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            //shader.Use();
            //GL.BindVertexArray(VertexArrayObject);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 2);

            GL.Begin(PrimitiveType.Quads);
            GL.Color3(Color.Red);
            GL.Vertex2(0, 0);
            GL.Color3(Color.Green);
            GL.Vertex2(1, 0);
            GL.Vertex2(1, -1);
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
