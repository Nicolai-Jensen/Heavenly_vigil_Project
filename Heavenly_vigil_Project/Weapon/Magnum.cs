using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    internal class Magnum : Weapon
    {

        // -----FIELDS-----
        private float rotation;
        private Texture2D[] magnumShot;
        private Vector2 playerPosition;

        // -----PROPERTIES-----
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }


        // -----CONSTRUCTORS-----
        public Magnum(Texture2D sprite, Vector2 position)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = sprite;
            this.position = position;
            scale = 1f;
            rotation = 0f;
            speed = 800;
        }
        
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
            ChooseDirection();
        }

        public override void Attack(GameTime gameTime)
        {

        }

        private void ChooseDirection()
        {
            Vector2 playerPosition = ReturnEnemyPosition();

            velocity = playerPosition - position;
            velocity.Normalize();
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
            return new Vector2(0, 0);
        }
    }
}
