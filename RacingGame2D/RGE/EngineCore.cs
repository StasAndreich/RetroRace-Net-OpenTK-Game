using System;
using RGEngine.Graphics;
using RGEngine.Input;
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
        /// <summary>
        /// Stores all current objects in the game.
        /// </summary>
        public static readonly List<GameObject> gameObjects = new List<GameObject>();
        private double totalTimeElapsed;

        /// <summary>
        /// Defines a time interval for FixedUpdate().
        /// </summary>
        public static double deltaTimeFixedUpdate;
        private bool EnableColliderDrawing { get; }

        /// <summary>
        /// Default ctor for EngineCore class.
        /// </summary>
        public EngineCore()
        {
            deltaTimeFixedUpdate = 0.001f;
        }

        /// <summary>
        /// Ctor that gives ability to enable collider drawing on the scene.
        /// </summary>
        /// <param name="enableCollidersDrawing"></param>
        public EngineCore(bool enableCollidersDrawing)
            : this()
        {
            this.EnableColliderDrawing = enableCollidersDrawing;
        }

        /// <summary>
        /// Inits the game on loading.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {            
            GL.ClearColor(Color.White);
            Camera.SetView(Width, Height);
            base.OnLoad(e);
        }

        /// <summary>
        /// Defines frames rendering.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            SpriteRenderer.RenderEntireFrame(gameObjects.ToList<GameObject>());

            if (EnableColliderDrawing)
                foreach (var @object in gameObjects.ToList<GameObject>())
                    @object.collider?.Draw();

            SwapBuffers();
            base.OnRenderFrame(e);
        }

        /// <summary>
        /// Do all the preparations before frame rendering and updates the game state.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            InputController.Update();

            foreach (var gameObject in gameObjects.ToList<GameObject>())
            {
                gameObject.PerformUpdate(e.Time);
            }

            totalTimeElapsed += e.Time;
            // Prevents redundant Updating of FixedUpdate() method.
            // *REASON: Rendering is slower than physics fixed updates.*
            //
            // Cheks if the time elapsed is bigger than deltaTimeFixedUpdate
            // than choose elapsed time from the last frame render for a FixedUpdate.
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

        /// <summary>
        /// Defines actions that are made on GameWindow Resized event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            Camera.SetView(Width, Height);
            base.OnResize(e);
        }

        /// <summary>
        /// Do actions on unloading the game.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnUnload(EventArgs e)
        {
            gameObjects.RemoveRange(0, gameObjects.Count);
            base.OnUnload(e);
        }

        /// <summary>
        /// Adds a GameObject to a static gameObjects list.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }

        /// <summary>
        /// Removes Adds a GameObject from a static gameObjects list.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }
    }
}
