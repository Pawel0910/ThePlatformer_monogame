using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using ThePlatformer.SpriteBase.Animation;

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

        public ShootingEnemy(Vector2 position)
            : base(position)
        {
        }

        public override void Load(Texture2D texture,IAnimation animation)
        {
            base.Load(texture,animation);
        }
        public override void Update(GameTime gameTime)
        {

            playerPosX = MarcoPlayer.rectangleStatic.X;
            base.Update(gameTime);
            rotateEnemy();
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

            if (_rectangle.X - distanceToSeePlayer < playerPosX && playerPosX < _rectangle.X ||
                playerPosX > _rectangle.X && _rectangle.X + distanceToSeePlayer > playerPosX)
            {
                if (startTime > delayBetweenBulletShots && bulletList.Count < maxBulletCount)
                {
                    isShoot = true;
                    startTime = 0;
                }
            }
        }
        public void myShoot()
        {
            Vector2 ballPosition = new Vector2(_position.X + 12, _position.Y);
            Bullet bullet = new Bullet(ballPosition, isLeft,3.5f);
            bulletList.Add(bullet);
            isShoot = false;

        }
        private new void rotateEnemy()
        {
            if (_rectangle.X < playerPosX)
            {
                flip = SpriteEffects.None;
                rotation = MathHelper.TwoPi;
                isLeft = false;
            }
            else
            {
                flip = SpriteEffects.FlipVertically;
                rotation = MathHelper.Pi;
                isLeft = true;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            switch (liveStatus)
            {
                case LiveStatus.alive:
                    // spriteBatch.Draw(texture, _position, null, Color.White, 0f, Vector2.Zero, 1, flip, 0);
                    spriteBatch.Draw(_texture, _position, null, null, _origin, rotation, scaleVector, Color.White, flip);

                    healthBar.Draw(spriteBatch, scale);
                    break;
                case LiveStatus.dead:
                    break;
            }
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }

        }
    }
}
