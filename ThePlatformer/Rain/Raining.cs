using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Rain
{
    public class Raining
    {
        private Texture2D texture;
        private Vector2 position;


        public void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("drop_rain");
        }

        public void Update(GameTime gameTime)
        {
            position = new Vector2(20, 30);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);

            //spriteBatch.Draw(texture, new Rectangle((int)position.Y, (int)position.X, 800, 600), Color.White);
        }
    }
}
