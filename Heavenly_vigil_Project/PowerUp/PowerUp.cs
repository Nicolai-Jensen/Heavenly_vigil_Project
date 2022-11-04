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
    public abstract class PowerUp : GameObject
    {
        //Fields
        protected int valueAmp;
        protected string powerUpDescription;
        protected string powerUpTitle;
        //Properties
        //Constructors
        //Methods
        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }
        public abstract void AddValue(GameObject player);
    }
}
