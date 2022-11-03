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
        private Rectangle greenRectangle;
        private Rectangle blackRectangle;
        private Rectangle blueRectangle;
        private Rectangle xpBlackrectangle;
        private SpriteFont gameFont;
        private SpriteFont timerFont;
        

        //Properties

        //Constructors
        //Methods
        /// <summary>
        /// This Method loads in information and code needed for the game
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[3];
            objectSprites[0] = content.Load<Texture2D>("GreenHealth");
            objectSprites[1] = content.Load<Texture2D>("BlackHealth");
            objectSprites[2] = content.Load<Texture2D>("BlueEXP");
            position = new Vector2(30, 50);
            greenRectangle = new Rectangle(10, 10, Player.Health * 2, 20);
            blackRectangle = new Rectangle(0, 0, 220, 40);
            blueRectangle = new Rectangle(0, 0, ExperiencePoints.ExpPercentage, 20);
            xpBlackrectangle = new Rectangle(0, 0, 1900, 40);
            gameFont = content.Load<SpriteFont>("GameFont");
            timerFont = content.Load<SpriteFont>("TimerFont");

        }
        /// <summary>
        /// This Update Method constantly loops throughout the program aslong as it is running, other methods we want to be looped are called inside this one
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            TimeManager.UpdateClock(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                ExperiencePoints.PlayerExp++;
                ExperiencePoints.CheckForLevelUp();
                blueRectangle.Width = (int)(ExperiencePoints.ExpPercentage * 18.75);
            }
                greenRectangle.Width = Player.Health * 2;

            
        }
        /// <summary>
        /// The Method for drawing out a sprite to the screen, this method is an override for the virtual one in GameObject and is called in GameWorld
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 10), blackRectangle, Color.White);
            spriteBatch.Draw(objectSprites[0], new Vector2(20, 20), greenRectangle, Color.White);
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 1000), xpBlackrectangle, Color.White);
            spriteBatch.Draw(objectSprites[2], new Vector2(20, 1010), blueRectangle, Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, $"Lvl. {ExperiencePoints.PlayerLevel}", new Vector2(950, 1009), Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(timerFont, $"{TimeManager.timerMinutes} : {TimeManager.timerSeconds}", new Vector2(GameWorld.ScreenSize.X / 2, 10), Color.White);
        }
    }
}
