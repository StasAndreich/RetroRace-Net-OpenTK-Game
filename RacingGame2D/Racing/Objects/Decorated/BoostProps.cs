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
        /// Testing ctor.
        /// </summary>
        /// <param name="props"></param>
        /// <param name="owner"></param>
        public BoostProps(CarProps props, Car owner)
            : base(props, owner)
        {
        }

        /// <summary>
        /// Overrides MaxVelocity prop.
        /// </summary>
        public override float MaxVelocity
        {
            get => BaseProps.MaxVelocity * 1.5f;
            set => base.MaxVelocity = value; 
        }

        /// <summary>
        /// Overrides MaxEngineForce prop.
        /// </summary>
        public override float MaxEngineForce 
        { 
            get => BaseProps.MaxEngineForce * 2f;
            set => base.MaxEngineForce = value; 
        }
    }
}
