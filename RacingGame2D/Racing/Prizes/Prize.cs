using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using OpenTK;
using RGEngine;
using Racing.Objects;
using System.Linq;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines an abstract class for a prize.
    /// </summary>
    public abstract class Prize : GameObject, ICollidable, INonResolveable
    {
        private SpriteRenderer _spriteRenderer;
        private RigidBody2D _rigidBody;
        private Animator _animator;

        /// <summary>
        /// Default ctor with setting collider and animation.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texturesPath"></param>
        public Prize(Vector2 position, params string[] texturesPath)
        {
            _spriteRenderer = (SpriteRenderer)AddComponent("SpriteRenderer");
            _animator = (Animator)AddComponent("Animator");

            // Set default position.
            Position = position;

            for (int i = 0; i < texturesPath.Length; i++)
            {
                var tex = ContentLoader.LoadTexture(texturesPath[i]);
                var sprite = new Sprite(tex, position, new Vector2(0.55f, 0.55f), new Vector2(0f, 0f), 3);
                _animator.AnimationSprites.AddSprite(sprite);
            }
            _animator.FPS = 7;

            collider = new PolyCollider(this, new Vector2(40f, 40f));
        }

        internal void PickUp(Car car)
        {
            ApplyPrize(car);
            RemovePrize();
        }

        /// <summary>
        /// Removes a prize from the scene.
        /// </summary>
        protected void RemovePrize()
        {
            PolyCollider.allCollidersAttached.Remove(this);

            var gameObjects = EngineCore.gameObjects.ToList();
            foreach (var gameObject in gameObjects)
            {
                if (ReferenceEquals(gameObject, this))
                {
                    EngineCore.RemoveGameObject(this);
                }
            }
        }

        /// <summary>
        /// Applies a prize to a Car.
        /// </summary>
        /// <param name="car"></param>
        protected abstract void ApplyPrize(Car car);
    }
}