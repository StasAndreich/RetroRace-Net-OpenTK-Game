using System;
using System.Collections.Generic;
using OpenTK;


namespace RGEngine.BaseClasses
{
    /// <summary>
    /// Base class for all entities in RGEngine scenes.
    /// </summary>
    public abstract class GameObject
    {
        public GameObject()
        {

        }

        private List<Component> components;

        public Vector2 Position { get; set; }


        protected void AddComponent<TComponent>()
        {

        }


        /// <summary>
        /// Runs once per frame. Independent from Game Physics.
        /// Uses a variable time-step.
        /// </summary>
        protected abstract void Update();
        /// <summary>
        /// Updates everything that is need to be applied to a Rigidbody.
        /// Uses a fixed time-step.
        /// </summary>
        protected abstract void FixedUpdate();
    }
}
