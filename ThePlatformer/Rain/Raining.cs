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
            velocity.Y = 0.2f;
            //velocity.Y = (float)elapsedTime / 24;
            //velocity = new Vector2((float)gameTime.ElapsedGameTime.TotalMilliseconds / 4, velocity.Y + 0.4f);

            position += velocity;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 35f, Vector2.Zero, 0.05f, SpriteEffects.None, 0);
        }
    }
}
