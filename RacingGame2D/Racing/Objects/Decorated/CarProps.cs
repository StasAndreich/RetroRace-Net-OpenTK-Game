namespace Racing.Objects
{
    public class CarProps
    {
        public Car owner;

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

        public float MaxEngineForce { get; set; }

        public float MaxVelocity { get; set; }
        public float MaxVelocityReverse { get; set; }
        public float MaxSteeringAngle { get; set; }
        public float MaxBreakingForce { get; set; }

        public float MaxFuelLevel { get; set; }
        public float IdleFuelConsumption { get; set; }
        public float DrivingFuelConsumption { get; set; }
        public float FuelFillUp { get; set; }
    }
}
