﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    internal class AttackSpeedUp : PowerUp
    {
        //Fields
        public int attackSpdAmp;
        //Properties
        //Constructors
        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];

            objectSprites[0] = content.Load<Texture2D>("GreenHealth");

        }
        public override void Update(GameTime gameTime)
        {
            
        }
        public override void AddValue(GameObject player)
        {
           
        }
    }
}
