using OpenTK;
using RGEngine;
using RGEngine.Graphics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using System;

namespace Racing.Prizes
{
    public class PrizeGenerator : GameObject, IPrizeFactory, INonRenderable
    {
        public PrizeGenerator()
        {
            spawnTimer = new GameTimer();
            spawnTimer.Interval = 7f;
            spawnTimer.Elapsed += (sender, e) => AddPrizeOnScene();
        }
        
        private GameTimer spawnTimer;
        private Vector2[] spawnPositions =
        {
            new Vector2(800f, 400f),
            new Vector2(805f, -350f),
            new Vector2(-350f, -350f),
            new Vector2(-550f, 405f),

            new Vector2(-800f, 0f),
            new Vector2(805f, 0f),
            new Vector2(-10f, -380f),
            new Vector2(-330f, 405f),
        };


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
            var randPrize = random.Next(1, prizesCount);

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
            var random = new Random();
            var randSpawnPos = random.Next(0, this.spawnPositions.Length);

            foreach (var @object in EngineCore.gameObjects)
            {
                if (@object is Prize)
                {
                    if (Math.Abs(@object.Position.X - this.spawnPositions[randSpawnPos].X) < 0.01f &&
                        Math.Abs(@object.Position.Y - this.spawnPositions[randSpawnPos].Y) < 0.01f)
                    {
                        randSpawnPos = random.Next(0, this.spawnPositions.Length);
                    }
                }
            }

            return this.spawnPositions[randSpawnPos];
        }
    }
}
