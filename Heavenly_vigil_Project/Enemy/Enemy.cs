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

namespace Heavenly_vigil_Project
{
    internal class Enemy : GameObject
    {

        //Fields

        private int health;
        private int damage;

        //Properties
        //Constructors
        public Enemy()
        {
            velocity.Y = 0;
            speed = 100;
            position.X = 500;
            position.Y = 500;
            scale = 2;
        }
        //Method
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>($"tile_bat");
        }

        public override void Update(GameTime gameTime)
        {
            ChooseDirection();
            Move(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[0], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 1f);
        }
        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                Player.Health--;
            }
        }

        private void ChooseDirection()
        {
            Vector2 playerPosition = ReturnPlayerPosition();

            if (playerPosition.Y > position.Y)
            {
                velocity.Y = 1;
            }
            if (playerPosition.X > position.X)
            {
                velocity.X = 1;
            }
            if (playerPosition.Y < position.Y)
            {
                velocity.Y = -1;
            }
            if (playerPosition.X < position.X)
            {
                velocity.X = -1;
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
            return new Vector2(0, 0);
        }
    }
}
