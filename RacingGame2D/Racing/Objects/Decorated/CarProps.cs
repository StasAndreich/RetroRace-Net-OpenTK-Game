using RGEngine.BaseClasses;
using RGEngine.Graphics;
using RGEngine.Physics;

namespace Racing.Objects
{
    /// <summary>
    /// Defines a main set of car properties.
    /// </summary>
    public class CarProps : GameObject, INonResolveable, INonRenderable
    {
        /// <summary>
        /// Object-owner.
        /// </summary>
        public Car owner;

        /// <summary>
        /// Init with default values.
        /// </summary>
        /// <param name="owner"></param>
        public CarProps(Car owner)
        {
            this.owner = owner;

            MaxEngineForce = 430000f;
            MaxVelocity = 480f;
            MaxVelocityReverse = -250f;
            MaxSteeringAngle = 25f;
            MaxBreakingForce = 500000f;

            MaxFuelLevel = 40f;
            IdleFuelConsumption = 1f;
            DrivingFuelConsumption = 3f;
            FuelFillUp = 0f;
        }

        /// <summary>
        /// Defines max engine force.
        /// </summary>
        public virtual float MaxEngineForce { get; set; }
        /// <summary>
        /// Defines max velocity.
        /// </summary>
        public virtual float MaxVelocity { get; set; }
        /// <summary>
        /// Defines max velocity in reverse.
        /// </summary>
        public float MaxVelocityReverse { get; set; }
        /// <summary>
        /// Defines max steering angle.
        /// </summary>
        public float MaxSteeringAngle { get; set; }
        /// <summary>
        /// Defines max breaking force.
        /// </summary>
        public float MaxBreakingForce { get; set; }

        /// <summary>
        /// Defines max fuel level.
        /// </summary>
        public float MaxFuelLevel { get; set; }
        /// <summary>
        /// Defines idle fuel consumption.
        /// </summary>
        public float IdleFuelConsumption { get; set; }
        /// <summary>
        /// Defines driving fuel consumption.
        /// </summary>
        public float DrivingFuelConsumption { get; set; }
        /// <summary>
        /// Defines additional fuel from can.
        /// </summary>
        public virtual float FuelFillUp { get; set; }
    }
}
