using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ThePlatformer
{
    class Player
    {

        public Texture2D Texture { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        private int currentFrame;

        private int totalFrames;
        //slow down frame animation
        private int timeSinceLastFrame = 0;

        private int millisecondsPerFrame = 100;
        public Player(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;

                //icnrement current frame 
                currentFrame++;
                timeSinceLastFrame = 0;
                if (currentFrame == totalFrames)
                {
                    currentFrame = 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch,Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            
        }
    }
}
