using System;
using System.Collections.Generic;
using OpenTK;
using RGEngine.Graphics;
using RGEngine.Support;

namespace Racing.Objects.UserInterface
{
    /// <summary>
    /// Defines a simple UI text element.
    /// </summary>
    public class TextElement : UserInterfaceElement
    {
        private const int SpaceWidth = -7;
        private const string Digits = "0123456789";
        private static readonly Texture2D[] _digitTextures;

        static TextElement()
        {
            _digitTextures = LoadSymbolsTexArray(@"Contents\UI\Digits\0.png",
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
        public TextElement(string name, Vector2 position, Vector2 scale)
            : base(position)
        {
            Name = name;
            SpriteRenderer.RenderQueue = ParseStringToSprites("0", position, scale);
        }

        /// <summary>
        /// Keeps the name of the Text element.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Parses string to a set of sprites from pre-defined textures.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="textPosition"></param>
        /// <param name="symbolScale"></param>
        /// <returns></returns>
        public static SpriteBatch ParseStringToSprites(string data, Vector2 textPosition, Vector2 symbolScale)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException("There is no any data to parse in this string");
            }

            var sprites = new List<Sprite>();
            var startPos = textPosition;
            var offset = _digitTextures[0].Width / 2 + SpaceWidth;

            foreach (var symbol in data)
            {
                Sprite symbolSprite;
                for (int i = 0; i < Digits.Length; i++)
                {
                    if (symbol == Digits[i])
                    {
                        symbolSprite = new Sprite(
                            _digitTextures[i],
                            startPos,
                            symbolScale,
                            textPosition,
                            10);
                        sprites.Add(symbolSprite);

                        // Make an offset by X-axis for the next symbol if there is one.
                        textPosition += new Vector2(offset, 0);
                    }
                }
            }

            var textOutputSprites = SpriteBatch.CreateSpriteBatch(sprites.ToArray());

            return textOutputSprites;
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
    }
}
