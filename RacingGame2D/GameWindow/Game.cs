using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;


namespace MainGameWindow
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) :
            base(width, height, GraphicsMode.Default, title)
        {

        }


        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.4f, 0.7f, 0.1f, 1f);

            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Context.SwapBuffers();
            
            base.OnRenderFrame(e);
        }


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
            base.OnResize(e);
        }
    }
}
