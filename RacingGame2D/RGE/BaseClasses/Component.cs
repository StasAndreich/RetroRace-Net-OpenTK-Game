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

        /// <summary>
        /// Switches ON and OFF the current component.
        /// </summary>
        public bool IsEnabled { get; set; }


        public Component(GameObject gameObject)
        {
            this.attachedTo = gameObject;
            IsEnabled = true;
        }

        //public Component GetComponent()
        //{
            
        //}
    }
}
