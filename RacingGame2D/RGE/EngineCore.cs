using System;
using RGEngine.Graphics;
using RGEngine.Input;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RGEngine.BaseClasses;
using System.Collections.Generic;
using System.Linq;
using RGEngine.Multiplayer;

namespace RGEngine
{
    /// <summary>
    /// Main core class of the engine that controlls all parts
    /// like Graphics, Physics, Input etc.
    /// </summary>
    public class EngineCore : GameWindow
    {
        /// <summary>
        /// Delta time.
        /// </summary>
        private const double DeltaTimeFixedUpdate = 0.001f;
        /// <summary>
        /// All gameObj in game.
        /// </summary>
        public static readonly List<GameObject> GameObjects = new List<GameObject>();
        private double _totalTimeElapsed;

        private bool IsEnabledColliderDrawing { get; }

        /// <summary>
        /// Ctor that gives ability to enable collider drawing on the scene.
        /// </summary>
        /// <param name="enableCollidersDrawing"></param>
        public EngineCore(bool enableCollidersDrawing)
        {
            IsEnabledColliderDrawing = enableCollidersDrawing;
        }

        /// <summary>
        /// Ctor for multiplayer.
        /// </summary>
        /// <param name="enableCollidersDrawing"></param>
        /// <param name="isMultiplayer"></param>
        public EngineCore(bool enableCollidersDrawing, bool isMultiplayer)
            : this(enableCollidersDrawing)
        {
            IsMultiplayerEnabled = isMultiplayer;
        }

        public static bool IsMultiplayerEnabled { get; private set; }

        public static bool IsReadyToStart { get; set; }

        /// <summary>
        /// Inits the game on loading.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            //if (IsMultiplayerEnabled)
            //{
            //    Client.PongServer();
            //}
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
            SpriteRenderer.RenderEntireFrame(GameObjects.ToList());

            if (IsEnabledColliderDrawing)
                foreach (var @object in GameObjects.ToList())
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

            _totalTimeElapsed += e.Time;
            // Prevents redundant Updating of FixedUpdate() method.
            // *REASON: Rendering is slower than physics fixed updates.*
            //
            // Cheks if the time elapsed is bigger than deltaTimeFixedUpdate
            // than choose elapsed time from the last frame render for a FixedUpdate.
            if (_totalTimeElapsed >= DeltaTimeFixedUpdate)
            {
                foreach (var gameObject in GameObjects.ToList())
                {
                    gameObject.PerformFixedUpdate(_totalTimeElapsed);
                }
            }
            _totalTimeElapsed = 0f;

            foreach (var gameObject in GameObjects.ToList())
            {
                gameObject.PerformUpdate(e.Time);
            }

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
            GameObjects.RemoveRange(0, GameObjects.Count);
            base.OnUnload(e);
        }

        /// <summary>
        /// Adds a GameObject to a static gameObjects list.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void AddGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
        }

        /// <summary>
        /// Removes Adds a GameObject from a static gameObjects list.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void RemoveGameObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
        }
    }
}
