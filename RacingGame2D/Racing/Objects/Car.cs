﻿using System;
using OpenTK;
using OpenTK.Input;
using RGEngine.BaseClasses;
using RGEngine.Input;
using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.Support;


namespace Racing.Objects
{
    public abstract class Car : GameObject
    {
        public Car(string vehicleTexturePath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            rigidBody2D = AddComponent<RigidBody2D>();

            var vehicleTexture = ContentLoader.LoadTexture(vehicleTexturePath);
            var wheelTexture = ContentLoader.LoadTexture(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\wheel.png");

            // Set defaults for a default car.
            MaxEngineForce = 430000f;
            MaxVelocity = 400f;
            MaxVelocityReverse = -250f;
            MaxSteeringAngle = 20f;
            MaxSteeringSpeed = 220f;
            MaxBreakingForce = 500000f;

            // Set default wheelBase value and start car position.
            wheelBase = 170f;
            SetStartCarPosition(new Vector2(0f, 0f), wheelBase);

            // Set default RigidBody parameters for a basic Car object.
            rigidBody2D.mass = 1200f;
            rigidBody2D.dragCoefficient = 750f;


            var vehicleSprite = new Sprite(vehicleTexture, new Vector2(0.2f, 0.2f),
                new Vector2(0f, 0f), 2);
            var wheelSpriteLeft = new Sprite(wheelTexture, new Vector2(0.25f, 0.25f),
                new Vector2(wheelBase / 2f, -59f), 0);
            var wheelSpriteRight = new Sprite(wheelTexture, new Vector2(0.25f, 0.25f),
                new Vector2(wheelBase / 2f, 59f), 1);

            wheelSpriteLeft.PointOfRotation = this.Position;
            wheelSpriteRight.PointOfRotation = this.Position;

            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite,
                                                                        wheelSpriteLeft,
                                                                        wheelSpriteRight);

            rigidBody2D.colliders = ColliderBatch.CreateColliderBatch(new BoxCollider(135, 55));
        }


        protected float MaxEngineForce { get; set; }

        protected float MaxVelocity { get; set; }
        protected float MaxVelocityReverse { get; set; }
        protected float MaxSteeringAngle { get; set; }
        protected float MaxSteeringSpeed { get; set; }
        protected float MaxBreakingForce { get; set; }

        protected float wheelBase;

        protected float steeringAngle;
        protected float carDirectionAngle;
        protected float steeringSpeed;

        protected Vector2 frontWheelPosition;
        protected Vector2 backWheelPosition;

        protected int drivingMode;

        protected float MaxFuelAmount { get; set; }
        protected float FuelLevel { get; set; }

        protected const float minVelocityConstraint = 6f;
        protected const float minSteeringAngleConstraint = 3f;

        // GameObject Components.
        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody2D;

        private float prevCarDirectionAngle;
        

        public override void FixedUpdate(double fixedDeltaTime)
        {
            // Update Steering angle of front car wheels.
            steeringAngle += steeringSpeed * (float)fixedDeltaTime;
            // Update steering.
            if (this.steeringAngle >= MaxSteeringAngle)
                this.steeringAngle = MaxSteeringAngle;
            if (this.steeringAngle <= -MaxSteeringAngle)
                this.steeringAngle = -MaxSteeringAngle;

            if (this.steeringAngle >= -minSteeringAngleConstraint &&
                this.steeringAngle < minSteeringAngleConstraint)
            {
                this.steeringAngle = 0f;
            }

            backWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(carDirectionAngle),
                (float)Math.Sin(carDirectionAngle));

            frontWheelPosition += rigidBody2D.velocity * (float)fixedDeltaTime *
                new Vector2((float)Math.Cos(carDirectionAngle + MathHelper.DegreesToRadians(steeringAngle)),
                (float)Math.Sin(carDirectionAngle + MathHelper.DegreesToRadians(steeringAngle)));

            base.Position = (frontWheelPosition + backWheelPosition) / 2;
            carDirectionAngle = (float) Math.Atan2(frontWheelPosition.Y - backWheelPosition.Y,
                frontWheelPosition.X - backWheelPosition.X);

            if (carDirectionAngle > 0 && carDirectionAngle < Math.PI)
            {
                carDirectionAngle = carDirectionAngle - ((int)(carDirectionAngle / (Math.PI)) * (float)(Math.PI));
            }
            if (carDirectionAngle < 0 && carDirectionAngle < -Math.PI)
            {
                carDirectionAngle = carDirectionAngle + ((int)(carDirectionAngle / (Math.PI)) * (float)(Math.PI));
            }

