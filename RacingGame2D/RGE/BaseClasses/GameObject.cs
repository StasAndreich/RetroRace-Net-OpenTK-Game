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
        public Vector2 Position { get; set; }

        /// <summary>
        /// Stores a list of the components for Update().
        /// </summary>
        private List<Component> componentsUpd;

        /// <summary>
        /// Stores a list of the components for FixedUpdate().
        /// </summary>
        private List<Component> componentsFixedUpd;

        


        public T AddComponent<T>() where T : Component, new()
        {
            return new T();
        }   


        public T GetComponent<T>()
        {

        }

        /// <summary>
        /// Runs once per frame. Independent from Game Physics.
        /// Uses a variable time-step.
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// Updates everything that is need to be applied to a Rigidbody.
        /// Uses a fixed time-step.
        /// </summary>
        public virtual void FixedUpdate(double deltaTime) { }
    }
}
