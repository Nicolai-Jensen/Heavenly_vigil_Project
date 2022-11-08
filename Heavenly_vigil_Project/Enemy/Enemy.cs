using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX;
using System;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// loads and draws the enemy with all the Fields and Methods, the enemy should have.
    /// </summary>
    internal class Enemy : GameObject
    {

        //Fields
        protected SoundEffect defeatedSound;
        protected int health;
        protected static Random rnd = new Random();
        protected bool katanahit = false;
        protected bool hitCooldown = false;
        protected float hitCooldownTimer;
        protected bool hitFeedback = false;
        protected bool gotHit = false;
        protected float feedbackTimer;
        protected Color color;
 


        //Properties

        //Constructors
        public Enemy()
        {
            speed = 150;
            position = SpawnPosition();
            scale = 1;
            health = 15 + ExperiencePoints.PlayerLevel * 4;
            damage = 5;
            color = Color.White;
            
 
        }
        //Method
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[4];
            for (int i = 0; i < objectSprites.Length; i++)
            {
                objectSprites[i] = content.Load<Texture2D>($"skeleton_0{i + 1}");
            }
            defeatedSound = content.Load<SoundEffect>("enemy_defeat");
        }

        public override void Update(GameTime gameTime)
        {
            ChooseDirection();
            Animate(gameTime);
            Move(gameTime);
            Death();
            KatanaDamaged(gameTime);
            DamagedFeedBack(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            if(ReturnPlayerPosition().X < position.X)
            spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.FlipHorizontally, 1f);
            else
            spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.None, 1f);
        }
        public override void OnCollision(GameObject other)
        {
            if (Player.HitCooldown == false)
            {
                if (other is Player)
                {
                    Player.Health -= damage;
                    Player.HealthModified = true;
                }
            }

            if (other is Enemy)
            {
                Vector2 d = position - other.Position;
                position += 10 * d / (d.LengthSquared() + 1);
            }

            if (other is Magnum)
            {
                health -= Magnum.Damage;
                gotHit = true;

            }

            if (other is Katana && katanahit == false)
            {
                health -= Katana.Damage;
                gotHit = true;
                katanahit = true;
            }

        }

        /// <summary>
        /// Enemy Movement, Makes the enemy move towards the player.
        /// </summary>
        protected void ChooseDirection()
        {
            Vector2 playerPosition = ReturnPlayerPosition();

            velocity = playerPosition - position;
            velocity.Normalize();
        }

        protected Vector2 ReturnPlayerPosition()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go is Player)
                {
                    return go.Position;
                }
            }
            return new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2);
        }
        /// <summary>
        /// Uses a random number, to set the enemy´s position outside of the gameworld, and which side the enemy should spawn from. 
        /// </summary>
        /// <returns></returns>
        public Vector2 SpawnPosition()
        {
            Random rnd = new Random();
            int pos = rnd.Next(1, 5);
            if (pos == 1)
            {
                position.Y = -25;
                position.X = rnd.NextFloat(0, GameWorld.ScreenSize.X);
                return position;
            }
            else if (pos == 2)
            {
                position.Y = 1080;
                position.X = rnd.NextFloat(0, GameWorld.ScreenSize.X);
                return position;
            }
            else if (pos == 3)
            {
                position.Y = rnd.NextFloat(0, GameWorld.ScreenSize.Y);
                position.X = -25;
                return position;
            }
            else
            {
                position.Y = rnd.NextFloat(0, GameWorld.ScreenSize.Y);
                position.X = 1920;
                return position;

            }
        }

        public void KatanaDamaged(GameTime gameTime)
        {
            if (katanahit == true)
            {
                hitCooldown = true;
                if (hitCooldown == true)
                {
                }
                hitCooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (hitCooldownTimer >= 0.2f)
                {
                    hitCooldown = false;
                    hitCooldownTimer = 0;
                    katanahit = false;
                }

            }
        }

        public void DamagedFeedBack(GameTime gameTime)
        {
            if (gotHit == true)
            {
                hitFeedback = true;
                if (hitFeedback == true)
                {
                    color = Color.Red;
                }
                feedbackTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (feedbackTimer >= 0.1f)
                {
                    hitFeedback = false;
                    color = Color.White;
                    feedbackTimer = 0;
                    gotHit = false;
                }

            }

        }
        /// <summary>
        /// Removes the Enemy when killed and grant XP to the player.
        /// this is done by using a bool where it is added to the gameObjectsToRemove list.
        /// </summary>
        public void Death()
        {
            if (health <= 0)
            {
                toBeRemoved = true;
                ExperiencePoints.PlayerExp += 10;
                SoundEffectInstance newSoundIntance = defeatedSound.CreateInstance();
                newSoundIntance.Volume = 0.2f;
                newSoundIntance.Play();
            }
        }
    }
}
