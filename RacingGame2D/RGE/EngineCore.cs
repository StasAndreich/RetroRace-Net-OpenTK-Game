using System;
using RGEngine.Graphics;
using RGEngine.Input;
using RGEngine.Physics;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RGEngine.BaseClasses;
using System.Collections.Generic;
using System.Linq;

namespace RGEngine
{
    /// <summary>
    /// Main core class of the engine that controlls all parts
    /// like Graphics, Physics, Input etc.
    /// </summary>
    public class EngineCore : GameWindow
    {
        public static readonly List<GameObject> gameObjects = new List<GameObject>();
        private double totalTimeElapsed;

        public static double deltaTimeFixedUpdate;
        private bool EnableColliderDrawing { get; }

        public static int GameWidth { get; set; }
        public static int GameHeight { get; set; }

        public EngineCore()
        {
            // may need to add smth like GameWidth = ClientSize.Width;
            deltaTimeFixedUpdate = 0.001f;
        }

        public EngineCore(bool enableCollidersDrawing)
            : this()
        {
            this.EnableColliderDrawing = enableCollidersDrawing;
        }

        protected override void OnLoad(EventArgs e)
        {            
            GL.ClearColor(Color.White);
            Camera.SetView(Width, Height);
            base.OnLoad(e);
        }


        protected override void OnRenderFrame(FrameEventArgs e)
        {
            SpriteRenderer.RenderEntireFrame(gameObjects.ToList<GameObject>());

            if (EnableColliderDrawing)
                foreach (var @object in gameObjects.ToList<GameObject>())
                    @object.collider?.Draw();

            SwapBuffers();
            base.OnRenderFrame(e);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            InputController.Update();

            foreach (var gameObject in gameObjects.ToList<GameObject>())
            {
                gameObject.PerformUpdate(e.Time);
            }

            totalTimeElapsed += e.Time;
            /// Prevents redundant Updating of FixedUpdate() method.
            /// *REASON: Rendering is slower than physics fixed updates.*
            ///
            /// Cheks if the time elapsed is bigger than deltaTimeFixedUpdate
            /// than choose elapsed time from the last frame render for a FixedUpdate.
            if (totalTimeElapsed >= deltaTimeFixedUpdate)
            {
                foreach (var gameObject in gameObjects.ToList<GameObject>())
                {
                    gameObject.PerformFixedUpdate(totalTimeElapsed);
                }
            }
            totalTimeElapsed = 0f;
            
            base.OnUpdateFrame(e);
        }


        protected override void OnResize(EventArgs e)
        {
            Camera.SetView(Width, Height);
            base.OnResize(e);
        }


        protected override void OnUnload(EventArgs e)
        {
            gameObjects.RemoveRange(0, gameObjects.Count);
            base.OnUnload(e);
        }


        public static void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }
    }
}
