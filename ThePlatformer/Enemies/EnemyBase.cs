using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Enemies
{
    public abstract class EnemyBase
    {
        public Rectangle rectangle;
        public Texture2D texture;
        public Vector2 velocity, position = new Vector2(10, 10);
        public bool hasJumped = false, canTeleport = false;
        public void Load(ContentManager Content,String path)
        {
            texture = Content.Load<Texture2D>(path);
        }
        public void Load(ContentManager Content,String path, Vector2 startPosition)
        {
            Load(Content, path);
            this.position = startPosition;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        virtual public void  Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            //grawitacja
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
        }
        virtual public void restart()
        {
            position = new Vector2(10, 10);
            this.velocity = new Vector2();
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            hasJumped = false;
            canTeleport = false;

        }
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
        }
    }
}
