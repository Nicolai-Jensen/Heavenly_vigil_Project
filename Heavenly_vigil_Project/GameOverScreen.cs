using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Heavenly_vigil_Project
{
    internal class GameOverScreen : GameObject
    {
        Rectangle gameOverRectangle;
        SpriteFont gameOver;
        public override void LoadContent(ContentManager content)
        {
            objectSprites = new Texture2D[1];
            objectSprites[0] = content.Load<Texture2D>("Game_Over");
            gameOverRectangle = new Rectangle(0, 0, objectSprites[0].Width, objectSprites[0].Height);
            gameOver = content.Load<SpriteFont>("TimerFont");
        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            spriteBatch.Draw(objectSprites[0], new Vector2(950, 450), gameOverRectangle, Color.White, 0f, origin, 1, SpriteEffects.None, 1f);
            spriteBatch.DrawString(gameOver, "Press 'R' to reset the game", new Vector2(720, 700), Color.Red);
        }      
    }
}
