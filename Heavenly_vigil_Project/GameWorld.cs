using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Heavenly_vigil_Project
{
    public class GameWorld : Game
    {
        //-----FIELDS-----

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player player1 = new Player(new Vector2(0, 50));
            gameObjects.Add(new Background());
            gameObjects.Add(player1);
            gameObjects.Add(new UserInterface());
            foreach (GameObject go in gameObjects)
            {
                go.LoadContent(Content);
            }

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SpawnEnemy(gameTime);
            RemoveGameObjects();

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
            }


            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

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
        }

        private void SpawnEnemy(GameTime gameTime)
        {
            spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (spawnTimer > 1)
            {
                Enemy spawnedEnemy = new Enemy();
                spawnedEnemy.LoadContent(Content);
                gameObjects.Add(spawnedEnemy);
                spawnTimer = 0;
            }

        }
    }
}