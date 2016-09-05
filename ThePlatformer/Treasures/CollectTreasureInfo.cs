using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Treasures
{
    public class CollectTreasureInfo
    {
        private static SpriteFont font;
        private long elapsed, delay = 200;
        private float transparency = 1f;
        private string title = "";
        private Vector2 position;
        private Vector2 velocity;
        private float maxRightMove, maxLeftMove;
        private bool goRight;
        public bool deleteMyself;
        public CollectTreasureInfo(String title, Vector2 position)
        {
            this.position = position;
            maxRightMove = position.X + 10;
            maxLeftMove = position.X - 10;
            this.title = title;
        }

        public static void LoadContent(ContentManager Content)
        {
            font = Content.Load<SpriteFont>("healthsFont");
        }
        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > delay && transparency > 0)
            {
                transparency -= 0.1f;
                elapsed = 0;
            }
            else if (transparency == 0)
            {
                deleteMyself = true;
            }
            position += velocity;
            changePosition(gameTime);
        }
        private void changePosition(GameTime gameTime)
        {
            setDirection();
            if (goRight)
            {
                velocity.X = 0.5f;
            }
            else
            {
                velocity.X = -0.5f;
            }
            velocity.Y = -1f;
        }
        private void setDirection()
        {
            if (position.X > maxRightMove)
            {
                goRight = false;
            }
            else if (position.X < maxLeftMove)
            {
                goRight = true;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, title, position, Color.Black * transparency);
        }
    }
}
