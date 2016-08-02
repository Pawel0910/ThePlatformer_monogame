using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.View.Menu
{
    public class MenuViewManager
    {
        private MainMenu mainMenu;
        private GraphicsDevice graphics;
        public void LoadContent(ContentManager Content)
        {
            mainMenu = new MainMenu();
            mainMenu.LoadContent(Content);
        }
        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            this.graphics = graphics;
            UpdateBegin(gameTime);
        }
        public void UpdateBegin(GameTime gameTime)
        {
            mainMenu.Update(gameTime);
        }
        
        public void UpdatePause(GameTime gameTime)
        {

        }
        public void UpdateRestart(GameTime gameTime)
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {

        }
        public void DrawBegin(SpriteBatch spriteBatch)
        {
            mainMenu.Draw(spriteBatch,getXYtoDrawMenu());
           // spriteBatch.Draw(Content.Load<Texture2D>("mainMenu"), new Rectangle((int)vector.Y, (int)vector.X, 800, 600), Color.White);
        }

        public void DrawPause(SpriteBatch spriteBatch)
        {

        }
        public void DrawRestart(SpriteBatch spriteBatch)
        {

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
