using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Support;
using System.Linq;

namespace Racing.Objects
{
    /// <summary>
    /// Desines a decorator for a car props.
    /// </summary>
    public abstract class CarPropsDecorator : CarProps
    {
        /// <summary>
        /// Keeps base props object.
        /// </summary>
        protected CarProps baseProps;
        /// <summary>
        /// Sets prize life timer.
        /// </summary>
        protected GameTimer lifeTimer;

        /// <summary>
        /// Sets decorator life time period in a timer.
        /// </summary>
        /// <param name="baseProps"></param>
        public CarPropsDecorator(CarProps baseProps)
            : base(baseProps.owner)
        {
            this.baseProps = baseProps;
            base.owner = baseProps.owner;

            this.lifeTimer = new GameTimer(3f);
            this.lifeTimer.Elapsed += (sender, e) => RemoveDecorator();

            EngineCore.AddGameObject(this);
        }


        /// <summary>
        /// Overrides FixedUpdate with inner timer update.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            lifeTimer.Update(fixedDeltaTime);
            base.FixedUpdate(fixedDeltaTime);
        }

        private void RemoveDecorator()
        {
            var list = EngineCore.gameObjects.ToList<GameObject>();
            foreach (var @object in list)
            {
                if ((@object is Car) && ReferenceEquals(@object, base.owner))
                {
                    var car = (Car)@object;
                    car.properties = this.baseProps;
                }
            }
            EngineCore.RemoveGameObject(this);
        }
    }
}
