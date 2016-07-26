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
        public Rectangle rectangle;
        private bool negativeSpeed = false;
        private float RotationAngle;
        public float bulletSpeed=1f;
        public Bullet() { }
        public Bullet(Vector2 startPos,bool isLeft) {
            this.startPos = startPos;
            this.position = startPos;
            this.negativeSpeed = isLeft;
            
        }
        public void Load(ContentManager Content)
        {
            textureBullet = Content.Load<Texture2D>("bulletBig");
        }
        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 1, 1);
            if (!negativeSpeed)
                position.X += bulletSpeed;
            else
                position.X -= bulletSpeed;
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
