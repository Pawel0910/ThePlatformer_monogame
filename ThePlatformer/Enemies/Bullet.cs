using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Enemies
{
    public class Bullet
    {
        public static Texture2D textureBullet;
        private Vector2 velocity;
        public Vector2 position,startPos;
        private Vector2 origin;
        private float RotationAngle;
        public Bullet() { }
        public Bullet(Vector2 startPos) {
            this.startPos = startPos;
            this.position = startPos;
            
        }
        public void Load(ContentManager Content)
        {
            textureBullet = Content.Load<Texture2D>("bulletBig");
        }
        public void Update()
        {
            position.X += 4f;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureBullet, position, null, Color.White, MathHelper.Pi, Vector2.Zero,0.01f, SpriteEffects.None, 0);
        }
        //public void instatiate(GameTime gameTime, Vector2 startPos)
        //{

        //}
    }
}
