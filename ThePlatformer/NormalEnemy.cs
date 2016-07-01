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
        public bool hasJumped = false, canTeleport = false;
        public static Rectangle rectangle;
        private float startTime = 0, endTime = 0;
        
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
            if (position.Y > MarcoPlayer.mapHeight - rectangle.Height)
            {
                //position += new Vector2(100, 0);
                position.X = MarcoPlayer.rectangle.X+300;
                position.Y = -20;
            }
            //    startTime += gameTime.ElapsedGameTime.Milliseconds;
            //    if (startTime > 3500)
            //    {
            //        if (canTeleport)
            //        {
            //            position += new Vector2(100, 0);
            //            position.Y = 0;
            //        //canTeleport = false - bedzie woczas ustawiany w updatcie kolizji
            //    }
            //    startTime = 0;
            //}
            //running away before player
            if (rectangle.X-MarcoPlayer.rectangle.X< distanceToPlayer)
            {
                position += new Vector2(4,0);
                //position += new Vector2(MarcoPlayer.rectangle.X + 10, 0);
            }
            //zmiana pozycji
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            //grawitacaj
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }
        
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                canTeleport = false;
                hasJumped = false;
            }
            else if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
                if (hasJumped == false)
                {
                   // Rectangle playerRectangle = MarcoPlayer.rectangle;

                    position.Y -= 5f;
                    velocity.Y = -12f;
                    hasJumped = true;
                }
            }
            else if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (!rectangle.TouchTopOf(newRectangle))
            {
                canTeleport = true;
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
