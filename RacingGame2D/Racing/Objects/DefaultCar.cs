using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;


namespace Racing.Objects
{
    public class DefaultCar : Car
    {
        public DefaultCar(string vehicleTexturePath)
            : base(vehicleTexturePath)
        {
            rigidBody2D.mass = 1200f;
            rigidBody2D.dragCoefficient = 750f;

            MaxEngineForce = 400000f;
            MaxVelocity = 520f;
            MaxVelocityReverse = -250f;
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState();

            // Velocity constraints.
            if (rigidBody2D.velocity > MaxVelocity)
            {
                rigidBody2D.velocity = MaxVelocity;
            }
            if (rigidBody2D.velocity < MaxVelocityReverse)
            {
                rigidBody2D.velocity = MaxVelocityReverse;
            }

            // Fixing a STOP-point.
            if (rigidBody2D.velocity >= -2.5f && rigidBody2D.velocity <= 2.5f)
            {
                rigidBody2D.velocity = 0f;
            }


            if (InputController.CurrentKeyboardState.IsKeyDown(Key.W))
            {
                if (rigidBody2D.velocity == 0f)
                {
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;

                }
                else if (rigidBody2D.velocity > 0f)
                {
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;
                }
                else
                {
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;
                }
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.S))
            {
                if (rigidBody2D.velocity == 0f)
                {
                    rigidBody2D.breakingForce = 0f;
                }
                else if (rigidBody2D.velocity > 0f)
                {
                    rigidBody2D.breakingForce = 500000f;
                    // make all the numbers  to a props of an object init in construct
                }
                else
                {
                    rigidBody2D.breakingForce = -500000f;
                }
            }
            else
            {
                rigidBody2D.breakingForce = 0f;
                rigidBody2D.engineForce = 0f;
            }



            if (this.steeringAngle > MaxSteeringAngle)
                this.steeringAngle = MaxSteeringAngle;
            if (this.steeringAngle < -MaxSteeringAngle)
                    this.steeringAngle = -MaxSteeringAngle;


            if (this.steeringAngle >= -1f && this.steeringAngle <= 1f)
            {
                this.steeringAngle = 0f;
            }



            if (InputController.CurrentKeyboardState.IsKeyDown(Key.A))
            {
                base.steeringConstant = -80f;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(Key.D))
            {
                base.steeringConstant = 80f;
            }
            else
            {
                if (this.steeringAngle == 0f)
                    base.steeringConstant = 0f;
                else if (this.steeringAngle > 0f)
                    base.steeringConstant = -80f;
                else
                    base.steeringConstant = 80f;
            }

            UpdateCarSteering(fixedDeltaTime);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
