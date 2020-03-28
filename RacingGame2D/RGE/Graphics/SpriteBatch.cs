using System.Collections.Generic;


namespace RGEngine.Graphics
{
    public class SpriteBatch //: IEnumerable<Sprite>
    {
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
        }

        public void RemoveSprite()
        {
            
        }


        internal static SpriteBatch CreateSpriteBatch(params Texture2D[] textures)
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
    }
}
