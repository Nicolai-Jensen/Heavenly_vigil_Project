using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    public abstract class GameObject
    {

        // -----FIELDS-----
        protected Vector2 position;
        protected float scale;
        protected float animationTime;
        protected float animationSpeed;
        protected Vector2 origin;
        protected Texture2D[] objectSprites;
        protected Vector2 velocity;
        protected float speed;
        protected bool toBeRemoved;
        protected int layerDepth;
        protected int damage;
        protected float attackSpeed;
        protected static int damageMultiplyer = 0;

        // -----PROPERTIES-----
        private Texture2D CurrentSprite
        {
            get { return objectSprites[(int)animationTime]; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        protected Vector2 SpriteSize
        {
            get { return new Vector2(CurrentSprite.Width * scale, CurrentSprite.Height * scale); }
        }

        public static int DamageMultiplyer
        {
            get { return damageMultiplyer; }
            set { damageMultiplyer = value; }

        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(

                    (int)(position.X - SpriteSize.X / 2),
                    (int)(position.Y - SpriteSize.Y / 2),
                    (int)SpriteSize.X, (int)SpriteSize.Y);
            }
        }

        public bool ToBeRemoved
        {
            get
            {
                return toBeRemoved;
            }
            set { toBeRemoved = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        // -----CONSTRUCTORS-----



        // -----METHODS-----
        /// <summary>
        /// This Method loads in information and code needed for the game
        /// </summary>
        /// <param name="content"></param>
        public abstract void LoadContent(ContentManager content);
        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// The Method for drawing out a sprite to the screen, this method is a virtual so we can override it, and is called in GameWorld
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(CurrentSprite.Width / 2, CurrentSprite.Height / 2);
            spriteBatch.Draw(objectSprites[(int)animationTime], position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }


        public bool IsColliding(GameObject other)
        {
            if (this == other)
            {
                return false;
            }
            return CollisionBox.Intersects(other.CollisionBox);
        }

        public virtual void OnCollision(GameObject other)
        {

        }

        protected void Move(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            position += ((velocity * speed) * deltaTime);
        }

        protected void Animate(GameTime gameTime)
        {
            animationTime += (float)gameTime.ElapsedGameTime.TotalSeconds * animationSpeed;

            if (animationSpeed > objectSprites.Length)
            {
                animationTime = 0;
            }
        }

        public bool IsOutOfBounds()
        {
            return (position.Y > GameWorld.ScreenSize.Y || position.X > GameWorld.ScreenSize.X || position.Y < -50 || position.X < -50);
        }


    }
}
