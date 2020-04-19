using RGEngine.Support;
using System;
using System.Runtime.InteropServices;

namespace Racing.Objects
{
    public abstract class CarPropsDecorator : CarProps
    {
        protected CarProps baseProps;
        protected GameTimer lifeTimer;

        public CarPropsDecorator(CarProps baseProps)
        {
            this.baseProps = baseProps;
            this.lifeTimer = new GameTimer();
            this.lifeTimer.Interval = 4f;
            this.lifeTimer.Elapsed += (sender, e) => OnExpired(new EventArgs());
        }

        public event EventHandler Expired;

        protected virtual void OnExpired(EventArgs e)
        {
            var handler = Expired;
            handler?.Invoke(this, e);
        }

        private void RemoveDecorator()
        {
            this.baseProps = new CarProps();
        }
    }
}
