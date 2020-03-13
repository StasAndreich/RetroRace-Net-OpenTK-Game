using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RGEngine.BaseClasses
{
    /// <summary>
    /// Base class for everything attached to GameObjects.
    /// </summary>
    public abstract class Component
    {
        /// <summary>
        /// The game object this component is attached to.
        /// </summary>
        public readonly GameObject attachedTo;


    }
}
