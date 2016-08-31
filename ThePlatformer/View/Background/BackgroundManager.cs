using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.View.Background
{
    class BackgroundManager
    {
        Scrolling scrolling1;
        Scrolling scrolling2;
        private MarcoPlayer player;
        int screenWidth;
        int screenHeight;

        /// Song song;
        public BackgroundManager(MarcoPlayer marcoPlayer)
        {
            player = marcoPlayer;
            scrolling1 = new Scrolling();
            scrolling2 = new Scrolling();
        }

        public void Initialize()
        {
        }
        public void LoadContent(ContentManager Content, int screenWidth, int screenHeight)
        {
            this.screenHeight = screenHeight;
            this.screenWidth = screenWidth;
            scrolling1.LoadContent(Content, "Background/ScrollingBackground12", new Rectangle((int)player._position.X - screenWidth / 2,
                ((int)player._position.Y - screenHeight / 2), screenWidth + 100, screenHeight + 100));
            scrolling2.LoadContent(Content, "Background/ScrollingBackground11", new Rectangle(screenWidth, -screenHeight / 2, screenWidth, screenHeight));
            // song = Content.Load<Song>("Sounds/Background/Chainsaw");

            // MediaPlayer.Play(song);
            // MediaPlayer.Volume = 0.2f;
            // MediaPlayer.IsRepeating = true;
        }
        public void Update(GameTime gameTime, Vector2 position)
        {
            continueBackgrounding();
            scrolling1.Update(player._position, screenWidth, screenHeight);
            scrolling2.Update(player._position, screenWidth, screenHeight);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
        }
        private void continueBackgrounding()
        {
            if (scrolling1.rect.X + scrolling1.rect.Width <= 0)
            {
                scrolling1.rect.X = scrolling2.rect.X + scrolling2.rect.Width;
            }
            if (scrolling2.rect.X + scrolling2.rect.Width <= 0)
            {
                scrolling2.rect.X = scrolling1.rect.X + scrolling1.rect.Width;
            }
        }
    }
}
