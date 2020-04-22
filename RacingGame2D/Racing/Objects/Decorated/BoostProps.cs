namespace Racing.Objects
{
    /// <summary>
    /// Defines props with applied fuel prize.
    /// </summary>
    public class BoostProps : CarPropsDecorator
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="props"></param>
        public BoostProps(CarProps props)
            : base(props)
        {
        }

        /// <summary>
        /// Overrides MaxVelocity prop.
        /// </summary>
        public override float MaxVelocity
        {
            get => baseProps.MaxVelocity * 1.5f;
            set => base.MaxVelocity = value; 
        }

        /// <summary>
        /// Overrides MaxEngineForce prop.
        /// </summary>
        public override float MaxEngineForce 
        { 
            get => baseProps.MaxEngineForce * 2f;
            set => base.MaxEngineForce = value; 
        }
    }
}
