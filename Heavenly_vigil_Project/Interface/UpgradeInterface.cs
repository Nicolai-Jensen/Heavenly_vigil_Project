using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heavenly_vigil_Project
{
    internal class UpgradeInterface : UserInterface
    {
        private Vector2 pos;
        private Rectangle textureRectangle;

        public static UserInterface lvlup;
        public static AttackSpeedUp atkSpd;
        public static DamageUp dmgUp;
        public static SpeedUp spdUp;

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

        public override void Update(GameTime gameTime)
        {
            HandleUpgradeInput(gameTime);
        }

        public static void ChooseUpgrade(GameTime gameTime)
        {

            lvlup = new UpgradeInterface();
            atkSpd = new AttackSpeedUp(new Vector2(960, 925), 2);
            dmgUp = new DamageUp(new Vector2(830, 925), 2);
            spdUp = new SpeedUp(new Vector2(1090, 925), 400f);


            //All the instantiated Power-ups and UpgradeInterface, being added to the GameObjectListToAdd.
            GameWorld.InstantiateGameObject(lvlup);
            GameWorld.InstantiateGameObject(atkSpd);
            GameWorld.InstantiateGameObject(dmgUp);
            GameWorld.InstantiateGameObject(spdUp);


        }

        public void HandleUpgradeInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();


            if (keyState.IsKeyDown(Keys.D1))
            {
                dmgUp.AddValue();

                RemoveUpgradeInterface();
            }
            else if(keyState.IsKeyDown(Keys.D2))
            {
                atkSpd.AddValue();

                RemoveUpgradeInterface();
            }
            else if(keyState.IsKeyDown(Keys.D3))
            {
                spdUp.AddValue();

                RemoveUpgradeInterface();
            }
        }
        public Player ReturnPlayer()
        {
            foreach (GameObject go in GameWorld.GameObjects)
            {
                if (go is Player)
                {
                    return (Player)go;
                }
            }
            return new Player(new(0, 0));
        }

        private void RemoveUpgradeInterface()
        {
            spdUp.ToBeRemoved = true;
            atkSpd.ToBeRemoved = true;
            dmgUp.ToBeRemoved = true;
            lvlup.ToBeRemoved = true;
        }

    }
}