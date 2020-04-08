using System.Collections;
using System.Collections.Generic;


namespace RGEngine.Graphics
{
    public class SpriteBatch : IEnumerable<Sprite>
    {
        /// <summary>
        /// Stores all sprites for some gameObject sorted in Z-index.
        /// </summary>
        private List<Sprite> sprites = new List<Sprite>();

        public int Quantity => sprites.Count;

        public Sprite this[int index]
        {
            get => sprites[index];
            set => sprites[index] = value;
        }

        public void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
            sprites.Sort();
        }

        public void RemoveSprite()
        {
            // ???
        }

        public static SpriteBatch CreateSpriteBatch(params Texture2D[] textures)
        {
            if (textures.Length == 0)
                return null;

            var batch = new SpriteBatch();

            // после реализации Enumrator в Spritebatch
            // попробовать заменить Фор на Форич
            for (int i = 0; i < textures.Length; i++)
            {
                batch.AddSprite(new Sprite(textures[i]));
            }

            return batch;
        }

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
