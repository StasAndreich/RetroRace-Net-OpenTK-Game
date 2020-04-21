﻿using OpenTK;
using RGEngine;
using RGEngine.Graphics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using System;
using System.Drawing;

namespace Racing.Prizes
{
    public class PrizeGenerator : GameObject, IPrizeFactory, INonRenderable
    {
        public PrizeGenerator()
        {
            spawnTimer = new GameTimer();
            spawnTimer.Interval = 7f;
            spawnTimer.Elapsed += (sender, e) => AddPrizeOnScene();

            rect1min = new Vector2(-870, 450);
            rect1max = new Vector2(870, -450);
            rect2min = new Vector2(-730, 310);
            rect2max = new Vector2(730, -310);
        }
        
        private GameTimer spawnTimer;
        //private Vector2[] spawnPositions =
        //{
        //    new Vector2(800f, 400f),
        //    new Vector2(805f, -350f),
        //    new Vector2(-350f, -350f),
        //    new Vector2(-550f, 405f),

        //    new Vector2(-800f, 0f),
        //    new Vector2(805f, 0f),
        //    new Vector2(-10f, -380f),
        //    new Vector2(-330f, 405f),
        //};

        // Rect1 is outer bound.
        // Rect2 is inner bound.
        private Vector2 rect1min;
        private Vector2 rect1max;
        private Vector2 rect2min;
        private Vector2 rect2max;


        private void AddPrizeOnScene()
        {
            var prize = GeneratePrize();
            if (prize != null)
            {
                prize.Position = GetPrizePosition();
                EngineCore.AddGameObject(prize);
            }
        }

        /// <summary>
        /// Main Fabric Method.
        /// </summary>
        public Prize GeneratePrize()
        {
            // Limit the number of prizes located
            // on scene at the same time.
            var prizesOnScene = 0;
            foreach (var @object in EngineCore.gameObjects)
            {
                if (@object is Prize)
                    prizesOnScene++;
            }

            if (prizesOnScene > 4)
                return null;

            
            var random = new Random();
            var prizesCount = 3;
            var randPrize = random.Next(1, prizesCount + 1);

            switch (randPrize)
            {
                case (int)Prizes.Fuel:
                    return new FuelPrize();

                case (int)Prizes.Boost:
                    return new BoostPrize();

                case (int)Prizes.Slowdown:
                    return new SlowdownPrize();

                default:
                    throw new Exception("There is no such a prize number.");
            }
        }

        public override void FixedUpdate(double fixedDeltaTime)
        {
            spawnTimer?.Update(fixedDeltaTime);
            base.FixedUpdate(fixedDeltaTime);
        }

        /// <summary>
        /// Generates random prize spawn position.
        /// </summary>
        /// <returns></returns>
        private Vector2 GetPrizePosition()
        {
            var randX = 0;
            var randY = 0;

            var random = new Random();
            var randRegion = random.Next(4);

            // INVERSED Y-axis.
            switch (randRegion)
            {
                case 0:
                    randX = random.Next((int)rect1min.X, (int)rect1max.X);
                    randY = random.Next((int)rect1max.Y, (int)rect2max.Y);
                    break;
                case 1:
                    randX = random.Next((int)rect1min.X, (int)rect2min.X);
                    randY = random.Next((int)rect2max.Y, (int)rect2min.Y);
                    break;
                case 2:
                    randX = random.Next((int)rect2max.X, (int)rect1max.X);
                    randY = random.Next((int)rect2max.Y, (int)rect2min.Y);
                    break;
                case 3:
                    randX = random.Next((int)rect1min.X, (int)rect1max.X);
                    randY = random.Next((int)rect2min.Y, (int)rect1min.Y);
                    break;
                default:
                    throw new Exception("Random out of range.");
            }

            return new Vector2(randX, randY);
        }
    }
}
