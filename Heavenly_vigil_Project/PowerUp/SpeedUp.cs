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
    internal class SpeedUp : PowerUp
    {
        //Fields
        public float speedAmp;
        //Properties
        //Constructors
        public SpeedUp(Vector2 position, float speedAmp)
        {
            this.position = position;
            this.speedAmp = speedAmp;
            powerUpDescription = "Increases the players speed.";
            powerUpTitle = "Speed";
            scale = 0.75f;
        }

        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];

            objectSprites[0] = content.Load<Texture2D>("GreenHealth");
        }
        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            spriteBatch.Draw(objectSprites[0], position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
        }
        public override void AddValue()
        {
            Player player = ReturnPlayer();
            player.Speed += speedAmp;
        }

    }
}
