namespace Racing.Objects
{
    public class SlowdownProps : CarPropsDecorator
    {
        public SlowdownProps(CarProps props)
            : base(props)
        {
            props.MaxVelocity /= 1.5f;
            props.MaxEngineForce /= 2f;
        }
    }
}
