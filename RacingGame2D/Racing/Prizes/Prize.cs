using RGEngine.Graphics;
using RGEngine.Physics;
using RGEngine.BaseClasses;
using RGEngine.Support;
using OpenTK;
using RGEngine;
using System.Linq;
using Racing.Objects;

namespace Racing.Prizes
{
    /// <summary>
    /// Defines an abstract class for a prize.
    /// </summary>
    public abstract class Prize : GameObject, ICollidable, INonResolveable
    {
        /// <summary>
        /// Default ctor with setting collider and animation.
        /// </summary>
        /// <param name="texturesPath"></param>
        public Prize(params string[] texturesPath)
        {
            spriteRenderer = AddComponent<SpriteRenderer>();
            animator = AddComponent<Animator>();

            // Set default position.
            Position = new Vector2(0f, 0f);

            for (int i = 0; i < texturesPath.Length; i++)
            {
                var tex = ContentLoader.LoadTexture(texturesPath[i]);
                var sprite = new Sprite(tex, new Vector2(0.55f, 0.55f), new Vector2(0f, 0f), 3);
                animator.AnimationSprites.AddSprite(sprite);
            }
            animator.FPS = 7;

            base.collider = new PolyCollider(this, new Vector2(40f, 40f));
        }

        /// <summary>
        /// SpriteRenderer component.
        /// </summary>
        protected SpriteRenderer spriteRenderer;
        /// <summary>
        /// RigidBody2D component.
        /// </summary>
        protected RigidBody2D rigidBody;
        /// <summary>
        /// Animator component.
        /// </summary>
        protected Animator animator;


        internal void PickUp(Car car)
        {
            ApplyDecorator(car);
            RemovePrize();
        }

        /// <summary>
        /// Removes a prize from the scene.
        /// </summary>
        protected void RemovePrize()
        {
            PolyCollider.allCollidersAttached.Remove(this);

            var list = EngineCore.gameObjects.ToList<GameObject>();
            foreach (var @object in list)
            {
                if (ReferenceEquals(@object, this))
                    EngineCore.RemoveGameObject(this);
            }
        }

        /// <summary>
        /// Applies a decorator to a CarProps.
        /// </summary>
        /// <param name="car"></param>
        protected abstract void ApplyDecorator(Car car);
    }

    /// <summary>
    /// Defines all possible in-game prizes.
    /// </summary>
    public enum Prizes
    {
        /// <summary>
        /// Adds fuel.
        /// </summary>
        Fuel = 1,
        /// <summary>
        /// Speeds up a car.
        /// </summary>
        Boost = 2,
        /// <summary>
        /// Slows a car.
        /// </summary>
        Slowdown = 3
    }
}