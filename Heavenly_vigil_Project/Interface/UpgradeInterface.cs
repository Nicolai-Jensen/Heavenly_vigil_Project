﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Heavenly_vigil_Project
{
    internal class UpgradeInterface : UserInterface
    {
        private Vector2 pos;
        //Rectangle for the Power-up background
        private Rectangle textureRectangle;
        private bool canBeChosen = false;
        private SpriteFont upgradeCount;
        private bool cooldown = true;
        private float cooldownTimer;

        public bool CanBeChosen
        {
            get { return canBeChosen; }
            set { canBeChosen = value; }
        }

        public override void LoadContent(ContentManager content)
        {
            scale = 1; 
            textureRectangle = new Rectangle(0, 0, 400, 100);
            objectSprites = new Texture2D[1];
            pos.X = 1010;
            pos.Y = 975;
            objectSprites[0] = content.Load<Texture2D>("BlackHealth");
            upgradeCount = content.Load<SpriteFont>("GameFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(250, 100);
            spriteBatch.Draw(objectSprites[0], pos, textureRectangle, Color.White, 0, origin, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(upgradeCount, $"Upgrades left: {GameWorld.UpgradeInterfaces.Count / 3}", new Vector2(pos.X - 110, pos.Y), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            HandleUpgradeInput(gameTime);
        }

        /// <summary>
        /// instantiates a black background and 3 upgrades to choose from.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void ChooseUpgrade(GameTime gameTime)
        {

            GameWorld.InstantiateGameObject(new UpgradeInterface());
            GameWorld.InstantiateUpgrade(new AttackSpeedUp(new Vector2(960, 925), 2));
            GameWorld.InstantiateUpgrade(new DamageUp(new Vector2(830, 925), 2));
            GameWorld.InstantiateUpgrade(new SpeedUp(new Vector2(1090, 925), 400f));

        }
        /// <summary>
        /// You choose and upgrade using Key 1, 2 or 3. and adds the value to the player.  After upgrade have been chosen its getting removed from the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void HandleUpgradeInput(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();

            if (cooldown == true)
            {

                if (keyState.IsKeyDown(Keys.D1) && canBeChosen)
                {
                    GameWorld.UpgradeInterfaces.ElementAt(0).AddValue();

                    RemoveUpgradeInterface();
                }
                else if (keyState.IsKeyDown(Keys.D2) && canBeChosen)
                {
                    GameWorld.UpgradeInterfaces.ElementAt(1).AddValue();

                    RemoveUpgradeInterface();
                }
                else if (keyState.IsKeyDown(Keys.D3) && canBeChosen)
                {
                    GameWorld.UpgradeInterfaces.ElementAt(0).AddValue();

                    RemoveUpgradeInterface();
                    toBeRemoved = true;
                }

                cooldown = false;
            }
            cooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (cooldownTimer >= 0.2f)
            {
                cooldown = true;
                cooldownTimer = 0;
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

        /// <summary>
        /// Removes the UpgradeInterface, After you pick and Upgrade.
        /// </summary>
        private void RemoveUpgradeInterface()
        {
            GameWorld.UpgradeInterfaces.Pop();
            GameWorld.UpgradeInterfaces.Pop();
            GameWorld.UpgradeInterfaces.Pop();
        }

    }
}