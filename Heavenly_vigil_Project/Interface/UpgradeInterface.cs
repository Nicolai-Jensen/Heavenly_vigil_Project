using Heavenly_vigil_Project;
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
            textureRectangle = new Rectangle(0, 0, 400, 100);
            objectSprites = new Texture2D[1];
            pos.X = 1010;
            pos.Y = 975;
            objectSprites[0] = content.Load<Texture2D>("BlackHealth");
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(250, 100);
            spriteBatch.Draw(objectSprites[0], pos, textureRectangle, Color.White, 0, origin, scale, SpriteEffects.None, 0);
        }
        public static void ChooseUpgrade(GameTime gameTime)
        {
            UpgradeInterface lvlup = new UpgradeInterface();
            AttackSpeedUp atkSpd = new AttackSpeedUp(new Vector2(960, 925), 2);
            DamageUp dmgUp = new DamageUp(new Vector2(830, 925), 2);
            SpeedUp spdUp = new SpeedUp(new Vector2(1090, 925), 2);

            GameWorld.InstantiateGameObject(lvlup);
            GameWorld.InstantiateGameObject(atkSpd);
            GameWorld.InstantiateGameObject(dmgUp);
            GameWorld.InstantiateGameObject(spdUp);
        }
    }
}