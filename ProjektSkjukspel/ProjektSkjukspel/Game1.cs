using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjektSkjukspel.Screens;

namespace ProjektSkjukspel
{
    enum Screen
    {
        PlayScreen,
        EditorScreen,
        GameOverScreen
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Screen currentScreen;

        GameOverScreen gameOverScreen;
        PlayScreen playScreen;
        MapEditor mapEditor;

        public const int tileSize = 64;
        public const int windowHeight = 768;
        public const int windowWidth = 1024;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mapEditor = new MapEditor(this);
            currentScreen = Screen.EditorScreen;
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (currentScreen != Screen.PlayScreen && Input.KeyClick(Keys.P))
            {
                playScreen = new PlayScreen(this);
                currentScreen = Screen.PlayScreen;
            }
            else if (currentScreen != Screen.EditorScreen && Input.KeyClick(Keys.E))
            {
                mapEditor = new MapEditor(this);
                currentScreen = Screen.EditorScreen;
            }

            switch (currentScreen)
            {
                case Screen.PlayScreen:
                    playScreen.Update(gameTime);
                    break;
                case Screen.EditorScreen:
                    mapEditor.Update(gameTime);
                    break;
                case Screen.GameOverScreen:
                    gameOverScreen.Update();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            switch (currentScreen)
            {
                case Screen.PlayScreen:
                    playScreen.Draw(spriteBatch);
                    break;
                case Screen.EditorScreen:
                    mapEditor.Draw(spriteBatch);
                    break;
                case Screen.GameOverScreen:
                    gameOverScreen.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GameOver()
        {
            gameOverScreen = new GameOverScreen(this);
            currentScreen = Screen.GameOverScreen;
        }
    }
}
