using System;
using RGEngine.Graphics;
using RGEngine.Support;
using RGEngine.Input;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using OpenTK.Input;


namespace RGEngine
{
    /// <summary>
    /// Main core class of the engine that controlls all parts
    /// like Graphics, Physics, Input etc.
    /// </summary>
    public class EngineCore : GameWindow
    {
        /// <summary>
        /// Indicates how much time elapsed from the start of the game.
        /// </summary>
        private double totalTimeElapsed;

        public static int GameWidth { get; set; }

        public static int GameHeight { get; set; }

        public EngineCore()
        {
            // may need to add smth like GameWidth = ClientSize.Width;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        // temp
        Texture2D texture;
        Sprite sprite;

        protected override void OnLoad(EventArgs e)
        {            
            GL.ClearColor(Color.SandyBrown);

            texture = ContentLoader.LoadTexture(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png");
            sprite = new Sprite(texture);

            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Camera.SetView(Width, Height);

            SpriteRenderer.RenderSprite(sprite);

            SwapBuffers();
            base.OnRenderFrame(e);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            InputController.Update();
            
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
                sprite.Position += new Vector2(0f, -4f);
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.S))
                sprite.Position += new Vector2(0f, 4f);
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.A))
                sprite.Position += new Vector2(-4f, 0f);
            if (InputController.CurrentKeyboardState.IsKeyDown(Key.D))
                sprite.Position += new Vector2(4f, 0f);

            base.OnUpdateFrame(e);
        }


        protected override void OnResize(EventArgs e)
        {
            Camera.SetView(Width, Height);

            base.OnResize(e);
        }


        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
    }
}
