namespace Racing.Objects
{
    /// <summary>
    /// Defines props with applied slowdown prize.
    /// </summary>
    public class SlowdownProps : CarPropsDecorator
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="props"></param>
        public SlowdownProps(CarProps props)
            : base(props)
        {
        }

        /// <summary>
        /// Testing ctor.
        /// </summary>
        /// <param name="props"></param>
        /// <param name="owner"></param>
        public SlowdownProps(CarProps props, Car owner)
            : base(props, owner)
        {
        }

        /// <summary>
        /// Overrides MaxVelocity prop.
        /// </summary>
        public override float MaxVelocity
        {
            get => baseProps.MaxVelocity / 1.5f;
            set => base.MaxVelocity = value;
        }
    }
}
