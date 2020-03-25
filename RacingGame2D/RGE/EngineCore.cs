using System;
using RGEngine.Graphics;
using RGEngine.Support;
using RGEngine.Input;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RGEngine.BaseClasses;
using OpenTK.Input;
using System.Collections.Generic;


namespace RGEngine
{
    /// <summary>
    /// Main core class of the engine that controlls all parts
    /// like Graphics, Physics, Input etc.
    /// </summary>
    public class EngineCore : GameWindow
    {
        private static readonly List<GameObject> gameObjects = new List<GameObject>();
        /// <summary>
        /// Indicates how much time elapsed from the start of the game.
        /// </summary>
        private double totalTimeElapsed;

        public static double deltaTimeFixedUpdate { get; set; }

        public static int GameWidth { get; set; }

        public static int GameHeight { get; set; }

        public EngineCore()
        {
            // may need to add smth like GameWidth = ClientSize.Width;
            deltaTimeFixedUpdate = 0.001f;
        }


        protected override void OnLoad(EventArgs e)
        {            
            GL.ClearColor(Color.SandyBrown);

            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Camera.SetView(Width, Height);

            SwapBuffers();
            base.OnRenderFrame(e);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            InputController.Update();

            //foreach (var gameObject in gameObjects)
            //{
            //    // update with deltatime
            //}

            // Make a fixed update over a delta time.
            gameObjects[0].FixedUpdate(deltaTimeFixedUpdate);

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


        public static void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
    }
}
