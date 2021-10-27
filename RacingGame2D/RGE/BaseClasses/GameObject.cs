using System;
using System.Collections.Generic;
using OpenTK;
using RGEngine.Physics;
using RGEngine.Graphics;

namespace RGEngine.BaseClasses
{
    /// <summary>
    /// Base class for all entities in RGEngine scenes.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Stores a list of the components for Update().
        /// </summary>
        internal readonly List<Component> components = new List<Component>();

        /// <summary>
        /// Keeps current position of GameObject.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Stores a GameObjectCollider.
        /// </summary>
        public PolyCollider collider;

        private float _rotation;
        /// <summary>
        /// Keeps current rotation of GameObject.
        /// </summary>
        public float Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                OnRotationChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Event that raised when rotation occured.
        /// </summary>
        public event EventHandler RotationChanged;

        /// <summary>
        /// Method that invokes a RotationChanged ivent.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnRotationChanged(EventArgs e)
        {
            var handler = RotationChanged;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// Adds a Component attached to a GameObject.
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns></returns>
        public Component AddComponent(string componentName)
        {
            Component newComponent = null;
            switch(componentName)
            {
                case "SpriteRenderer":
                    newComponent = new SpriteRenderer(this);
                    break;

                case "RigidBody2D":
                    newComponent = new RigidBody2D(this);
                    break;

                case "Animator":
                    newComponent = new Animator(this);
                    break;
            }

            components.Add(newComponent);

            return newComponent;
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
                if (@return != null && @return.GetType() == typeof(T))
                    return @return;
            }

            return null;
        }

        /// <summary>
        /// Runs once per frame. Independent from Game Physics.
        /// Uses a variable time-step.
        /// </summary>
        public virtual void Update(double deltaTime) { }
        /// <summary>
        /// Updates everything that is need to be applied to a Rigidbody.
        /// Uses a fixed time-step.
        /// </summary>
        public virtual void FixedUpdate(double fixedDeltaTime) { }


        /// <summary>
        /// Updates a game object and it's components
        /// over a variable (render) delta time interval.
        /// </summary>
        /// <param name="deltaTime"></param>
        internal void PerformUpdate(double deltaTime)
        {
            Update(deltaTime);
            foreach (var component in components)
            {
                if (!(component is IFixedUpdatable))
                    component.PerformComponent(deltaTime);
            }
        }

        /// <summary>
        /// Updates a game object and it's components
        /// over a fixed delta time interval.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        internal void PerformFixedUpdate(double fixedDeltaTime)
        {
            FixedUpdate(fixedDeltaTime);
            foreach (var component in components)
            {
                if (component is IFixedUpdatable)
                    component.PerformComponent(fixedDeltaTime);
            }
        }
    }
}
