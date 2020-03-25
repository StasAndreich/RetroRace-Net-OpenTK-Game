using System.Collections.Generic;


namespace RGEngine.Graphics
{
    public class SpriteBatch
    {
        public SpriteBatch()
        {

        }

        private List<Sprite> sprites = new List<Sprite>();

        public int Quantity => sprites.Count;

        public Sprite this[int index]
        {
            get => sprites[index];
            set => sprites[index] = value;
        }

        public void AddSprite()
        {

        }

        public void RemoveSprite()
        {

        }
    }
}
