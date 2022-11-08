using Microsoft.Xna.Framework;
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
    /// <summary>
    /// Sets a constructor, load and draws the Speedup Powerup, and Adds the value to the player. 
    /// </summary>
    internal class AttackSpeedUp : PowerUp
    {
        //Fields
        public float attackSpdAmp;
        private SpriteFont titleFont;
        //Properties
        //Constructors
        public AttackSpeedUp(Vector2 position, float attackSpdAmp)
        {
            this.position = position;
            this.attackSpdAmp = attackSpdAmp;
            powerUpDescription = "Increases the players attackspeed.";
            powerUpTitle = "Attackspeed";
            scale = 1f;
            
        }
        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>("PowerUpAttackSpeed");
            titleFont = content.Load<SpriteFont>("GameFont");

        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            spriteBatch.Draw(objectSprites[0], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(titleFont, powerUpTitle, new Vector2(position.X - objectSprites[0].Width / 2 - 25, position.Y - objectSprites[0].Height / 2 - 25), Color.White);
            spriteBatch.DrawString(titleFont, "2.", new Vector2(position.X - objectSprites[0].Width / 2 - 30, position.Y - objectSprites[0].Height / 2 + 15), Color.White);
        }

        public override void AddValue()
        {
            Player player = ReturnPlayer();
            player.CooldownTimerNumber /= attackSpdAmp;
            player.CooldownTimerNumber2 /= attackSpdAmp;
        }


    }
}
