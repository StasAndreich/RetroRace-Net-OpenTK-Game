﻿using RGEngine.Graphics;
using OpenTK.Input;
using OpenTK;
using RGEngine.Support;

namespace Racing.Objects
{
    /// <summary>
    /// Defines a player on a purple car.
    /// </summary>
    public class PurpleCar : Car
    {
        /// <summary>
        /// Default ctor.
        /// </summary>
        public PurpleCar()
        {
            base.id = "Purple";
            SetStartCarPosition(new Vector2(85f, 435f));

            var vehicleTexture = ContentLoader.LoadTexture(@"Contents\Cars\purple.png");
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
            UpdateGearboxState(Key.Keypad7, Key.Keypad9);
            GetUserInput(Key.Keypad8, Key.Keypad5, Key.Keypad4, Key.Keypad6);

            base.FixedUpdate(fixedDeltaTime);
        }
    }
}
