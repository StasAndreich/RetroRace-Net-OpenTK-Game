namespace Racing.Objects
{
    /// <summary>
    /// Defines props with applied fuel prize.
    /// </summary>
    public class FuelProps : CarPropsDecorator
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        /// <param name="props"></param>
        public FuelProps(CarProps props)
            : base(props)
        {
            props.FuelFillUp += 10f;
        }
    }
}
