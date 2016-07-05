using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ThePlatformer.Enemies
{
    class ShootingEnemy : EnemyBase
    {

        private SpriteEffects flip;
        private float playerPosX, distanceToSeePlayer = 100;
        private bool isLeft = false;
        private float bulletDistance = 500;
        public float startTime = 0, delayBetweenBulletShots = 1000;
        public int maxBulletCount = 3;

        public override void Update(GameTime gameTime)
        {
            playerPosX = MarcoPlayer.rectangle.X;
            base.Update(gameTime);
            rotation();
            shooting(gameTime);
            for (int i = 0; i < bulletList.Count; i++)
            {
                bulletList[i].Update();
                if (bulletList[i].position.X - bulletList[i].startPos.X > bulletDistance ||
                    bulletList[i].startPos.X - bulletList[i].position.X > bulletDistance)
                {
                    bulletList.RemoveAt(i);
                }
            }
        }
        private void shooting(GameTime gameTime)
        {
            if (playerPosX < 0)
            {
                playerPosX = playerPosX * -1;
            }
            startTime += gameTime.ElapsedGameTime.Milliseconds;

            if (rectangle.X-distanceToSeePlayer<playerPosX&&playerPosX<rectangle.X||
                playerPosX>rectangle.X&&rectangle.X+distanceToSeePlayer>playerPosX)
            {
                if (startTime > delayBetweenBulletShots && bulletList.Count< maxBulletCount)
                {
                    Bullet bullet = new Bullet(position, isLeft);
                    bulletList.Add(bullet);
                    startTime = 0;
                }
            }
        }
        
        private void rotation()
        {
            if(rectangle.X<playerPosX)
            {
                flip = SpriteEffects.None;
                isLeft = false;
            }
            else
            {
                flip = SpriteEffects.FlipHorizontally;
                isLeft = true;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, flip, 0);
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
        //public override void Update(GameTime gameTime)
        //{
        //    //rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        //    //grawitacja
        //    if (velocity.Y < 10)
        //    {
        //        velocity.Y += 0.4f;
        //    }
        //}
    }
}
