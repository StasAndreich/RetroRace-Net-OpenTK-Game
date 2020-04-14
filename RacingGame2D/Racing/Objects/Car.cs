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

            // Set defaults for a default car.
            MaxEngineForce = 430000f;
            MaxVelocity = 480f;
            MaxVelocityReverse = -250f;
            MaxSteeringAngle = 25f;
            MaxBreakingForce = 500000f;

            // Set default wheelBase value and start car position.
            wheelBase = 80f;
            SetStartCarPosition(new Vector2(50f, 50f));

            // Set default RigidBody parameters for a basic Car object.
            rigidBody2D.mass = 1200f;
            rigidBody2D.frictionConst = 750f;


            var vehicleTexture = ContentLoader.LoadTexture(vehicleTexturePath);
            var wheelTexture = ContentLoader.LoadTexture(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\wheel.png");

            var vehicleSprite = new Sprite(vehicleTexture, new Vector2(0.2f, 0.2f),
                new Vector2(0f, 300f), 2);
            var wheelSpriteLeft = new Sprite(wheelTexture, new Vector2(0.4f, 0.4f),
                new Vector2(0f, 0f), 0);
            var wheelSpriteRight = new Sprite(wheelTexture, new Vector2(0.25f, 0.25f),
                new Vector2(0f, 0f), 1);

            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite,
                                                                        wheelSpriteLeft,
                                                                        wheelSpriteRight);

            rigidBody2D.colliders = ColliderBatch.CreateColliderBatch(new BoxCollider(140, 60));
        }


        // GameObject Components.
        protected SpriteRenderer spriteRenderer;
        protected RigidBody2D rigidBody2D;

        protected float MaxEngineForce { get; set; }

        protected float MaxVelocity { get; set; }
        protected float MaxVelocityReverse { get; set; }
        protected float MaxSteeringAngle { get; set; }
        protected float MaxBreakingForce { get; set; }

        protected float wheelBase;

        protected float steeringAngle;
        protected float carDirectionAngle;

        protected Vector2 frontWheel;
        protected Vector2 backWheel;

        protected int drivingMode;

        protected float MaxFuelLevel { get; set; }

        private float fuelLevel;
        protected float FuelLevel
        {
            get => this.fuelLevel;
            set
            {
                if (this.fuelLevel + value > MaxFuelLevel)
                    this.fuelLevel = MaxFuelLevel;
                else if (this.fuelLevel + value < 0)
                    this.fuelLevel = 0;
                else
                    this.fuelLevel += value;
            }
        }

        protected const float velocityConstraint = 6f;


        public override void FixedUpdate(double fixedDeltaTime)
        {
            var steer = MathHelper.DegreesToRadians(steeringAngle);

            backWheel.X += rigidBody2D.velocity * (float)fixedDeltaTime *
                (float)Math.Cos(carDirectionAngle);
            backWheel.Y += rigidBody2D.velocity * (float)fixedDeltaTime *
                (float)Math.Sin(carDirectionAngle);

            // Calculations that push the front wheel forward and
            // conserve the wheelBase distance.
            var distance = rigidBody2D.velocity * (float)fixedDeltaTime;
            var B = (wheelBase - distance) * Math.Cos(steer);
            var C = distance * (2 * wheelBase - distance);
            var calc = Math.Sqrt(B * B + C) - B;

            frontWheel.X += (float)calc *
                (float)Math.Cos(carDirectionAngle + steer);
            frontWheel.Y += (float)calc *
                (float)Math.Sin(carDirectionAngle + steer);

            base.Position = (frontWheel + backWheel) / 2;
            carDirectionAngle = (float)Math.Atan2(frontWheel.Y - backWheel.Y,
                frontWheel.X - backWheel.X);

            //spriteRenderer.RenderQueue[0].Position = frontWheel;
            //spriteRenderer.RenderQueue[1].Position = backWheel;
            spriteRenderer.RenderQueue[2].Rotation = MathHelper.RadiansToDegrees(carDirectionAngle);
            //spriteRenderer.RenderQueue[2].Position = this.Position;

            foreach (var collider in rigidBody2D.colliders)
            {
                collider.Update(MathHelper.RadiansToDegrees(carDirectionAngle));
            }
        }

        protected virtual void GetUserInput(Key gas, Key brake, Key left, Key right)
        {
            // Check for max and min velocity-values.
            if (rigidBody2D.velocity >= MaxVelocity)
                rigidBody2D.velocity = MaxVelocity;
            if (rigidBody2D.velocity <= MaxVelocityReverse)
                rigidBody2D.velocity = MaxVelocityReverse;

            // Fixing a STOP-point.
            if (rigidBody2D.velocity <= velocityConstraint &&
                rigidBody2D.velocity >= -velocityConstraint)
                rigidBody2D.velocity = 0;


            if (InputController.CurrentKeyboardState.IsKeyDown(gas))
            {
                rigidBody2D.engineForce = drivingMode * MaxEngineForce;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(brake))
            {
                if (rigidBody2D.velocity == 0)
                    rigidBody2D.breakingForce = 0;
                else if (rigidBody2D.velocity > 0)
                    rigidBody2D.breakingForce = MaxBreakingForce;
                else
                    rigidBody2D.breakingForce = -MaxBreakingForce;
            }
            else
            {
                rigidBody2D.breakingForce = 0;
                rigidBody2D.engineForce = 0;
            }


            if (InputController.CurrentKeyboardState.IsKeyDown(left))
            {
                this.steeringAngle = -MaxSteeringAngle;
            }
            else if (InputController.CurrentKeyboardState.IsKeyDown(right))
            {
                this.steeringAngle = MaxSteeringAngle;
            }
            else
            {
                this.steeringAngle = 0f;
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

        protected virtual void SetStartCarPosition(Vector2 startPosition)
        {
            base.Position = startPosition;
            this.frontWheel = base.Position +
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
            this.backWheel = base.Position -
                this.wheelBase / 2 * new Vector2((float)Math.Cos(carDirectionAngle), (float)Math.Sin(carDirectionAngle));
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
