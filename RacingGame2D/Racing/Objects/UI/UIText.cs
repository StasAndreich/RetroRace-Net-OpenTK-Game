using System;
using System.Collections.Generic;
using OpenTK;
using RGEngine.Graphics;
using RGEngine.Support;

namespace Racing.Objects.UI
{
    /// <summary>
    /// Defines a simple UI text element.
    /// </summary>
    public class UIText : UIElement
    {
        private static Texture2D[] digitTextures;
        private static readonly string digits = "0123456789";
        /// <summary>
        /// Keeps the name of the Text element.
        /// </summary>
        public readonly string name;

        static UIText()
        {
            digitTextures = LoadSymbolsTexArray(@"Contents\UI\Digits\0.png",
                                                @"Contents\UI\Digits\1.png",
                                                @"Contents\UI\Digits\2.png",
                                                @"Contents\UI\Digits\3.png",
                                                @"Contents\UI\Digits\4.png",
                                                @"Contents\UI\Digits\5.png",
                                                @"Contents\UI\Digits\6.png",
                                                @"Contents\UI\Digits\7.png",
                                                @"Contents\UI\Digits\8.png",
                                                @"Contents\UI\Digits\9.png");
        }

        /// <summary>
        /// Ctor that defines basic text element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="scale"></param>
        public UIText(string name, Vector2 position, Vector2 scale)
            : base(position)
        {
            this.name = name;
            base.spriteRenderer.RenderQueue = ParseStringToSprites("0", position, scale);
        }

        private static Texture2D[] LoadSymbolsTexArray(params string[] texPath)
        {
            var textures = new Texture2D[texPath.Length];

            for (int i = 0; i < texPath.Length; i++)
            {
                textures[i] = ContentLoader.LoadTexture(texPath[i]);
            }

            return textures;
        }

        /// <summary>
        /// Parses string to a set of sprites from pre-defined textures.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="textPosition"></param>
        /// <param name="symbolScale"></param>
        /// <returns></returns>
        public static SpriteBatch ParseStringToSprites(string data, Vector2 textPosition, Vector2 symbolScale)
        {
            if (data.Equals(string.Empty))
                throw new ArgumentException("There is no parse data into the input string.");

            var sprites = new List<Sprite>();
            // Create an offset with the space size of 5.
            var spaceWidth = -7;
            var offset = digitTextures[0].Width / 2 + spaceWidth;

            foreach (var @char in data)
            {
                Sprite symbolSprite;
                for (int i = 0; i < digits.Length; i++)
                {
                    if (@char == digits[i])
                    {
                        symbolSprite = new Sprite(digitTextures[i], symbolScale,
                            textPosition, 10);
                        sprites.Add(symbolSprite);

                        // Make an offset by X-axis for the next symbol if there is one.
                        textPosition += new Vector2(offset, 0);
                    }
                }
            }
            var textOutputSprites = SpriteBatch.CreateSpriteBatch(sprites.ToArray());

            return textOutputSprites;
        }
    }
}
