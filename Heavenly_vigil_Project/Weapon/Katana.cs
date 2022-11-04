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
        private Vector2 enemyPosition;
        private bool attacked = false;
        private float attackedTimer;

        // -----PROPERTIES-----
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public static int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        // -----CONSTRUCTORS-----
        public Katana(Texture2D sprite, Vector2 position, GameTime gameTime)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = sprite;
            this.position = position;
            scale = 0.7f;
            rotation = 0f;
            speed = 0f;
            ChooseDirection();
            damage = 10;
        }

        // -----METHODS-----
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[0], position, null, Color.Blue, rotation, origin, scale, SpriteEffects.None, 0);
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

        private void ChooseDirection()
        {
            Vector2 enemyPosition = ReturnEnemyPosition();

            velocity += enemyPosition - position;
            velocity.Normalize();
            rotation = (float)Math.Atan2(enemyPosition.Y - position.Y, enemyPosition.X - position.X);

        }

        private void Attacking(GameTime gameTime)
        {
            if (attacked == false)
            {
                speed = 100f;
                attacked = true;
            }
            attackedTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (attackedTimer >= 0.15f)
            {
                position.Y = 10000000f;
                attacked = false;
                attackedTimer = 0;
            }
        }

        private Vector2 ReturnEnemyPosition()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {

                if (go is Enemy)
                {

                    return go.Position;
                }
            }

            return new Vector2(position.X, -100);
        }
    }
}
