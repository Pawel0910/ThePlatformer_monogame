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
    public class Background
    {
        public Texture2D texture;
        public Rectangle rect;
        public void Initialize() { }
        public void LoadContent(ContentManager Content, String assetName, Rectangle rect)
        {
            texture = Content.Load<Texture2D>(assetName);
            this.rect = rect;
        }
        public void Update(GameTime gameTime) { }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, Color.White);
        }
    }
}
