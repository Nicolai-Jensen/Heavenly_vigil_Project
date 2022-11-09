using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// A projectile, which shoots after the nearest enemy.
    /// </summary>
    internal class Magnum : Weapon
    {

        // -----FIELDS-----
        
        //Current Magnum doesn't use rotation, but the field is available should we get different bullets than circles
        private float rotation;
        private static int damage;

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

        //Magnums Base constructor is made with a Texture2D and a position (in our case both are done inside player where it is instantiated)
        public Magnum(Texture2D sprite, Vector2 position)
        {
            // --These indicate the stats of the sprite like its position, size, damage and speed--

            //Gives the object the sprite from the parameter
            objectSprites = new Texture2D[1];
            objectSprites[0] = sprite;

            //Sets the magnum objects position to the position parameter
            this.position = position;

            //General stats used in the Draw() method
            scale = 1.5f;
            rotation = 0f;
            speed = 1500f;

            //Velocity is the direction it is moving, which uses the 2 methods for closest enemy and the player position
            //Determining this in the objects constructor makes sure it is only run once, so the bullets don't home in on enemies
            velocity = DirectionClosestEnemy(ReturnPlayerPosition());

            //A simple equation for determining damage, damagemultiplyer is determined by the DamageUp upgrade level
            damage = 1 + DamageMultiplyer;
        }

        // -----METHODS-----
        /// <summary>
        /// Draws out the sprite on screen with the Monogame Framework parameter
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[0], position, null, Color.Blue, rotation, origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// /// This Method loads in information and code needed for the game
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
        }

        /// <summary>
        /// Determines what this object does when colliding with another object
        /// </summary>
        /// <param name="other">"other" is the parameter that determines which other object</param>
        public override void OnCollision(GameObject other)
        {
            //Checks if it is colliding an Enemy class object, and removes the bullet
            if (other is Enemy)
            {
                toBeRemoved = true;
            }
        }

        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }



        /// <summary>
        /// This Method scouts the gameobjects list for the player object and returns the position of it
        /// </summary>
        /// <returns> Returns the Vector2 position of the player </returns>
        private Vector2 ReturnPlayerPosition()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go is Player)
                {
                    return go.Position;
                }
            }

            //If it fails to find a player in the list it returns this default position, it is however impossible to occur as if there is no player, no magnum bullets will be able to spawn
            return new Vector2(position.X, -100);
        }

        /// <summary>
        /// This Method is used to calculate which enemy in the gameobjects list is closest to the player(or the position you choose)
        /// </summary>
        /// <param name="playerPosition"> The position of the player or a position you choose</param>
        /// <returns>Returns a Vector2 position which acts as a direction </returns>
        public Vector2 DirectionClosestEnemy(Vector2 playerPosition)
        {
            //Instantiates a few variables we need in our method
            Vector2 direction;
            Vector2 enemyPosition = new Vector2(0, 0);
            //Distance between the player and an enemy, set to a high value to begin with to make sure it gets overwritten by the following code.
            float distance = 2000f;
            //The shortest distance found between the player and an enemy.
            float shortestDistance = 2000f;

            //Cycles through the gameobjects list. This loop is constantly updating the enemyPosition Vector2
            foreach (GameObject enemy in GameWorld.GameObjects)
            {
                //Finds Enemy class objects inside the list
                if (enemy is Enemy)
                {
                    //Sets the distance variable to be the value between the playerposition and the enemy position
                    //It does this by using the Distance method that comes with the framework
                    distance = Vector2.Distance(playerPosition, enemy.Position);
                }

                //Checks if the distance variable value is under the shortestdistance variable value and that it still belongs to the Enemy class
                if (distance < shortestDistance && enemy is Enemy)
                {
                    //Sets the shortestdistance variable to be equal to distances variable and saves the Enemy's position
                    shortestDistance = distance;
                    enemyPosition = enemy.Position;
                }
            }

            //Sets the direction Vector2 by subtracting the playerposition from the enemyposition, then normalizes the vector so it remains consistent regardless of direction
            direction = enemyPosition - playerPosition;
            direction.Normalize();

            return direction;
        }

    }
}
