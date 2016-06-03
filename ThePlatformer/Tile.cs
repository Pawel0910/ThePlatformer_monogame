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
    class Tile
    {
        protected Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get { return rectangle; }
            protected set { rectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spritebBatch)
        {
            spritebBatch.Draw(texture, rectangle, Color.White);
        }
    }

    class CollisionTile : Tile
    {
        public CollisionTile(int i, Rectangle newRectangle)
        {
            texture = Content.Load<Texture2D>("tile" + i);
            this.Rectangle = newRectangle;
        }
    }
}
