using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RGEngine.Graphics
{
    /// <summary>
    /// Defines a class that stores a batch of sprites.
    /// </summary>
    public class SpriteBatch : IEnumerable<Sprite>
    {
        /// <summary>
        /// Stores all sprites for some gameObject sorted in Z-index.
        /// </summary>
        private List<Sprite> sprites = new List<Sprite>();

        /// <summary>
        /// Indicates amount of sprites stored in sprite batch.
        /// </summary>
        public int Quantity => sprites.Count;

        /// <summary>
        /// Basic indexer.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Sprite this[int index]
        {
            get => sprites[index];
            set => sprites[index] = value;
        }

        /// <summary>
        /// Event that raised when current batch was updated.
        /// </summary>
        public event EventHandler BatchUpdated;

        /// <summary>
        /// Adds a sprite to a sprite batch.
        /// </summary>
        /// <param name="sprite"></param>
        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
            sprites.Sort();
            BatchUpdated?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Removes a sprite from a sprite batch.
        /// </summary>
        /// <param name="name"></param>
        public void RemoveSprite(string name)
        {
            BatchUpdated?.Invoke(this, new EventArgs());
            foreach (var sprite in sprites.ToList<Sprite>())
            {
                if (sprite.Texture.Path == name)
                {
                    sprites.Remove(sprite);
                }
            }
        }

        /// <summary>
        /// Returns created from textures SpriteBatch object.
        /// </summary>
        /// <param name="textures"></param>
        /// <returns></returns>
        public static SpriteBatch CreateSpriteBatch(params Texture2D[] textures)
        {
            if (textures.Length == 0)
                return null;

            var batch = new SpriteBatch();

            for (int i = 0; i < textures.Length; i++)
            {
                batch.AddSprite(new Sprite(textures[i]));
            }

            return batch;
        }

        /// <summary>
        /// Returns created from sprites SpriteBatch object.
        /// </summary>
        /// <param name="sprites"></param>
        /// <returns></returns>
        public static SpriteBatch CreateSpriteBatch(params Sprite[] sprites)
        {
            if (sprites.Length == 0)
                return null;

            var batch = new SpriteBatch();
            for (int i = 0; i < sprites.Length; i++)
            {
                batch.AddSprite(sprites[i]);
            }

            return batch;
        }

        /// <summary>
        /// IEnumerator implementation.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Sprite> GetEnumerator()
        {
            return ((IEnumerable<Sprite>)sprites).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Sprite>)sprites).GetEnumerator();
        }
    }
}
