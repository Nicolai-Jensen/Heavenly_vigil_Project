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
    /// <summary>
    /// Sets a constructor, load and draws the Speedup Powerup, and Adds the value to the player. 
    /// </summary>
    internal class DamageUp : PowerUp
    {
        //Fields
        public int damageAmp;
        private SpriteFont titleFont;
        //Properties
        //Constructors

        /// <summary>
        /// Sets the specified variables when the object is instantiated.
        /// </summary>
        /// <param name="position">Position of the PowerUp</param>
        /// <param name="damageAmp"></param>
        public DamageUp(Vector2 position, int damageAmp)
        {
            this.position = position;
            this.damageAmp = damageAmp;
            powerUpDescription = "increases the players damage";
            powerUpTitle = "Damage";
            scale = 1f;
        }
        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];

            objectSprites[0] = content.Load<Texture2D>("PowerUpDamage");

            titleFont = content.Load<SpriteFont>("GameFont");
        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            spriteBatch.Draw(objectSprites[0], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(titleFont, powerUpTitle, new Vector2(position.X - objectSprites[0].Width / 2 - 5, position.Y - objectSprites[0].Height / 2 - 25), Color.White);
            spriteBatch.DrawString(titleFont, "1.", new Vector2(position.X - objectSprites[0].Width / 2 - 30, position.Y - objectSprites[0].Height / 2 + 15), Color.White);
        }

        /// <summary>
        /// Adds value to the damageMultiplier of both the Magnum and Katana classes.
        /// </summary>
        public override void AddValue()
        {

            Katana.DamageMultiplyer += 2;
            Magnum.DamageMultiplyer += 1;
        }

    }
}
