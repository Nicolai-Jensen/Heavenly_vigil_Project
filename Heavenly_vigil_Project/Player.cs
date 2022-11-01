using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    public class Player : GameObject
    {
        // -----FIELDS-----
        private Texture2D[] laserSprite;
        private bool cooldown = true;
        private float cooldownTimer;
        private SoundEffect shootingSound;
        private int health;

        // -----PROPERTIES-----

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        // -----CONSTRUCTORS-----
        public Player(Vector2 vector2)
        {
            position = vector2;
            scale = 1f;
            health = 100;
            speed = 400f;
        }
        // -----METHODS-----
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[3];
            for (int i = 0; i < objectSprites.Length; i++)
            {
                objectSprites[i] = content.Load<Texture2D>($"tile_arara_azul");
            }
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);

            position.X = GameWorld.ScreenSize.X / 2;
            position.Y = GameWorld.ScreenSize.Y / 2;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Move(gameTime);
            ScreenWrap();
            Animate(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[(int)animationTime], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0);
        }


        private void HandleInput(GameTime gameTime)
        {
            velocity = Vector2.Zero;

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }

            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }

            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(+1, 0);
            }

            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, +1);
            }

            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

           
        }

        public void ScreenWrap()
        {
            if (position.Y + objectSprites[0].Height / 2 * scale >= GameWorld.ScreenSize.Y)
            {
                position.Y = GameWorld.ScreenSize.Y - objectSprites[0].Height / 2 * scale;
            }

            if (position.Y - objectSprites[0].Height / 2 * scale < 0)
            {
                position.Y = 0 + objectSprites[0].Height / 2 * scale;
            }

            if (position.X + objectSprites[0].Width / 2 * scale >= GameWorld.ScreenSize.X)
            {
                position.X = GameWorld.ScreenSize.X - objectSprites[0].Width / 2 * scale;
            }

            if (position.X - objectSprites[0].Width / 2 * scale < 0)
            {
                position.X = 0 + objectSprites[0].Width / 2 * scale;
            }
        }

    }
}
