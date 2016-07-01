using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer
{
    class NormalEnemy
    {
        private Texture2D texture;
        private Vector2 position = new Vector2(50, 10);
        private Vector2 velocity;
        private Vector2 origin;
        private SpriteEffects flip;
        private bool isLeft = false, isRight = true;
        private int distanceToPlayer = 100;
        public bool hasJumped = false;
        public static Rectangle rectangle;
        
        public Vector2 Position
        {
            get { return position; }
        }
        public NormalEnemy() { }
        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("idle1");
        }

        public void Update(GameTime gameTime)
        {
            runAwayBeforePlayer();
            if (rectangle.X-MarcoPlayer.rectangle.X< distanceToPlayer)
            {
                position += new Vector2(4,0);
            }
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);

            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }
        public void runAwayBeforePlayer()
        {
            Rectangle playerRectangle = MarcoPlayer.rectangle;
            if (playerRectangle.TouchLeftOf(rectangle))
            {
                position += new Vector2(4, 0);
            }
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            else if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
                if (hasJumped == false)
                {
                    position.Y -= 5f;
                    velocity.Y = -12f;
                    hasJumped = true;
                }
            }
            //else if (rectangle.TouchRightOf(newRectangle))
            //{
            //    position.X = newRectangle.X + newRectangle.Width + 2;
            //}
            else if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
               
            }

            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
           // if (position.Y < 0) velocity.Y = 1f;
           // if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, flip, 0);
        }
    }
}
