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
    public class BaseTreasureAbstract
    {
        public Rectangle rectangle;
        public Texture2D texture;
        public Vector2 velocity, position = new Vector2(10, 10);//velocity jest, by działała na niego grawitacja
        public void Load(ContentManager Content, String path)
        {
            texture = Content.Load<Texture2D>(path);
        }
        virtual public void Update(GameTime gameTime)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            gravity();
        }
        public void CollisionMap(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }
            else if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
              
            }
            else if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
        }
        private void gravity()
        {
            //grawitacja
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);        
        }
    }
}
