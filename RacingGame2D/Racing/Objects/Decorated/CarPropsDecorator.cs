using RGEngine;
using RGEngine.BaseClasses;
using RGEngine.Support;
using System;
using System.Linq;

namespace Racing.Objects
{
    public abstract class CarPropsDecorator : CarProps
    {
        protected CarProps baseProps;
        protected GameTimer lifeTimer;

        public CarPropsDecorator(CarProps baseProps)
            : base(baseProps.owner)
        {
            this.baseProps = baseProps;
            base.owner = baseProps.owner;

            this.lifeTimer = new GameTimer();
            this.lifeTimer.Interval = 3f;
            this.lifeTimer.Elapsed += (sender, e) => RemoveDecorator();

            EngineCore.AddGameObject(this);
        }


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
