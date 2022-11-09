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

        //The extra Texture2D[] are here so that the weappons can be loaded and instantiated from the player itself
        private Texture2D[] magnumShot;
        private Texture2D[] katanaSlash;
        private Texture2D[] idleAnimation;
        private Texture2D[] runAnimation;

        //SoundEffect variables for the sounds loaded by the player object
        private SoundEffect shootingSound;
        private SoundEffect hurtSound;
        private SoundEffect swordSound;
        private SoundEffect gameOverSound;
        private SoundEffect levelupSound;

        //Fields that contain player stat data
        private Color color;
        private static int health;
        private static int mana;
        private int Levelup;

        //all of these bools are used to start and stop the various timers attached to the player object
        private bool cooldown = true;
        private bool cooldown2 = true;
        private bool manaCooldown = false;
        private bool manaRegen = false;
        private bool manadecrease = false;
        private static bool hitCooldown = false;
        private static bool healthModified = false;

        //Bool to decide which draw method to use depending on which way the player moves
        private bool isFacingRight = true;

        //These floats contain the value of the timer that is counting up, they are used as a stopwatch.
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
        /// <summary>
        /// The only constructor under player class, it is used when instantiating the player character
        /// </summary>
        /// <param name="vector2">This parameter is used to place the player in the middle of the screen at startup</param>
        public Player(Vector2 vector2)
        {
            //base stats and the sprites state
            position = vector2;
            Levelup = ExperiencePoints.PlayerLevel;
            scale = 1f;
            health = 100;
            mana = 100;
            speed = 300f;
            color = Color.White;

            //Base attackspeed variable used in the Attack method, no number is the magnum and 2 is the sword
            cooldownTimerNumber = 0.6f;
            cooldownTimerNumber2 = 0.4f;
        }

        // -----METHODS-----

        /// <summary>
        /// This Method loads in information and code needed for the game
        /// </summary>
        /// <param name="content">Parameter given by the monogame framework for our use</param>
        public override void LoadContent(ContentManager content)
        {
            //instantiates a new Texture2D in an Array
            runAnimation = new Texture2D[8];


            //The Array is then looped with this for loop where it cycles through a list of sprites with the array numbers
            for (int i = 0; i < runAnimation.Length; i++)
            {
                runAnimation[i] = content.Load<Texture2D>($"hero_{i}");
            }

            //The Array is then looped with this for loop where it cycles through a list of sprites with the array numbers
            for (int i = 0; i < idleAnimation.Length; i++)
            {
                idleAnimation[i] = content.Load<Texture2D>($"hero_{i}");
            }

            //This line of code places the objects origin within the middle of the sprite assuming all sprites in the array share the same size
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);

            //Places the Object in the middle of the game screen upon startup
            position.X = GameWorld.ScreenSize.X / 2;
            position.Y = GameWorld.ScreenSize.Y / 2;

            //Loads all of the soundeffects for use in other methods
            hurtSound = content.Load<SoundEffect>("hurt");
            swordSound = content.Load<SoundEffect>("Sword_swoosh");
            gameOverSound = content.Load<SoundEffect>("game_over_sound");
            levelupSound = content.Load<SoundEffect>("levelup_sound");
            shootingSound = content.Load<SoundEffect>("plst00");

            //Loads the textures for the weapons
            katanaSlash = new Texture2D[1];
            katanaSlash[0] = content.Load<Texture2D>("HeavenlyVigilSwordSlash");
            magnumShot = new Texture2D[1];
            magnumShot[0] = content.Load<Texture2D>("BulletSprite");
        }

        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime">Parameter given by the Monogame framework to simulate time</param>
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
        /// <param name="spriteBatch">Parameter given by the Monogame framework for the use of sprites</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //A Draw Method with different overloads, this particular one has 10 variables which can be defined
            if (velocity.X == 0 && velocity.Y == 0)
            {
                spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.None, 0);
            }
            //If the player has last pressed "D" to move right, it calls the first draw method, which doesn't flip the sprites
            if (isFacingRight)
            {
                spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.None, 0);
            }
            //If the player has last pressed "A" to move left the draw method with the sprites flipped horizontally will be called
            else if (!isFacingRight)
            {
                spriteBatch.Draw(objectSprites[(int)animationTime], position, null, color, 0, origin, scale, SpriteEffects.FlipHorizontally, 0);
            }
        }

        /// <summary>
        /// The Player movement Input is controlled in this Method
        /// </summary>
        /// <param name="gameTime">Parameter given by the Monogame framework to simulate time</param>
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
                objectSprites = runAnimation;
            }

            //Moves the player left when pressing A by removing X position value, and sets the the bool to false to determine which draw method to use
            if (keyState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
                objectSprites = runAnimation;
                isFacingRight = false;
            }
            //Moves the player right when pressing D by adding X position value, and sets the bool to true to determine which draw method to use
            if (keyState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(+1, 0);
                objectSprites = runAnimation;
                isFacingRight = true;
            }
            //Moves the player down when pressing S by adding Y position value 
            if (keyState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, +1);
                objectSprites = runAnimation;
            }

            if (!keyState.IsKeyDown(Keys.S) && !keyState.IsKeyDown(Keys.W) && !keyState.IsKeyDown(Keys.A) && !keyState.IsKeyDown(Keys.D))
            {
                objectSprites = idleAnimation;
            }

            //Code needed so that the objects speed isn't increased when moving diagonally
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }


        }

        /// <summary>
        /// This method is used to Restore a small portion of hp every levelup
        /// </summary>
        /// <param name="levelup">A variable set to be 1 at the start of the game</param>
        /// <param name="ExperiencePoints.Playerlevel">The players actual level</param>
        public void Restorehealth()
        {
            //Checks if the players level has gone over the levelup variable, essentially checking if the player leveled up
            if (ExperiencePoints.PlayerLevel > Levelup)
            {
                //adds 3 health to the playerd HP pool while making sure it isn't above 100
                health += 3;
                if (health > 100)
                {
                    health = 100;
                }
                //Sets the levelup parameter to be even with the player level
                Levelup = ExperiencePoints.PlayerLevel;

                //Plays the levelup sound effect
                SoundEffectInstance newSoundIntance = levelupSound.CreateInstance();
                newSoundIntance.Volume = 0.2f;
                newSoundIntance.Play();
            }
        }

        /// <summary>
        /// This method is used for entering the "PowerState", where the players stats are doubled for a short duration indicated by the mana bar
        /// </summary>
        /// <param name="gameTime">Parameter given by the Monogame framework to simulate time</param>
        public void PowerState(GameTime gameTime)
        {
            //Keystate reads which key is being used
            KeyboardState keyState2 = Keyboard.GetState();

            //Checks if Space is pressed and if the players mana is available for use (which is 100 mana)
            if (keyState2.IsKeyDown(Keys.Space) && manaCooldown == false)
            {
                //Doubles all stats
                color = Color.Blue;
                scale *= 2;
                speed *= 2;
                cooldownTimerNumber /= 2f;
                cooldownTimerNumber2 /= 2f;
                Katana.ScaleValue *= 1.5f;
                Katana.SpeedValue *= 3f;
                Katana.TravelDistance = 0.8f;

                //Disables the use of space bar by setting manaCooldownn to true and activates the manaDrain effect in the if statement below
                manadecrease = true;
                manaCooldown = true;

                //Disables the players ability to choose upgrades if any are available, as they would give unintended results when PowerState is over
                UpgradeInterface.IsInteractive = false;
            }

            //Starts draining mana one at a time as long as its above 0
            if (mana > 0 && manadecrease == true)
            {
                manadecreasing += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (manadecreasing >= 0.05f)
                {
                    mana--;
                    manadecreasing = 0;
                }

            }

            //When mana reaches 0 and the mana drain effect is active it plays these following effects
            if (mana == 0 && manadecrease == true)
            {
                //disables the mana drain effect while enabling the mana regen effect
                manaRegen = true;
                manadecrease = false;

                //resets all stats to what they were before the PowerState
                color = Color.White;
                scale /= 2;
                speed /= 2;
                cooldownTimerNumber *= 2f;
                cooldownTimerNumber2 *= 2f;
                Katana.ScaleValue /= 1.5f;
                Katana.SpeedValue /= 3f;
                Katana.TravelDistance = 0.3f;

                //Enables the players ability to choose upgrades again
                UpgradeInterface.IsInteractive = true;
            }

            //Starts refilling mana over time (this rate is slower than the drain effect)
            if (manaRegen == true && mana < 100)
            {
                manaRegenerating += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (manaRegenerating >= 0.25f)
                {
                    mana++;
                    manaRegenerating = 0;
                }
            }

            //When mana reaches 100 it disables the mana regen effect and enables the spacebar to be used again
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

        /// <summary>
        /// This Method indicates what the player object does when colliding with another object
        /// </summary>
        /// <param name="other">"other" in this case would refer to the object that is colliding with the player object</param>
        public override void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// A Method that waits for an enemy to damage the player
        /// when the player is damaged it makes the enemy unable to hit the player and marks the player as red
        /// a timer starts and when it is over the player is tangible again for enemies to hit reseting the timer and color.
        /// </summary>
        /// <param name="gameTime">A parameter from the Framework that acts as a timer</param>
        public void Damaged(GameTime gameTime)
        {
            //When Damaged by an enemy they set this value to true on their collision method
            if (healthModified == true)
            {
                //when hit cooldown is true it turns the player red and prevents enemies from doing damage to the player. It also starts the a timer
                hitCooldown = true;
                if (hitCooldown == true)
                {
                    color = Color.Red;
                }
                hitCooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                //this timer if statement is hit almost immediately, making the code inside only able to play effectly once
                if (hitCooldownTimer <= 0.05f)
                {
                    //plays a Sound effect that indicates the player has been hit
                    SoundEffectInstance hurtSoundIntance = hurtSound.CreateInstance();
                    hurtSoundIntance.Volume = 0.3f;
                    hurtSoundIntance.Play();
                }

                //When the timer hits over this value it makes the player normal colored and enables them to be hit again
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
        /// Moreover this bool also activates the GameOver screen object and sound effect
        /// </summary>
        public void Death()
        {
            if (health <= 0)
            {
                //deletes player
                ToBeRemoved = true;

                //instantiates the gameover screen
                GameOverScreen gameover = new GameOverScreen();
                GameWorld.InstantiateGameObject(gameover);

                //plays the gameover sound effect
                SoundEffectInstance newSoundIntance = gameOverSound.CreateInstance();
                newSoundIntance.Volume = 0.5f;
                newSoundIntance.Play();
            }
        }

        /// <summary>
        /// The Attack method here opperates the players weapons and their attackspeed
        /// </summary>
        /// <param name="gameTime">FrameWork parameter, used to simulate in game time</param>
        public void Attack(GameTime gameTime)
        {
            //Gives the player use of both existing weapon types
            bool hasKatana = true;
            bool hasMagnum = true;

            //Checks if the Magnum is ready to attack
            if (cooldown == true)
            {
                //If the player has the magnum, the player attacks with a magnum shot
                if (hasMagnum == true)
                {
                    //instantiates a new bullet that spawns from the player
                    Magnum shot = new Magnum(magnumShot[0], new Vector2(position.X, position.Y));
                    GameWorld.InstantiateGameObject(shot);

                    //Plays a soundeffect to indicate that a bullet is being fired
                    SoundEffectInstance shootingSoundIntance = shootingSound.CreateInstance();
                    shootingSoundIntance.Volume = 0.04f;
                    shootingSoundIntance.Play();
                }

                //Sets the magnum to not be ready to attack
                cooldown = false;
            }

            //Starts a timer that will determine the time before the magnum is ready to shoot again, then resets the timer
            //This is essentially a "time between shots" indicator
            cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownTimer >= CooldownTimerNumber)
            {
                cooldown = true;
                cooldownTimer = 0;
            }

            //Checks if the Katana is ready to attack
            if (cooldown2 == true)
            {
                //If the player has the Katana, the player attacks with a swooping slash
                if (hasKatana == true)
                {
                    //instantiates a new slash sprite that spawns from the player
                    Katana slash = new Katana(katanaSlash[0], new Vector2(position.X, position.Y), gameTime);
                    GameWorld.InstantiateGameObject(slash);

                    //Makes a sword swoosh sound effect
                    SoundEffectInstance newSoundIntance = swordSound.CreateInstance();
                    newSoundIntance.Volume = 0.08f;
                    newSoundIntance.Play();

                    //In the Katana class there is a bool that determines a Draw() Method, this code cycles through those 2 each swing
                    if (Katana.AttackAnimation == true)
                    {
                        Katana.AttackAnimation = false;
                    }
                    else { Katana.AttackAnimation = true; }
                }

                //Sets the Katana to unable to swing
                cooldown2 = false;
            }

            //Like the magnum this is essentially a time between swings timer that determines when the Katana is ready to swing again
            cooldownTimer2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownTimer2 >= cooldownTimerNumber2)
            {
                cooldown2 = true;
                cooldownTimer2 = 0;
            }

        }
    }
}