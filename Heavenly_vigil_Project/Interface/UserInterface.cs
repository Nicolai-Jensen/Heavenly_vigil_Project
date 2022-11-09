using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// Controls the UI of the game.
    /// </summary>
    internal class UserInterface : GameObject
    {
        //Fields
        //Rectangle for the health bar.
        private Rectangle greenRectangle;
        //Rectangle for the health background
        private Rectangle blackRectangle;
        //Rectangle for the xp bar.
        private Rectangle blueRectangle;
        //Rectangle for the xp bar background
        private Rectangle xpBlackrectangle;
        //Rectangle for the mana bar
        private Rectangle manaRectangle;
        private SpriteFont gameFont;
        private SpriteFont timerFont;



        //Properties

        //Constructors
        //Methods

        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[6];
            objectSprites[0] = content.Load<Texture2D>("GreenHealth");
            objectSprites[1] = content.Load<Texture2D>("HealthUI");
            objectSprites[2] = content.Load<Texture2D>("BlueEXP");
            objectSprites[3] = content.Load<Texture2D>("UIEXPBase");
            objectSprites[4] = content.Load<Texture2D>("Tutorial");
            objectSprites[5] = content.Load<Texture2D>("UIBlock");
            greenRectangle = new Rectangle(10, 10, Player.Health * 2, 20);
            blueRectangle = new Rectangle(0, 0, ExperiencePoints.ExpPercentage, 20);
            manaRectangle = new Rectangle(0, 0, Player.Mana, 20);
            gameFont = content.Load<SpriteFont>("GameFont");
            timerFont = content.Load<SpriteFont>("TimerFont");

        }

        public override void Update(GameTime gameTime)
        {
            TimeManager.UpdateClock(gameTime);
            ExperiencePoints.CheckForLevelUp(gameTime);
            blueRectangle.Width = (int)(ExperiencePoints.ExpPercentage * 18.75);
            greenRectangle.Width = Player.Health * 2;
            manaRectangle.Width = Player.Mana * 2;


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 10), Color.White);
            spriteBatch.Draw(objectSprites[0], new Vector2(20, 20), greenRectangle, Color.White);
            spriteBatch.Draw(objectSprites[3], new Vector2(10, 1000), Color.White);
            spriteBatch.Draw(objectSprites[2], new Vector2(20, 1010), blueRectangle, Color.White);
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 55), Color.White);
            spriteBatch.Draw(objectSprites[2], new Vector2(20, 65), manaRectangle, Color.White);
            spriteBatch.Draw(objectSprites[4], new Vector2(1650, 20), null, Color.White, 0f, origin, 0.5f, SpriteEffects.None, 1f);
            spriteBatch.Draw(objectSprites[5], new Vector2(886, 2), null, Color.White, 0f, origin, 2f, SpriteEffects.None, 1f);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, $"Lvl. {ExperiencePoints.PlayerLevel}", new Vector2(950, 1009), Color.White);
            spriteBatch.End();
            spriteBatch.Begin();

            spriteBatch.DrawString(timerFont, $"{TimeManager.timerMinutes} : {TimeManager.timerSeconds}", new Vector2(GameWorld.ScreenSize.X / 2 - 57, 10), Color.White);
         }
    }
}
