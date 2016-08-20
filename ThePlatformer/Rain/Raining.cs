using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThePlatformer.Rain
{
    public class Raining
    {
        private static Texture2D texture;
        public Vector2 position{ get; set; }
        public float rotation { get; set; }
        public Vector2 velocity;
        private Rectangle rectangle;
        private float scale = 0.05f;
        public static void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("drop_rain");
        }

        public void Update(GameTime gameTime)
        {
            velocity.X = 0.2f;
            //velocity.Y = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 24;
            velocity.Y= 0.2f; 
            //velocity = new Vector2((float)gameTime.ElapsedGameTime.TotalMilliseconds / 4, velocity.Y + 0.4f);

            position += velocity;
        }
        public void Update(long elapsedTime)
        {
            velocity.X = 0.2f;
            velocity.Y = (float)elapsedTime / 24;

            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width* scale),
                (int)(texture.Height*scale));

            position += velocity;

        }
        public bool isCollisionWithPlayer()
        {
            if (this.rectangle.Intersects(MarcoPlayer.rectangle))
            {
                return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 35f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
