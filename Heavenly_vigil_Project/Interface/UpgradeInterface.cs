using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using Keyboard = Microsoft.VisualBasic.Devices.Keyboard;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace Heavenly_vigil_Project
{
    internal class UpgradeInterface : UserInterface
    {
        private Vector2 pos;
        private Rectangle textureRectangle;
        public override void LoadContent(ContentManager content)
        {
            scale = 1; 
            textureRectangle = new Rectangle(0, 0, 500, 200);
            objectSprites = new Texture2D[1];
            pos.X = GameWorld.ScreenSize.X / 2;
            pos.Y = 875;
            objectSprites[0] = content.Load<Texture2D>("BlackHealth");
        }

        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(250, 100);
            spriteBatch.Draw(objectSprites[0], pos, textureRectangle, Color.White, 0, origin, scale, SpriteEffects.None, 0);
        }
    }
}
