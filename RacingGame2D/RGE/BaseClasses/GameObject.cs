using System;
using System.Reflection;
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
        private List<Component> components;

        ///// <summary>
        ///// Stores a list of the components for FixedUpdate().
        ///// </summary>
        //private List<Component> componentsFixedUpd;


        /// <summary>
        /// Adds a Component attached to a GameObject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void AddComponent<T>() where T : Component
        {
            // Instantiate THIS class in Component constructor.
            var tConstructor = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                null, new[] { typeof(GameObject) }, null);

            var newComponent = (T)tConstructor.Invoke(new[] { this });

            components.Add(newComponent);
        }   


        /// <summary>
        /// Returns a Component attached to a GameObject.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : Component
        {
            foreach (var component in components)
            {
                var @return = component as T;
                if (@return != null && @return.GetType() != typeof(T))
                    return @return;
            }

            return null;
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
