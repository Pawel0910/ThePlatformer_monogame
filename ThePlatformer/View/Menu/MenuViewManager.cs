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

namespace ThePlatformer.View.Menu
{
    public class MenuViewManager
    {
        private Game1 game;
        private MainMenu mainMenu;
        private GraphicsDevice graphics;
        private cButton btnPlay;
        private cButton backToGameButton, exitButton;
        private bool pause = false;
        public void LoadContent(ContentManager Content, Game1 game)
        {
            this.game = game;
            mainMenu = new MainMenu();
            mainMenu.LoadContent(Content);
            btnPlay = new cButton(Content.Load<Texture2D>("button"));
            backToGameButton = new cButton(Content.Load<Texture2D>("button"));
            exitButton = new cButton(Content.Load<Texture2D>("button"));
        }
        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            this.graphics = graphics;
        }
        public void UpdateMainMenu(GameTime gameTime)
        {
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
        }

        public void UpdatePause(GameTime gameTime)
        {
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
        }
        public void UpdateDeadMenu(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            game.IsMouseVisible = true;
            if (backToGameButton.isClicked == true)
            {
                backToGameButton.isClicked = false;
                game.restart();
            }
            else
            {
                backToGameButton.Update(mouse);
            }
            if (exitButton.isClicked == true)
            {
                game.Exit();
            }
            else { exitButton.Update(mouse); }

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
        public void DrawBegin(SpriteBatch spriteBatch)
        {
            Vector2 position = getXYtoDrawMenu();

            mainMenu.Draw(spriteBatch, position);
            btnPlay.setPosition(new Vector2(330 + (int)position.Y, 300 + (int)position.X));
            btnPlay.Draw(spriteBatch);
        }

        public void DrawPause(SpriteBatch spriteBatch)
        {
            Vector2 position = getXYtoDrawMenu();

            backToGameButton.setPosition(new Vector2(330 + (int)position.Y, 300 + (int)position.X));
            backToGameButton.Draw(spriteBatch);

            exitButton.setPosition(new Vector2(330 + (int)position.Y, 350 + (int)position.X));
            exitButton.Draw(spriteBatch);
        }
        public void DrawDeadMenu(SpriteBatch spriteBatch)
        {
            Vector2 position = getXYtoDrawMenu();
            backToGameButton.setPosition(new Vector2(330 + (int)position.Y, 300 + (int)position.X));

            backToGameButton.Draw(spriteBatch);
            exitButton.setPosition(new Vector2(330 + (int)position.Y, 350 + (int)position.X));

            exitButton.Draw(spriteBatch);
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
