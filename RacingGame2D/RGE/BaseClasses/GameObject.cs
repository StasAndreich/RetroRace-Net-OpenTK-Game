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

        ///// <summary>
        ///// Stores a list of the components with Update()
        ///// </summary>
        //private List<Component> componentsWithUpd;

        //private List<Component> componentsWithFixedUpd;

        public Vector2 Position { get; set; }


        //protected T AddComponent<T>()
        //{
        //    return ;
        //}


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
