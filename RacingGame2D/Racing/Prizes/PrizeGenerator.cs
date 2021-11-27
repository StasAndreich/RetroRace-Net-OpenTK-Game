using OpenTK;
using RGEngine;
using RGEngine.Graphics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using System;
using RGEngine.Multiplayer;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines a class responsible for a prize generation.
    /// </summary>
    public class PrizeGenerator : GameObject, IPrizeFactory, INonRenderable
    {

        private const int PrizesCount = 3;

        // Rect1 is outer bound.
        // Rect2 is inner bound.
        private Vector2 _rect1min;
        private Vector2 _rect1max;
        private Vector2 _rect2min;
        private Vector2 _rect2max;

        private GameTimer _spawnTimer;
        private bool _isHost;

        /// <summary>
        /// Sets the geteration interval and generation area bounds.
        /// </summary>
        public PrizeGenerator(bool isHost)
        {
            _isHost = isHost;
            // is host
            // prize type in message
            if (isHost)
            {
                _spawnTimer = new GameTimer(5f);
                _spawnTimer.Elapsed += (sender, e) => AddPrizeOnScene();

                _rect1min = new Vector2(-870, 450);
                _rect1max = new Vector2(870, -450);
                _rect2min = new Vector2(-730, 310);
                _rect2max = new Vector2(730, -310);
            }
        }

        private void AddPrizeOnScene()
        {
            var rand = new Random();
            var prize = GeneratePrize(rand, GetPrizePosition(rand));
            if (prize != null)
            {
                EngineCore.AddGameObject(prize);
            }
        }

        /// <summary>
        /// Main Fabric Method.
        /// </summary>
        public Prize GeneratePrize(Random random, Vector2 position)
        {
            // Limit the number of prizes located
            // on scene at the same time.
            var prizesOnScene = 0;
            foreach (var gameObject in EngineCore.GameObjects)
            {
                if (gameObject is Prize)
                {
                    prizesOnScene++;
                }
            }

            if (prizesOnScene > 4)
            {
                return null;
            }

            var randPrize = random.Next(1, PrizesCount + 1);
            UdpHandlerObject.MessageToSend.PrizeType = randPrize;
            UdpHandlerObject.MessageToSend.PrizePosition = position;

            return randPrize switch
            {
                (int)PrizesTypes.Fuel => new FuelPrize(position),
                (int)PrizesTypes.Boost => new BoostPrize(position),
                (int)PrizesTypes.Slowdown => new SlowdownPrize(position),
                _ => throw new Exception("There is no such a prize number."),
            };
        }

        /// <summary>
        /// Overrides FixedUpdate with updating inner timer.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            if (_isHost)
            {
                _spawnTimer?.Update(fixedDeltaTime);
            }
            else
            {
                if (UdpHandlerObject.ReceivedMessage.PrizeType != 0)
                {
                    Prize prize = (PrizesTypes)UdpHandlerObject.ReceivedMessage.PrizeType switch
                    {
                        PrizesTypes.Fuel => new FuelPrize(UdpHandlerObject.ReceivedMessage.PrizePosition),
                        PrizesTypes.Boost => new BoostPrize(UdpHandlerObject.ReceivedMessage.PrizePosition),
                        _ => new SlowdownPrize(UdpHandlerObject.ReceivedMessage.PrizePosition),
                    };

                    if (prize != null)
                    {
                        EngineCore.AddGameObject(prize);
                    }

                    //UdpHandlerObject.ReceivedMessage.PrizeType = 0;
                    //UdpHandlerObject.ReceivedMessage.PrizePosition = Vector2.Zero;
                }
                UdpHandlerObject.ReceivedMessage.PrizeType = 0;
                UdpHandlerObject.ReceivedMessage.PrizePosition = Vector2.Zero;
            }

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
                    randX = random.Next((int)_rect1min.X, (int)_rect1max.X);
                    randY = random.Next((int)_rect1max.Y, (int)_rect2max.Y);
                    break;
                case 1:
                    randX = random.Next((int)_rect1min.X, (int)_rect2min.X);
                    randY = random.Next((int)_rect2max.Y, (int)_rect2min.Y);
                    break;
                case 2:
                    randX = random.Next((int)_rect2max.X, (int)_rect1max.X);
                    randY = random.Next((int)_rect2max.Y, (int)_rect2min.Y);
                    break;
                case 3:
                    randX = random.Next((int)_rect1min.X, (int)_rect1max.X);
                    randY = random.Next((int)_rect2min.Y, (int)_rect1min.Y);
                    break;
                default:
                    throw new Exception("Random out of range.");
            }

            return new Vector2(randX, randY);
        }
    }
}
