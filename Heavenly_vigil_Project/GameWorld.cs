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

        private static List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        private List<GameObject> gameObjectsToRemove = new List<GameObject>();
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
            gameObjects.Add(new UserInterface());
            gameObjects.Add(player1);
            foreach (GameObject go in gameObjects)
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
                DrawCollisionBox(go);
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


        public static void InstantiateGameObject(GameObject gObject)
        {
            gameObjectsToAdd.Add(gObject);
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
    }
}