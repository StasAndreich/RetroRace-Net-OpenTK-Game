using System;

namespace RGEngine.Support
{
    /// <summary>
    /// 
    /// </summary>
    public class GameTimer
    {
        /// <summary>
        /// Basic ctor that init timer with 0 interval.
        /// </summary>
        public GameTimer()
        {
            this.Interval = 0;
        }

        /// <summary>
        /// Basic ctor that init timer with param interval value.
        /// </summary>
        /// <param name="interval"></param>
        public GameTimer(double interval)
        {
            this.Interval = interval;
        }

        private double elapsedTime;
        
        /// <summary>
        /// Keeps time-interval value.
        /// </summary>
        public double Interval { get; set; }

        /// <summary>
        /// Keeps the number of invokations of a timer Elapsed event.
        /// </summary>
        public int Invokations { get; private set; }

        /// <summary>
        /// Event that raised when timer interval elapsed.
        /// </summary>
        public event EventHandler Elapsed;

        /// <summary>
        /// Updates the timer with deltaTime value.
        /// </summary>
        /// <param name="deltaTime"></param>
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
