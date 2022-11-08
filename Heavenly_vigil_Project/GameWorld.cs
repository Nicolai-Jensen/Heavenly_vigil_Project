using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace Heavenly_vigil_Project
{
    public class GameWorld : Game
    {
        //-----FIELDS-----

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();
        private static Stack<PowerUp> upgradeInterfaces = new Stack<PowerUp>();
        private static List<PowerUp> upgradeIToAdd = new List<PowerUp>();
        private List<PowerUp> upgradeIToRemove = new List<PowerUp>();
        private Texture2D pixel;
        private float spawnTimer;

        private static Vector2 screenSize;

        //-----PROPERTIES-----
        public static Vector2 ScreenSize
        {
            get
            {
                return screenSize;
            }
        }

        public static List<GameObject> GameObjects
        {
            get { return gameObjects; }
        }

        public static Stack<PowerUp> UpgradeInterfaces
        {
            get { return upgradeInterfaces; }
            set { upgradeInterfaces = value; }
        }

        //-----CONSTRUCTORS-----

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);


        }

        //-----METHODS-----

        protected override void Initialize()
        {
            Player player1 = new Player(new Vector2(0, 50));
            gameObjects.Add(new Background());
            gameObjects.Add(new UserInterface());
            gameObjects.Add(player1);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //Player player1 = new Player(new Vector2(0, 50));
            //gameObjects.Add(new Background());
            //gameObjects.Add(new UserInterface());
            //gameObjects.Add(player1);
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }
            foreach (PowerUp go in upgradeInterfaces)
            {
                go.LoadContent(Content);
            }
            pixel = Content.Load<Texture2D>("pixel");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SpawnEnemy(gameTime);
            RemoveGameObjects();
            ResetInitialize();

            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);

                foreach (GameObject other in gameObjects)
                {
                    if (go.IsColliding(other))
                    {
                        go.OnCollision(other);
                        other.OnCollision(go);
                    }
                }
            }

            foreach (GameObject gameObjectsToSpawn in gameObjectsToAdd)
            {
                gameObjectsToSpawn.LoadContent(Content);
                gameObjects.Add(gameObjectsToSpawn);
            }

            gameObjectsToAdd.Clear();

            // UPGRADE INTERFACE

            foreach (PowerUp go in upgradeInterfaces)
            {
                go.Update(gameTime);

                foreach (PowerUp other in upgradeInterfaces)
                {
                    if (go.IsColliding(other))
                    {
                        go.OnCollision(other);
                        other.OnCollision(go);
                    }
                }
            }

            foreach (PowerUp gameObjectsToSpawn in upgradeIToAdd)
            {
                gameObjectsToSpawn.LoadContent(Content);
                upgradeInterfaces.Push(gameObjectsToSpawn);
            }

            upgradeIToAdd.Clear();

            SetUpgradeCanBeChosen();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.FrontToBack);


            foreach (GameObject go in gameObjects)
            {
                go.Draw(_spriteBatch);
                DrawCollisionBox(go);
            }
            foreach (PowerUp go in upgradeInterfaces)
            {
                go.Draw(_spriteBatch);
                DrawCollisionBox(go);
            }


            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        /// <summary>
        /// Checks if a gameObject is out of bounds, or should be removed. If true. Adds the Gameobject to the gameObjectsToRemove list.
        ///  Checks if a Power-up should be removed. If true. Adds the Power-up to the upgradeIToRemove list. and pops the stack.
        /// </summary>
        private void RemoveGameObjects()
        {
            foreach (GameObject go in gameObjects)
            {
                bool shouldRemoveGameObject = go.IsOutOfBounds();
                if (shouldRemoveGameObject || go.ToBeRemoved)
                {
                    gameObjectsToRemove.Add(go);
                }
            }

            foreach (GameObject goToRemove in gameObjectsToRemove)
            {
                gameObjects.Remove(goToRemove);
            }

            // UPGRADE INTERFACE

            foreach (PowerUp go in upgradeInterfaces)
            {
                bool shouldRemoveGameObject = go.IsOutOfBounds();
                if (shouldRemoveGameObject || go.ToBeRemoved)
                {
                    upgradeIToRemove.Add(go);
                }
            }

            foreach (PowerUp goToRemove in upgradeIToRemove)
            {
                upgradeInterfaces.Pop();
            }
        }

        /// <summary>
        /// Adds a Gameobject to the gameObjectsToAdd list.
        /// </summary>
        /// <param name="gObject"></param>
        public static void InstantiateGameObject(GameObject gObject)
        {
            gameObjectsToAdd.Add(gObject);
        }
        /// <summary>
        /// Adds a Gameobject to the upgradeIToAdd list.
        /// </summary>
        /// <param name="gameObject"></param>
        public static void InstantiateUpgrade(PowerUp gameObject)
        {
            upgradeIToAdd.Add(gameObject);
        }
        /// <summary>
        /// After a set amount of time, it instantiates an enemy, and adds it to the gameObjects list as long as the amount of total enemies is below 100.
        /// </summary>
        /// <param name="gameTime"></param>
        private void SpawnEnemy(GameTime gameTime)
        {
            int enemyCount = 0;

            //Loops through the list "gameObjects" to count the amount of enemies currently instantiated.
            foreach (GameObject go in gameObjects)
            {
                if (go is Enemy)
                    enemyCount++;
            }

            //If the timer is above a value that scales with minutes survived in the game and the amount of enemies is below 100, instantiates an enemy.
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer > 1 - Math.Min((TimeManager.timerMinutes * 0.1) * 0.9, 0.8f) && enemyCount < 100)
            {
                Enemy spawnedEnemy = new Enemy();
                spawnedEnemy.LoadContent(Content);
                gameObjects.Add(spawnedEnemy);
                spawnTimer = 0;
            }

        }
        /// <summary>
        /// Draws a CollisionBoxto all gameObjects on screen.
        /// </summary>
        /// <param name="go"></param>
        private void DrawCollisionBox(GameObject go)

        {
            Rectangle top = new Rectangle(go.CollisionBox.X, go.CollisionBox.Y, go.CollisionBox.Width, 1);
            Rectangle bottom = new Rectangle(go.CollisionBox.X, go.CollisionBox.Y + go.CollisionBox.Height, go.CollisionBox.Width, 1);
            Rectangle left = new Rectangle(go.CollisionBox.X, go.CollisionBox.Y, 1, go.CollisionBox.Height);
            Rectangle right = new Rectangle(go.CollisionBox.X + go.CollisionBox.Width, go.CollisionBox.Y, 1, go.CollisionBox.Height);

            _spriteBatch.Draw(pixel, top, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(pixel, bottom, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(pixel, left, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(pixel, right, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);

        }

        private void SetUpgradeCanBeChosen()
        {
            for (int i = 0; i < gameObjects.Count; i++)
            {
                if (gameObjects[i] is UpgradeInterface)
                {
                    UpgradeInterface activeUpgrade = (UpgradeInterface)gameObjects[i];

                    activeUpgrade.CanBeChosen = true;

                    break;
                }
            }
        }
        public void ResetInitialize()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                foreach (GameObject go in GameWorld.GameObjects)
                {
                    go.ToBeRemoved = true;
                }
                foreach (PowerUp go in GameWorld.UpgradeInterfaces)
                {
                    go.ToBeRemoved = true;
                }
                this.Initialize();
                ExperiencePoints.MaxEXP = 100;
                ExperiencePoints.PlayerLevel = 1;
                ExperiencePoints.PlayerExp = 0;
                TimeManager.timerSeconds = 0;
                TimeManager.timerMinutes = 0;
            }



        }
    }
}