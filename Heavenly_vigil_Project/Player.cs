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
        private static int health;

        // -----PROPERTIES-----

        public static int Health
        {
            get { return health; }
            set { health = value; }
        }

        // -----CONSTRUCTORS-----
        public Player(Vector2 vector2)
        {
            position = vector2;
            scale = 2f;
            health = 100;
            speed = 400f;
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
        }

        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            //Calls Method for player inputs
            HandleInput(gameTime);
            //Calls the Method needed for objects to be moveable
            Move(gameTime);
            //Calls the Method that controls where the screenlimits of the player sprite are
            ScreenWrap();
            //Calls the GameObject Method for animating a sprite with elapsed time
            Animate(gameTime);
        }

        /// <summary>
        /// The Method for Drawing out a sprite to the screen, this method is an override for the abstract one in GameObject and is called in GameWorld
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            //A Draw Method with different overloads, this particular one has 10 variables which can be defined
            spriteBatch.Draw(objectSprites[(int)animationTime], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0);
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

    }
}
