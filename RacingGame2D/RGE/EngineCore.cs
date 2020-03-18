using System;
using RGEngine.Graphics;
using RGEngine.Support;

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
            // may need to add smth like GameWidth = ClientSize.Width;
            
        }

        Texture2D texture;
        Sprite sprite;

        protected override void OnLoad(EventArgs e)
        {
            // custom clearcolor vector.
            GL.ClearColor(0.3f, 0.2f, 0.6f, 1f);

            texture = ContentLoader.LoadTexture(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\low-poly-texture.jpg");
            sprite = new Sprite(texture);

            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Camera.SetView(Width, Height);

            SpriteRenderer.RenderSprite(sprite, new Vector2(0f, 0f));

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
