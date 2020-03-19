using System;
using System.Collections.Generic;
using OpenTK;


namespace RGEngine.BaseClasses
{
    /// <summary>
    /// Base class for all entities in RGEngine scenes.
    /// </summary>
    public class GameObject
    {
        private List<Component> components;

        public Vector2 Position { get; }

        public GameObject()
        {

        }
    }
}
