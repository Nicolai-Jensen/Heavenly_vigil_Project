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
        

        public override void LoadContent(ContentManager content)
        {
            objectSprites[0] = content.Load<Texture2D>("");
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
