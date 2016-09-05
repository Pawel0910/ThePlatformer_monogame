using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Profile;

namespace ThePlatformer.View.Menu
{
    public class MenuViewManager
    {
        private Game1 game;
        private MainMenu mainMenu;
        private GraphicsDevice graphics;
        private cButton btnPlay;
        private cButton backToGameButton, exitButton, restartButton;
        private bool pause = false;
        private SpriteFont font;
        private long elapsed, delay = 100;
        private float transparency = 1f, factorTransparency = -0.1f;
        private readonly string backToGame = "Press 'Escape' to return";
        private readonly string quitGame = "Press 'Q' to quit game";
        private readonly string startGame = "Press 'Enter' to start game";
        private readonly string restartGame = "Press 'R' to restart game";
        public enum CurrentDevice
        {
            Phone,
            Desktop
        }
        private CurrentDevice device;
        public void LoadContent(ContentManager Content, Game1 game)
        {
            this.game = game;
            mainMenu = new MainMenu();
            mainMenu.LoadContent(Content);
            btnPlay = new cButton(Content.Load<Texture2D>("Menu/Buttons/playButton"));
            backToGameButton = new cButton(Content.Load<Texture2D>("Menu/Buttons/playButton"));
            exitButton = new cButton(Content.Load<Texture2D>("Menu/Buttons/exitButton"));
            restartButton = new cButton(Content.Load<Texture2D>("Menu/Buttons/restartButton"));
            font = Content.Load<SpriteFont>("healthsFont");
            switch (AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Mobile":
                    device = CurrentDevice.Phone;
                    break;
                default:
                    device = CurrentDevice.Desktop;
                    break;
            }
        }
        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }
        public void UpdateMainMenu(GameTime gameTime)
        {
            switch (device)
            {
                #region UpdateMainMenu Desktop
                case CurrentDevice.Desktop:
                    game.IsMouseVisible = true;
                    mainMenu.Update(gameTime);
                    MouseState mouse = Mouse.GetState();
                    var touchPanelState = TouchPanel.GetState();
                    if (touchPanelState.Count >= 1)
                    {
                        Game1.CurrentGameState = Game1.GameState.Playing;
                    }
                    if (btnPlay.isClicked == true) Game1.CurrentGameState = Game1.GameState.Playing;
                    btnPlay.Update(mouse);
                    break;
                #endregion
                #region UpdateMainMenu Phone
                case CurrentDevice.Phone:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        Game1.CurrentGameState = Game1.GameState.Playing;
                    }
                    else
                    {
                        changeTransparency(gameTime);
                    }
                    break;
                    #endregion
            }


        }

        public void UpdatePause(GameTime gameTime)
        {
            switch (device)
            {
                #region UpdatePause Desktop
                case CurrentDevice.Desktop:
                    MouseState mouse = Mouse.GetState();
                    game.IsMouseVisible = true;
                    if (backToGameButton.isClicked == true)
                    {
                        pause = false;
                        Game1.CurrentGameState = Game1.GameState.Playing;
                        backToGameButton.isClicked = false;
                    }
                    else
                    {
                        backToGameButton.Update(mouse);
                    }
                    if (exitButton.isClicked == true) { game.Exit(); }
                    else { exitButton.Update(mouse); }
                    break;
                #endregion
                #region UpdatePause Phone
                case CurrentDevice.Phone:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        pause = false;
                        Game1.CurrentGameState = Game1.GameState.Playing;
                    }
                    else
                    {
                        changeTransparency(gameTime);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    {
                        game.Exit();
                    }
                    break;
                    #endregion
            }
        }
        public void UpdateDeadMenu(GameTime gameTime)
        {
            switch (device)
            {
                #region UpdateDeadMenu Desktop
                case CurrentDevice.Desktop:
                    MouseState mouse = Mouse.GetState();
                    game.IsMouseVisible = true;
                    if (restartButton.isClicked == true)
                    {
                        restartButton.isClicked = false;
                        game.restart();
                    }
                    else
                    {
                        restartButton.Update(mouse);
                    }
                    if (exitButton.isClicked == true)
                    {
                        game.Exit();
                    }
                    else { exitButton.Update(mouse); }
                    break;
                #endregion
                #region UpdateDeadMenu Phone
                case CurrentDevice.Phone:
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        game.restart();
                    }
                    else
                    {
                        changeTransparency(gameTime);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Q))
                    {
                        game.Exit();
                    }
                    break;
                    #endregion
            }
        }
        private void changeTransparency(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > delay)
            {
                elapsed = 0;
                transparency += factorTransparency;

                if (transparency <= 0.2)
                {
                    factorTransparency *= -1;
                }
                else if (transparency >= 1)
                {
                    factorTransparency *= -1;
                }
            }
        }
        public void UpdatePlaying()
        {
            // Mouse.SetPosition(0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.P) && pause == false)
            {
                pause = true;
                Game1.CurrentGameState = Game1.GameState.Pause;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void DrawMainMenu(SpriteBatch spriteBatch)
        {
            Vector2 position = getXYtoDrawMenu();
            mainMenu.Draw(spriteBatch, position);
            switch (device)
            {
                #region DrawMainMenu Desktop
                case CurrentDevice.Desktop:
                    btnPlay.setPosition(new Vector2(330 + (int)position.Y, 255 + (int)position.X));
                    btnPlay.Draw(spriteBatch);
                    break;
                #endregion
                #region DrawMainMenu Phone
                case CurrentDevice.Phone:
                    spriteBatch.DrawString(font, startGame, new Vector2(270 + (int)position.Y, 240 + (int)position.X), Color.Black * transparency);
                    break;
                    #endregion
            }


        }

        public void DrawPause(SpriteBatch spriteBatch)
        {
            Vector2 position = getXYtoDrawMenu();
            switch (device)
            {
                #region DrawPause Desktop
                case CurrentDevice.Desktop:
                    backToGameButton.setPosition(new Vector2(330 + (int)position.Y, 300 + (int)position.X));
                    backToGameButton.Draw(spriteBatch);

                    exitButton.setPosition(new Vector2(330 + (int)position.Y, 350 + (int)position.X));
                    exitButton.Draw(spriteBatch);
                    break;
                #endregion
                #region DrawPause Phone
                case CurrentDevice.Phone:
                    spriteBatch.DrawString(font, backToGame, new Vector2(300 + (int)position.Y, 240 + (int)position.X), Color.Black * transparency);
                    spriteBatch.DrawString(font, quitGame, new Vector2(300 + (int)position.Y, 270 + (int)position.X), Color.Black * transparency);
                    break;
                    #endregion
            }

        }
        public void DrawDeadMenu(SpriteBatch spriteBatch)
        {
            Vector2 position = getXYtoDrawMenu();
            switch (device)
            {
                #region DrawDeadMenu Desktop
                case CurrentDevice.Desktop:
                    restartButton.setPosition(new Vector2(330 + (int)position.Y, 300 + (int)position.X));
                    restartButton.Draw(spriteBatch);
                    exitButton.setPosition(new Vector2(330 + (int)position.Y, 350 + (int)position.X));
                    exitButton.Draw(spriteBatch);
                    break;
                #endregion
                #region DrawDeadMenu Phone
                case CurrentDevice.Phone:
                    spriteBatch.DrawString(font, restartGame, new Vector2(300 + (int)position.Y, 300 + (int)position.X), Color.Black * transparency);
                    spriteBatch.DrawString(font, quitGame, new Vector2(300 + (int)position.Y, 350 + (int)position.X), Color.Black * transparency);
                    break;
                    #endregion
            }

            spriteBatch.DrawString(font, "You gained: ", new Vector2(340 + (int)position.Y, 250 + (int)position.X), Color.Black);
            spriteBatch.DrawString(font, MarcoPlayer.score.ToString() + " points", new Vector2(350 + (int)position.Y, 280 + (int)position.X), Color.Black);

        }
        private Vector2 getXYtoDrawMenu()
        {

            int a = graphics.Viewport.Height;
            int screenHeight = graphics.Viewport.Height;
            int screenWidth = graphics.Viewport.Width;
            Vector2 vector = new Vector2();
            if (screenHeight > 600)
            {
                vector.X = (screenHeight - 600) / 2;
            }
            else vector.X = 0;
            if (screenWidth > 800)
            {
                vector.Y = (screenWidth - 800) / 2;
            }
            else
                vector.Y = 0;
            return vector;
        }
    }
}
