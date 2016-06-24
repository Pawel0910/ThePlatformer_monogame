using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexturePackerLoader;

namespace ThePlatformer
{
    class PlayerTexturePackerTest
    {
        //SpriteSheet spriteSheet;
       // SpriteRender spriteRender;
        public Texture2D Texture { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }

        private int currentFrame;

        private int totalFrames;
        //slow down frame animation
        private int timeSinceLastFrame = 0;

        private int millisecondsPerFrame = 100;
        public PlayerTexturePackerTest(Texture2D texture, int rows, int columns)
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
        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            //this.spriteRender.Draw(
            //    this.spriteSheet.Sprite(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002),
            //        new Vector2(350, 530));

        }
        public void DrawMoja(SpriteRender spriteRender,SpriteSheet spriteSheet)
        {
            List<String> lista1 = PlayerAnimationLists.getTestList();
            List<String> list = new List<String>();
            list.Add(TexturePackerMonoGameDefinitions.mainMenu.MainMenu1);
            list.Add(TexturePackerMonoGameDefinitions.mainMenu.MainMenu2);
            //lista1.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002);
            //lista1.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0003);
            //lista1.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0004);
            //lista1.Add(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0005);
            spriteRender.Draw(
                spriteSheet.Sprite(list[0]),
                    new Vector2(100, 100));
        }
    }
}

