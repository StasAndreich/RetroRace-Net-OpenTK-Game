namespace Racing.Objects
{
    public class FuelProps : CarPropsDecorator
    {
        public FuelProps(CarProps props)
            : base(props)
        {
            props.FuelFillUp += 10f;
        }
    }
}
