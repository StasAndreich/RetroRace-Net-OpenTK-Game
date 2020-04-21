namespace Racing.Objects
{
    public class SlowdownProps : CarPropsDecorator
    {
        public SlowdownProps(CarProps props)
            : base(props)
        {
        }

        public override float MaxVelocity
        {
            get => baseProps.MaxVelocity / 1.5f;
            set => base.MaxVelocity = value;
        }

        public override float MaxEngineForce
        {
            get => baseProps.MaxEngineForce / 2f;
            set => base.MaxEngineForce = value;
        }
    }
}
