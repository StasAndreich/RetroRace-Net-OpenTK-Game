using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;
using RGEngine.Support;

namespace Racing.Objects
{
    public class BlackCar : Car
    {
        public BlackCar()
        {
            base.id = "Black";
            SetStartCarPosition(new Vector2(85f, 325f));

            var vehicleTexture = ContentLoader.LoadTexture(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\cars\black.png");
            var vehicleSprite = new Sprite(vehicleTexture, new Vector2(0.4f, 0.4f),
                new Vector2(0f, 0f), 2);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite);
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState(Key.Q, Key.E);
            GetUserInput(Key.W, Key.S, Key.A, Key.D);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
