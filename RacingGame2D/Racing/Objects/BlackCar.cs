using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;
using RGEngine.Support;

namespace Racing.Objects
{
    /// <summary>
    /// Defines a player on a black car.
    /// </summary>
    public class BlackCar : Car
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public BlackCar()
        {
            base.id = "Black";
            SetStartCarPosition(new Vector2(85f, 325f));

            var vehicleTexture = ContentLoader.LoadTexture(@"Contents\Cars\black.png");
            var vehicleSprite = new Sprite(vehicleTexture, Position, new Vector2(0.4f, 0.4f),
                new Vector2(0f, 0f), 2);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite);
        }

        /// <summary>
        /// Override of FixedUpdate with new user input.
        /// </summary>
        /// <param name="fixedDeltaTime"></param>
        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState(Key.Q, Key.E);
            GetUserInput(Key.W, Key.S, Key.A, Key.D);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
