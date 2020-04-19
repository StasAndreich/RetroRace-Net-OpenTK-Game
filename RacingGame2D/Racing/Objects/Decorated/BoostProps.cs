namespace Racing.Objects
{
    public class BoostProps : CarPropsDecorator
    {
        public BoostProps(CarProps props)
            : base(props)
        {
            props.MaxVelocity *= 1.5f;
            props.MaxEngineForce *= 2f;
        }
    }
}
