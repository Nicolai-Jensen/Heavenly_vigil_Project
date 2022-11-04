using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using SharpDX.Direct3D9;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Microsoft.Xna.Framework.Input;

namespace Heavenly_vigil_Project
{
    internal class Enemy : GameObject
    {

        //Fields

        private int health;
        private int damage = 10;
        protected static Random rnd = new Random();

        //Properties

        //Constructors
        public Enemy()
        {
            speed = 150;
            position = SpawnPosition();
            scale = 2;
            health = 20;
        }
        //Method
        public override void LoadContent(ContentManager content)
        {
            int i = rnd.Next(1, 4);
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>($"enemy_{i}");
        }

        public override void Update(GameTime gameTime)
        {  
            ChooseDirection();
            Move(gameTime);
            Death();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            spriteBatch.Draw(objectSprites[0], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 1f);
        }
        public override void OnCollision(GameObject other)
        {
            if (Player.HitCooldown == false)
            {
                if (other is Player)
                {
                    Player.Health--;
                    Player.HealthModified = true;
                }
            }

            if (other is Magnum)
            {
                health -= Magnum.Damage;
            }

            if (other is Katana)
            {
                health -= Katana.Damage;
            }

        }

        private void ChooseDirection()
        {
            Vector2 playerPosition = ReturnPlayerPosition();

            velocity = playerPosition - position;
            velocity.Normalize();
        }

        private Vector2 ReturnPlayerPosition()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go is Player)
                {
                    return go.Position;
                }
            }
            return new Vector2(0, 0);
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

        public void Death()
        {
            if (health <= 0)
            {
                position.Y = 1000000;
                ExperiencePoints.PlayerExp += 10;
            }
        }
    }
}
