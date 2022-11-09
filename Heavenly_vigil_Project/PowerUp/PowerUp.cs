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
    /// Super class for the Power-ups. holds the fields and Methods, the other powerups should have.
    /// </summary>
    public abstract class PowerUp : GameObject
    {
        //Fields
        protected int valueAmp;
        protected string powerUpTitle;

        //Methods
        public override void LoadContent(ContentManager content)
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }
        public abstract void AddValue();
        /// <summary>
        /// Goes through the GameObjects list to find the Player, and returns the Player.
        ///  If no Player is found, return new Player(0, 0).
        /// </summary>
        /// <returns></returns>
        public Player ReturnPlayer()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go is Player)
                {
                    return (Player)go;
                }
            }
            return new Player(new(0, 0)); ;
        }
    }
}
