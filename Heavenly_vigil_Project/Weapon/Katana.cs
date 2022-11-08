using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    internal class Katana : Weapon
    {

        // -----FIELDS-----
        private float rotation;
        private static int damage;
        private bool attacked = false;
        private float attackedTimer;
        private static bool attackAnimation = true;
        private static float scaleValue = 1f;
        private static float speedValue = 120f;
        private static float travelDistance = 0.3f;

        // -----PROPERTIES-----
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public static float SpeedValue
        {
            get { return speedValue; }
            set { speedValue = value;}
        }

        public static float TravelDistance
        {
            get { return travelDistance; }
            set { travelDistance = value; }
        }

        public static float ScaleValue
        {
            get { return scaleValue; }
            set { scaleValue = value; }
        }
        public static int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public static bool AttackAnimation
        {
            get { return attackAnimation; }
            set { attackAnimation = value; }
        }
        // -----CONSTRUCTORS-----
        public Katana(Texture2D sprite, Vector2 position, GameTime gameTime)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = sprite;
            this.position = position;
            rotation = 0f;
            scale = scaleValue;
            speed = 0f;
            velocity = DirectionClosestEnemy(ReturnPlayerPosition());
            damage = 2 + DamageMultiplyer;

        }

        // -----METHODS-----
        public override void Draw(SpriteBatch spriteBatch)
        {

            if (attackAnimation == true)
            {
                spriteBatch.Draw(objectSprites[0], position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(objectSprites[0], position, null, Color.White, rotation, origin, scale, SpriteEffects.FlipHorizontally, 0);
            }
            
        }

        public override void LoadContent(ContentManager content)
        {
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
        }

        public override void OnCollision(GameObject other)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            Attack(gameTime);
            Attacking(gameTime);
        }

        public override void Attack(GameTime gameTime)
        {

        }


        private void Attacking(GameTime gameTime)
        {
            if (attacked == false)
            {
                speed = speedValue;
                attacked = true;
            }
            attackedTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (attackedTimer >= travelDistance)
            {
                position.Y = 10000000f;
                attacked = false;
                attackedTimer = 0;
            }
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

            return new Vector2(position.X, -100);
        }
        public Vector2 DirectionClosestEnemy(Vector2 playerPosition)
        {
            Vector2 direction;
            Vector2 enemyPosition = new Vector2(0, 0);
            float distance = 1000f;
            float shortestDistance = 2000f;

            foreach (GameObject enemy in GameWorld.GameObjects)
            {
                if (enemy is Enemy)
                {
                    distance = Vector2.Distance(playerPosition, enemy.Position);
                }

                if (distance < shortestDistance && enemy is Enemy)
                {
                    shortestDistance = distance;
                    enemyPosition = enemy.Position;
                }
            }

            direction = enemyPosition - playerPosition;
            direction.Normalize();
            rotation = (float)Math.Atan2(enemyPosition.Y - position.Y, enemyPosition.X - position.X) + 1.4f;

            return direction;
        }
    }
}
