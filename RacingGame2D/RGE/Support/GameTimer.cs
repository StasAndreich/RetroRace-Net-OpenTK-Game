using System;

namespace RGEngine.Support
{
    public class GameTimer
    {
        public GameTimer(double interval)
        {
            this.Interval = interval;
        }

        private double elapsedTime;
        
        public double Interval { get; private set; }

        public int Invokations { get; private set; }

        public event EventHandler Elapsed;


        public void Update(double deltaTime)
        {
            elapsedTime += deltaTime;

            if (Math.Abs(elapsedTime - Interval) <= 0.01f)
            {
                Elapsed?.Invoke(this, new EventArgs());
                Invokations += 1;
                elapsedTime = 0;
            }
        }
    }
}
