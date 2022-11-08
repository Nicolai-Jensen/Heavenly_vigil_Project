using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Heavenly_vigil_Project
{
    /// <summary>
    /// Controls the Level up system, instantiates 3 Power-ups, and the Player picks 1 out of 3 power-ups.
    /// </summary>
    internal class UpgradeInterface : UserInterface
    {
        private Vector2 pos;
        //Rectangle for the Power-up background
        private Rectangle textureRectangle;
        private bool canBeChosen = false;
        private static bool isInteractive = true;
        private SpriteFont upgradeCount;
        KeyboardState currentKey;
        KeyboardState previousKey;

        public bool CanBeChosen
        {
            get { return canBeChosen; }
            set { canBeChosen = value; }
        }

        public static bool IsInteractive
        {
            get { return isInteractive; }
            set { isInteractive = value; }
        }

        public override void LoadContent(ContentManager content)
        {
            scale = 1; 
            textureRectangle = new Rectangle(0, 0, 400, 100);
            objectSprites = new Texture2D[1];
            pos.X = GameWorld.ScreenSize.X / 2;
            pos.Y = 910;
            objectSprites[0] = content.Load<Texture2D>("UIBase2");
            upgradeCount = content.Load<SpriteFont>("GameFont");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(objectSprites[0].Width / 2, objectSprites[0].Height / 2);
            spriteBatch.Draw(objectSprites[0], pos, null, Color.White, 0, origin, scale, SpriteEffects.None, 0);
            spriteBatch.DrawString(upgradeCount, $"Upgrades left: {GameWorld.UpgradeInterfaces.Count / 3}", new Vector2(pos.X - 60, 975), Color.White);
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
            GameWorld.InstantiateUpgrade(new DamageUp(new Vector2(830, 925), 2));
            GameWorld.InstantiateUpgrade(new AttackSpeedUp(new Vector2(960, 925), 1.1f));
            GameWorld.InstantiateUpgrade(new SpeedUp(new Vector2(1090, 925), 30f));

        }
        /// <summary>
        /// You choose and upgrade using Key 1, 2 or 3. and adds the value to the player.  After upgrade have been chosen its getting removed from the screen.
        /// </summary>
        /// <param name="gameTime"></param>
        public void HandleUpgradeInput(GameTime gameTime)
        {
            previousKey = currentKey;
            currentKey = Keyboard.GetState();

            if (currentKey.IsKeyDown(Keys.D1) && previousKey.IsKeyUp(Keys.D1) && canBeChosen && isInteractive == true)
            {
                GameWorld.UpgradeInterfaces.ElementAt(2).AddValue();

                RemoveUpgradeInterface();
                toBeRemoved = true;

            }
            else if (currentKey.IsKeyDown(Keys.D2) && previousKey.IsKeyUp(Keys.D2) && canBeChosen && isInteractive == true)
            {
                GameWorld.UpgradeInterfaces.ElementAt(1).AddValue();

                RemoveUpgradeInterface();
                toBeRemoved = true;
            }
            else if (currentKey.IsKeyDown(Keys.D3) && previousKey.IsKeyUp(Keys.D3) && canBeChosen && isInteractive == true)
            {
                GameWorld.UpgradeInterfaces.ElementAt(0).AddValue();

                RemoveUpgradeInterface();
                toBeRemoved = true;
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