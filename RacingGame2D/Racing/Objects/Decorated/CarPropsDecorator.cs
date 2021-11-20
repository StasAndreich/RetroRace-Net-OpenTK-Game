using RGEngine;
using RGEngine.Support;

namespace Racing.Objects
{
    /// <summary>
    /// Desines a decorator for a car props.
    /// </summary>
    public abstract class CarPropsDecorator : CarProps
    {
        private const float TimeIntervalValue = 3f;

        /// <summary>
        /// Sets prize life timer.
        /// </summary>
        private readonly GameTimer _lifeTimer;

        /// <summary>
        /// Sets decorator life time period in a timer.
        /// </summary>
        /// <param name="baseProps"></param>
        public CarPropsDecorator(CarProps baseProps)
            : base(baseProps.Owner)
        {
            BaseProps = baseProps;
            Owner = baseProps.Owner;

            _lifeTimer = new GameTimer(TimeIntervalValue);
            _lifeTimer.Elapsed += (sender, e) => RemoveDecorator();

            EngineCore.AddGameObject(this);
        }

        /// <summary>
        /// Testing ctor.
        /// </summary>
        public CarPropsDecorator(CarProps baseProps, Car owner)
        {
            BaseProps = baseProps;
            Owner = owner;
        }

        /// <summary>
        /// Keeps base props object.
        /// </summary>
        protected CarProps BaseProps { get; set; }

        /// <summary>
        /// Overrides FixedUpdate with inner timer update.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            _lifeTimer.Update(fixedDeltaTime);
            base.FixedUpdate(fixedDeltaTime);
        }

        private void RemoveDecorator()
        {
            foreach (var gameObject in EngineCore.GameObjects)
            {
                if ((gameObject is Car car) && ReferenceEquals(gameObject, Owner))
                {
                    car.Properties = BaseProps;
                }
            }

            EngineCore.RemoveGameObject(this);
        }
    }
}
