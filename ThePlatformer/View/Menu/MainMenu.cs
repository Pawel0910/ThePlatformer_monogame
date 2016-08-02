using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerLoader;

namespace ThePlatformer.View.Menu
{
    class MainMenu
    {
        public Texture2D texture { get; set; }
        private int currentFrame;
        private int totalFrames;
        //slow down frame animation
        private int timeSinceLastFrame = 0;

        private int millisecondsPerFrame = 100;
        public MainMenu()
        {
            currentFrame = 0;
            totalFrames = PlayerAnimationLists.drawMenuStart().Count-1;
        }
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("mainMenu");
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
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.Y, (int)position.X, 800, 600), Color.White);
        }
        public void Draw(SpriteRender spriteRender, SpriteSheet spriteSheet)
        {
            List<String> lista1 = PlayerAnimationLists.getTestList();
            spriteRender.Draw(
                spriteSheet.Sprite(lista1[currentFrame]),
                    new Vector2(200, 200));
        }
    }
}
