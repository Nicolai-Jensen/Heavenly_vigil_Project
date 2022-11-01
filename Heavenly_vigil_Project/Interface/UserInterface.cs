﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Heavenly_vigil_Project
{
    internal class UserInterface : GameObject
    {
        //Fields
        private Rectangle greenRectangle;
        private Rectangle blackRectangle;

        //Properties

        //Constructors
        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[2];
            objectSprites[0] = content.Load<Texture2D>("GreenHealth");
            objectSprites[1] = content.Load<Texture2D>("BlackHealth");
            position = new Vector2(30, 50);
            greenRectangle = new Rectangle(10, 10, 200, 20);
            blackRectangle = new Rectangle(0, 0, 220, 40);

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                greenRectangle.Width -= 1;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 10), blackRectangle, Color.White);
            spriteBatch.Draw(objectSprites[0], new Vector2(20, 20), greenRectangle, Color.White);
        }
    }
}
