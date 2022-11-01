using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Heavenly_vigil_Project
{
    internal class UserInterface : GameObject
    {
        //Fields
        private Rectangle rectangle;

        //Properties

        //Constructors
        //Methods
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>("tile_arara_azul");
            position = new Vector2(30, 50);
            rectangle = new Rectangle(0, 0, objectSprites[0].Width, objectSprites[0].Height);

        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                rectangle.Width -= 1;
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[0], position, rectangle, Color.White);
        }
    }
}
