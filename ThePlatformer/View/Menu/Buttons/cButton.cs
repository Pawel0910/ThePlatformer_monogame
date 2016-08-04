using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer
{
    class cButton
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        public Vector2 size;

        Color colour = new Color(255, 255, 255, 255);

        
        public cButton(Texture2D newTexture)
        {
            texture = newTexture;
        }
        bool down;
        public bool isClicked;
        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y,
                texture.Width,texture.Height);
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A < 255) down = false;
                if (colour.A == 0) down = true;
                if (down) colour.A += 3;
                    else colour.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }else if (colour.A < 255)
            {
                colour.A += 3;
                isClicked = false;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }
        public void Draw(SpriteBatch spriteBach)
        {
            spriteBach.Draw(texture, rectangle, colour);
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

        }
    }
}
