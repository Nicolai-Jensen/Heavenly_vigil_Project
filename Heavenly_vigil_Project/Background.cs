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
    internal class Background : GameObject
    {
        //Fields

        //Properties

        //Constructors

        //Methods

        /// <summary>
        /// This Method loads in information and code needed for the game
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>("Baggrund");
        }

        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            
        }
        /// <summary>
        /// The Method for Drawing out a sprite to the screen, this method is an override for the abstract one in GameObject and is called in GameWorld
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[0], Vector2.Zero, Color.White);
        }
    }
}
