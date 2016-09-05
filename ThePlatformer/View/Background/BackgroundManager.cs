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
            scrolling2.LoadContent(Content, "Background/ScrollingBackground11", new Rectangle(screenWidth, -screenHeight / 2, screenWidth + 200, screenHeight));
            // song = Content.Load<Song>("Sounds/Background/Chainsaw");

            // MediaPlayer.Play(song);
            // MediaPlayer.Volume = 0.2f;
            // MediaPlayer.IsRepeating = true;
        }
        public void Update(GameTime gameTime, Vector2 position)
        {
            scrolling2.Update(player._position, screenWidth + 100, screenHeight);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            scrolling2.Draw(spriteBatch);
        }
    }
}
