namespace Racing.Objects
{
    public class BoostProps : CarPropsDecorator
    {
        public BoostProps(CarProps props)
            : base(props)
        {
        }

        public override float MaxVelocity
        {
            get => baseProps.MaxVelocity * 1.5f;
            set => base.MaxVelocity = value; 
        }

        public override float MaxEngineForce 
        { 
            get => baseProps.MaxEngineForce * 2f;
            set => base.MaxEngineForce = value; 
        }
    }
}