            float dir;
            if (steeringAngle == 0f)
            {
                dir = carDirectionAngle;
                prevCarDirectionAngle = carDirectionAngle;
            }
            else
            {
                if (Math.Abs(carDirectionAngle - prevCarDirectionAngle) > 0)
                    dir = carDirectionAngle;
                else
                    dir = prevCarDirectionAngle;
            }


            spriteRenderer.RenderQueue[2].Rotation = MathHelper.RadiansToDegrees(dir);
            //spriteRenderer.RenderQueue[0].PointOfRotation = this.backWheelPosition;
            spriteRenderer.RenderQueue[0].Rotation = steeringAngle;
            //spriteRenderer.RenderQueue[1].PointOfRotation = this.backWheelPosition;
            spriteRenderer.RenderQueue[1].Rotation = steeringAngle;

            foreach (var collider in rigidBody2D.colliders)
            {
                collider.Update(MathHelper.RadiansToDegrees(dir));
            }
        }

        protected virtual void TakeUserInput(Key gas, Key brake, Key left, Key right)
        {
            // Check for max and min velocity-values.
            if (rigidBody2D.velocity > MaxVelocity)
                rigidBody2D.velocity = MaxVelocity;
            if (rigidBody2D.velocity < MaxVelocityReverse)
                rigidBody2D.velocity = MaxVelocityReverse;

            // Fixing a STOP-point.
            if (rigidBody2D.velocity >= -minVelocityConstraint &&
                rigidBody2D.velocity <= minVelocityConstraint)
            {
                rigidBody2D.velocity = 0f;
            }

            if (InputController.CurrentKeyboardState.IsKeyDown(gas))
            {
                if (rigidBody2D.velocity == 0f)
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;

                else if (rigidBody2D.velocity > 0f)
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;

                else
                    rigidBody2D.engineForce = drivingMode * MaxEngineForce;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(brake))
            {
                if (rigidBody2D.velocity == 0f)
                    rigidBody2D.breakingForce = 0f;

                else if (rigidBody2D.velocity > 0f)
                    rigidBody2D.breakingForce = MaxBreakingForce;

                else
                    rigidBody2D.breakingForce = -MaxBreakingForce;
            }
            else
            {
                rigidBody2D.breakingForce = 0f;
                rigidBody2D.engineForce = 0f;
            }


            // Update steering.
            if (this.steeringAngle >= MaxSteeringAngle)
                this.steeringAngle = MaxSteeringAngle;
            if (this.steeringAngle <= -MaxSteeringAngle)
                this.steeringAngle = -MaxSteeringAngle;

            if (this.steeringAngle >= -minSteeringAngleConstraint &&
                this.steeringAngle < minSteeringAngleConstraint)
            {
                this.steeringAngle = 0f;
            }

            if (InputController.CurrentKeyboardState.IsKeyDown(left))
            {
                this.steeringSpeed = -MaxSteeringSpeed;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(right))
            {
                this.steeringSpeed = MaxSteeringSpeed;
            }
            else
            {
                if (this.steeringAngle == 0f)
                    this.steeringSpeed = 0f;
                else if (this.steeringAngle > 0f)
                    this.steeringSpeed = -MaxSteeringSpeed;
                else
                    this.steeringSpeed = MaxSteeringSpeed;
            }
        }

        protected virtual void UpdateGearboxState(Key reverse, Key drive)
        {
            if (InputController.CurrentKeyboardState.IsKeyDown(drive))
            {
                drivingMode = (int)DrivingModes.Drive;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(reverse))
            {
                drivingMode = (int)DrivingModes.Reverse;
            }
        }

        protected virtual void SetStartCarPosition(Vector2 startPosition, float carWheelBaseLength)
        {
            base.Position = startPosition;
            this.frontWheelPosition = base.Position +
                carWheelBaseLength / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheelPosition = base.Position -
                carWheelBaseLength / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
        }
    }

    public enum DrivingModes
    {
        /// <summary>
        /// Returns -1 cause all forces are in 'negative' direction.
        /// </summary>
        Reverse = -1,
        Neutral = 0,
        /// <summary>
        /// Returns 1 cause all forces are in 'positive' direction.
        /// </summary>
        Drive = 1
    }
}
