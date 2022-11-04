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
        private Texture2D[] magnumShot;
        private static int health;
        private bool cooldown = true;
        private float cooldownTimer;
        private static bool hitCooldown = false;
        private float hitCooldownTimer;
        private bool dashCooldown = true;
        private float dashCooldownTimer;
        private bool dashed = false;
        private float dashedTimer;
        private Color color;
        private static bool healthModified = false;
        private static bool hasKatana = true;
        private static bool hasMagnum = false;

        // -----PROPERTIES-----

        public static int Health
        {
            get { return health; }
            set { health = value; }
        }

        public static bool HasMagnum
        {
            get { return hasMagnum; }
            set { hasMagnum = value; }
        }
        public static bool HealthModified
        {
            get { return healthModified; }
            set { healthModified = value; }
        }
        public static bool HitCooldown
        {
            get { return hitCooldown; }
        }

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        // -----CONSTRUCTORS-----
        public Player(Vector2 vector2)
        {
            position = vector2;
            scale = 2f;
            health = 100;
            speed = 400f;
            color = Color.White;
        }
        // -----METHODS-----

        /// <summary>
        /// This Method loads in information and code needed for the game
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            //instantiates a new Texture2D in an Array
            objectSprites = new Texture2D[3];
            //The Array is then looped with this for loop where it cycles through a list of sprites with the array numbers
            for (int i = 0; i < objectSprites.Length; i++)
            {
                objectSprites[i] = content.Load<Texture2D>($"tile_arara_azul");
            }
            //This line of code places the objects origin within the middle of the sprite assuming all sprites in the array share the same size
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            //Places the Object in the middle of the game screen upon startup
            position.X = GameWorld.ScreenSize.X / 2;
            position.Y = GameWorld.ScreenSize.Y / 2;




            magnumShot = new Texture2D[1];
            magnumShot[0] = content.Load<Texture2D>("laser");
        }

        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            Move(gameTime);
            ScreenWrap();
            Animate(gameTime);
            Death();
            Damaged(gameTime);
            Attack(gameTime);
        }

        /// <summary>
        /// The Method for Drawing out a sprite to the screen, this method is an override for the vitual one in GameObject and is called in GameWorld
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //A Draw Method with different overloads, this particular one has 10 variables which can be defined
            spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// The Player Input is controlled in this Method
        /// </summary>
        /// <param name="gameTime"></param>
        private void HandleInput(GameTime gameTime)
        {
            //velocity determines the direction the object is moving, this code sets the vector values to 0
            velocity = Vector2.Zero;

            //Keystate reads which key is being used
            KeyboardState keyState = Keyboard.GetState();

            //Moves the player up when pressing W by removing Y position value 
            if (keyState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0, -1);
            }
            
            //Moves the player left when pressing A by removing X position value 
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
            //Moves the player right when pressing D by adding X position value 
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(+1, 0);
            }
            //Moves the player down when pressing S by adding Y position value 
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, +1);
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (dashCooldown == true)
                {
                    if (dashed == false)
                    {
                        speed = 1200f;
                        dashed = true;
                        dashCooldown = false;
                    }              
                }
                dashedTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dashedTimer >= 0.1f)
                {
                    speed = 400f;
                    dashedTimer = 0;
                }
                dashCooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (dashCooldownTimer >= 1.3f)
                {
                    dashed = false;
                    dashCooldown = true;
                    dashCooldownTimer = 0;
                }
            }

            //Code needed so that the objects speed isn't increased when moving diagonally
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

           
        }

        /// <summary>
        /// This Method is used to make sure the object is not moving outside the screenbounds
        /// It does this with if statements checking if the objects position values are outside the bounders of the screen
        /// </summary>
        public void ScreenWrap()
        {
            //Checks if the sprite is moving outside the bottom of the screen and blocks it
            if (position.Y + objectSprites[0].Height / 2 * scale >= GameWorld.ScreenSize.Y)
            {
                position.Y = GameWorld.ScreenSize.Y - objectSprites[0].Height / 2 * scale;
            }
            //Checks if the sprite is moving outside the top of the screen and blocks it
            if (position.Y - objectSprites[0].Height / 2 * scale < 0)
            {
                position.Y = 0 + objectSprites[0].Height / 2 * scale;
            }
            //Checks if the sprite is moving outside the right of the screen and blocks it
            if (position.X + objectSprites[0].Width / 2 * scale >= GameWorld.ScreenSize.X)
            {
                position.X = GameWorld.ScreenSize.X - objectSprites[0].Width / 2 * scale;
            }
            //Checks if the sprite is moving outside the left of the screen and blocks it
            if (position.X - objectSprites[0].Width / 2 * scale < 0)
            {
                position.X = 0 + objectSprites[0].Width / 2 * scale;
            }
        }

        public override void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// A Method that waits for an enemy to damage the player
        /// when the player is damaged it makes the enemy unable to hit the player and marks the player as red
        /// a timer starts and when it is over the player is tangible again for enemies to hit reseting the timer and color.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Damaged(GameTime gameTime)
        {
            if (healthModified == true)
            {
                hitCooldown = true;
                if (hitCooldown == true)
                {
                    color = Color.Red;
                }
                hitCooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (hitCooldownTimer >= 0.3f)
                {
                    hitCooldown = false;
                    color = Color.White;
                    hitCooldownTimer = 0;
                    healthModified = false;
                }
                
            }
        }

        /// <summary>
        /// This Method is used to kill off the player entity if it reaches 0 health value
        /// ths is done by putting its position to an outofbounds area where it is automatically deleted from the gameObjects list
        /// </summary>
        public void Death()
        {
            if (health <= 0)
            {
                position.Y = 1000000;
            }
        }

        public void Attack(GameTime gameTime)
        {
            hasMagnum = true;
            if (hasMagnum == true)
            {
                if (cooldown == true)
                {
                    Magnum shot = new Magnum(magnumShot[0], new Vector2(position.X, position.Y - 60));
                    GameWorld.InstantiateGameObject(shot);
                    cooldown = false;
                }
                cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (cooldownTimer >= 0.3f)
                {
                    cooldown = true;
                    cooldownTimer = 0;
                }
            }
        }

    }
}
