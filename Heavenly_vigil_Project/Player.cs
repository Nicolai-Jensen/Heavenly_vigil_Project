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
 /// <summary>
 /// loads and draws the player with all the functionality, the player should have.
 /// </summary>
    public class Player : GameObject
    {
        // -----FIELDS-----
        private Texture2D[] magnumShot;
        private Texture2D[] katanaSlash;
        private SoundEffect shootingSound;
        private SoundEffect hurtSound;
        private Color color;
        private static int health;
        private static int mana;
        private int Levelup;
        private bool cooldown = true;
        private bool cooldown2 = true;
        private bool manaCooldown = false;
        private bool manaRegen = false;
        private bool manadecrease = false;
        private static bool hitCooldown = false;
        private static bool healthModified = false;
        private float cooldownTimer;
        private float cooldownTimer2;
        private float cooldownTimerNumber;
        private float cooldownTimerNumber2;
        private float hitCooldownTimer;
        private float manaRegenerating = 0;
        private float manadecreasing = 0;

        // -----PROPERTIES-----

        public static int Health
        {
            get { return health; }
            set { health = value; }
        }
        public static int Mana
        {
            get { return mana; }
            set { mana = value; }
        }

        public float CooldownTimerNumber
        {
            get { return cooldownTimerNumber; }
            set { cooldownTimerNumber = value; }
        }
        public float CooldownTimerNumber2
        {
            get { return cooldownTimerNumber2; }
            set { cooldownTimerNumber2 = value; }
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


        // -----CONSTRUCTORS-----
        public Player(Vector2 vector2)
        {
            position = vector2;
            Levelup = ExperiencePoints.PlayerLevel;
            scale = 2f;
            health = 100;
            mana = 100;
            speed = 300f;
            color = Color.White;
            cooldownTimerNumber = 0.6f;
            cooldownTimerNumber2 = 0.4f;
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

            hurtSound = content.Load<SoundEffect>("hurt");

            katanaSlash = new Texture2D[1];
            katanaSlash[0] = content.Load<Texture2D>("HeavenlyVigilSwordSlash");


            magnumShot = new Texture2D[1];
            magnumShot[0] = content.Load<Texture2D>("BulletSprite");
            shootingSound = content.Load<SoundEffect>("energy_gun2");
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
            PowerState(gameTime);
            Restorehealth();
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





            //Code needed so that the objects speed isn't increased when moving diagonally
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }

           
        }

        public void Restorehealth()
        {
            if (ExperiencePoints.PlayerLevel > Levelup)
            {
                health += 3;
                if (health > 100)
                {
                    health = 100;
                }
                Levelup = ExperiencePoints.PlayerLevel;
            }
        }


        public void PowerState(GameTime gameTime)
        {
            //Keystate reads which key is being used
            KeyboardState keyState2 = Keyboard.GetState();

            if (keyState2.IsKeyDown(Keys.Space) && manaCooldown == false)
            {
                color = Color.Blue;
                scale *= 2;
                speed *= 2;
                cooldownTimerNumber /= 2f;
                cooldownTimerNumber2 /= 2f;
                Katana.ScaleValue *= 1.5f;
                Katana.SpeedValue *= 3f;
                Katana.TravelDistance = 0.8f;
                manadecrease = true;
                manaCooldown = true;
                UpgradeInterface.IsInteractive = false;
            }
           
            if (mana > 0 && manadecrease == true)
            {
                manadecreasing += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (manadecreasing >= 0.05f)
                {
                    mana--;
                    manadecreasing = 0;
                }

            }

            if (mana == 0 && manadecrease == true)
            {
                manaRegen = true;
                manadecrease = false;

                color = Color.White;
                scale /= 2;
                speed /= 2;
                cooldownTimerNumber *= 2f;
                cooldownTimerNumber2 *= 2f;
                Katana.ScaleValue /= 1.5f;
                Katana.SpeedValue /= 3f;
                Katana.TravelDistance = 0.3f;
                UpgradeInterface.IsInteractive = true;
            }
            
            if (manaRegen == true && mana < 100)
            {
                manaRegenerating += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (manaRegenerating >= 0.25f)
                {
                    mana++;
                    manaRegenerating = 0;
                }
            }

            if (mana == 100 && manaRegen == true)
            {
                manaRegen = false;
                manaCooldown = false;
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
                if (hitCooldownTimer <= 0.05f)
                {
                    SoundEffectInstance hurtSoundIntance = hurtSound.CreateInstance();
                    hurtSoundIntance.Volume = 0.5f;
                    hurtSoundIntance.Play();
                }

                if (hitCooldownTimer >= 0.4f)
                {
                    hitCooldown = false;
                    color = Color.White;
                    hitCooldownTimer = 0;
                    healthModified = false;
                }                
            }
        }

        /// <summary>
        /// This Method is used to kill off the player entity if it reaches 0 health value.
        /// this is done by using a bool where it is added to the gameObjectsToRemove list.
        /// </summary>
        public void Death()
        {
            if (health <= 0)
            {
                ToBeRemoved = true;
                GameOverScreen gameover = new GameOverScreen();
                GameWorld.InstantiateGameObject(gameover);
            }
        }

        public void Attack(GameTime gameTime)
        {
            bool hasKatana = true;
            bool hasMagnum = true;

            if (cooldown == true)
            {
                if (hasMagnum == true)
                {
                    Magnum shot = new Magnum(magnumShot[0], new Vector2(position.X, position.Y));
                    GameWorld.InstantiateGameObject(shot);
                    SoundEffectInstance shootingSoundIntance = shootingSound.CreateInstance();
                    shootingSoundIntance.Volume = 0.1f;
                    shootingSoundIntance.Play();
                }
                cooldown = false;
            }
            cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownTimer >= CooldownTimerNumber)
            {
                cooldown = true;
                cooldownTimer = 0;
            }

            if (cooldown2 == true)
            {
                if (hasKatana == true)
                {
                    Katana slash = new Katana(katanaSlash[0], new Vector2(position.X, position.Y), gameTime);
                    GameWorld.InstantiateGameObject(slash);
                    if (Katana.AttackAnimation == true)
                    {
                        Katana.AttackAnimation = false;
                    }
                    else { Katana.AttackAnimation = true; }
                }
                cooldown2 = false;
            }
            cooldownTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownTimer2 >= cooldownTimerNumber2)
            {
                cooldown2 = true;
                cooldownTimer2 = 0;
            }

        }
    }
}
