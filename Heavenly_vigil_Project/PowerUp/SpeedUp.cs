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
            spriteBatch.Draw(objectSprites[0], position, Color.White);
        }
        public override void AddValue(GameObject player)
        {
            player.Speed += speedAmp;
        }
    }
}
