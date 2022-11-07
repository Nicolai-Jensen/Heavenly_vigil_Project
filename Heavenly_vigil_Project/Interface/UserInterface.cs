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
            objectSprites = new Texture2D[3];
            objectSprites[0] = content.Load<Texture2D>("GreenHealth");
            objectSprites[1] = content.Load<Texture2D>("BlackHealth");
            objectSprites[2] = content.Load<Texture2D>("BlueEXP");
            greenRectangle = new Rectangle(10, 10, Player.Health * 2, 20);
            blackRectangle = new Rectangle(0, 0, 220, 40);
            blueRectangle = new Rectangle(0, 0, ExperiencePoints.ExpPercentage, 20);
            manaRectangle = new Rectangle(0, 0, Player.Mana, 20);
            xpBlackrectangle = new Rectangle(0, 0, 1900, 40);
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
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 10), blackRectangle, Color.White);
            spriteBatch.Draw(objectSprites[0], new Vector2(20, 20), greenRectangle, Color.White);
            spriteBatch.Draw(objectSprites[1], new Vector2(10, 1000), xpBlackrectangle, Color.White);
            spriteBatch.Draw(objectSprites[2], new Vector2(20, 1010), blueRectangle, Color.White);
            spriteBatch.Draw(objectSprites[2], new Vector2(20, 60), manaRectangle, Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(gameFont, $"Lvl. {ExperiencePoints.PlayerLevel}", new Vector2(950, 1009), Color.White);
            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.DrawString(timerFont, $"{TimeManager.timerMinutes} : {TimeManager.timerSeconds}", new Vector2(GameWorld.ScreenSize.X / 2, 10), Color.White);
        }
    }
}
