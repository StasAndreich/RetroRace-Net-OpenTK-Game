using RGEngine.Input;
using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;
using RGEngine.Physics;
using RGEngine.Support;

namespace Racing.Objects
{
    public class PurpleCar : Car
    {
        public PurpleCar()
        {
            base.id = "Purple";
            SetStartCarPosition(new Vector2(0f, 400f));

            var vehicleTexture = ContentLoader.LoadTexture(@"C:\Users\smedy\OneDrive\C4D\retro\launcher\cars\purple.png");
            var vehicleSprite = new Sprite(vehicleTexture, new Vector2(0.4f, 0.4f),
                new Vector2(0f, 0f), 2);
            spriteRenderer.RenderQueue = SpriteBatch.CreateSpriteBatch(vehicleSprite);
        }


        public override void FixedUpdate(double fixedDeltaTime)
        {
            UpdateGearboxState(Key.Keypad7, Key.Keypad9);
            GetUserInput(Key.Keypad8, Key.Keypad5, Key.Keypad4, Key.Keypad6);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
