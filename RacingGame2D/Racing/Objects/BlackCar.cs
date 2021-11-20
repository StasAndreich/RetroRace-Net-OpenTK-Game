using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;
using RGEngine.Support;
using Racing.Constants;

namespace Racing.Objects
{
    /// <summary>
    /// Defines a player on a black car.
    /// </summary>
    public class BlackCar : Car
    {
        public BlackCar()
            : base()
        {
            InitializeCar();
        }

        public BlackCar(bool isPlayable)
            : base(isPlayable)
        {
            InitializeCar();
        }

        /// <summary>
        /// Override of FixedUpdate with new user input.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            if (IsControlledByLocalUser)
            {
                UpdateGearboxState(Key.Q, Key.E);
                GetUserInput(Key.W, Key.S, Key.A, Key.D);
            }

            base.FixedUpdate(fixedDeltaTime);
        }

        private void InitializeCar()
        {
            Id = CarConstants.BlackCarName;
            SetStartCarPosition(new Vector2(85f, 325f));

            var vehicleTexture = ContentLoader.LoadTexture(@"Contents\Cars\black.png");
            var vehicleSprite = new Sprite(
                vehicleTexture,
                Position,
                CarConstants.DefaultSpriteScale,
                new Vector2(0f, 0f),
                2);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite);
        }
    }
}
