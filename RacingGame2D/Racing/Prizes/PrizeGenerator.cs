using OpenTK;
using RGEngine;
using RGEngine.Graphics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using System;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines a class responsible for a prize generation.
    /// </summary>
    public class PrizeGenerator : GameObject, IPrizeFactory, INonRenderable
    {
        /// <summary>
        /// Sets the geteration interval and generation area bounds.
        /// </summary>
        public PrizeGenerator()
        {
            spawnTimer = new GameTimer(4f);
            spawnTimer.Elapsed += (sender, e) => AddPrizeOnScene();

            rect1min = new Vector2(-870, 450);
            rect1max = new Vector2(870, -450);
            rect2min = new Vector2(-730, 310);
            rect2max = new Vector2(730, -310);
        }
        
        private GameTimer spawnTimer;

        // Rect1 is outer bound.
        // Rect2 is inner bound.
        private Vector2 rect1min;
        private Vector2 rect1max;
        private Vector2 rect2min;
        private Vector2 rect2max;


        private void AddPrizeOnScene()
        {
            var rand = new Random();
            var prize = GeneratePrize(rand);
            if (prize != null)
            {
                prize.Position = GetPrizePosition(rand);
                EngineCore.AddGameObject(prize);
            }
        }

        /// <summary>
        /// Main Fabric Method.
        /// </summary>
        public Prize GeneratePrize(Random random)
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

        /// <summary>
        /// Overrides FixedUpdate with updating inner timer.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            spawnTimer?.Update(fixedDeltaTime);
            base.FixedUpdate(fixedDeltaTime);
        }

        /// <summary>
        /// Generates random prize spawn position.
        /// </summary>
        /// <returns></returns>
        private Vector2 GetPrizePosition(Random random)
        {
            int randX;
            int randY;

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
