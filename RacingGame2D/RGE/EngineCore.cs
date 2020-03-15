using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RGEngine
{
    /// <summary>
    /// Main core class of the engine that controlls all parts
    /// like Graphics, Physics, Input etc.
    /// </summary>
    public class EngineCore : GameWindow
    {
        public static int GameWidth { get; set; }

        public static int GameHeight { get; set; }

        public EngineCore()
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            // custom clearcolor vector.
            GL.ClearColor(0.3f, 0.2f, 0.6f, 1f);

            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SwapBuffers();
            base.OnRenderFrame(e);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            

            base.OnUpdateFrame(e);
        }


        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            base.OnResize(e);
        }


        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
    }
}
