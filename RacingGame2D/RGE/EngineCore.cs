﻿using System;
using RGEngine.Graphics;
using RGEngine.Support;
using RGEngine.Input;
using RGEngine.Physics;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RGEngine.BaseClasses;
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
            SpriteRenderer.RenderEntireFrame(gameObjects);

            if (EnableColliderDrawing)
                for (int i = 0; i < Collider.sceneColliders.Count; i++)
                    Collider.sceneColliders[i].Draw();

            SwapBuffers();
            base.OnRenderFrame(e);
        }


        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            InputController.Update();

            foreach (var gameObject in gameObjects)
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
                foreach (var gameObject in gameObjects)
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
            // Add Removing gameObjects.
            base.OnUnload(e);
        }


        public static void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
            gameObject.InitializeObject();
        }
    }
}
