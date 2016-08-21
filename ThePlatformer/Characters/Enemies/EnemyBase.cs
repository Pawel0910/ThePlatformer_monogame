using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Health;

namespace ThePlatformer.Enemies
{
    public abstract class EnemyBase
    {
        public float scale = 0.2f;
        public Rectangle rectangle;
        public Texture2D texture;
        public Vector2 velocity, position = new Vector2(10, 10);
        public bool hasJumped = false, canTeleport = false;
        public List<Bullet> bulletList = new List<Bullet>();
        public int bulletStrengthHit;
        public int livePoints;
        public HealthBar healthBar;
        public bool isDead = false;
        public MapManager mapManager = MapManager.getInstance();
        public enum LiveStatus
        {
            alive,
            dead
        }
        public LiveStatus liveStatus = LiveStatus.alive;
        public void Load(ContentManager Content,String path)
        {
            texture = Content.Load<Texture2D>(path);
        }
        public void Load(ContentManager Content,String path, Vector2 startPosition)
        {
                    Load(Content, path);
                    this.position = startPosition;
                    rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    bulletStrengthHit = (int)((double)MarcoPlayer.healthBar.fullHealth / 5);
                    healthBar = new HealthBar(Content);
                    livePoints = healthBar.fullHealth;
        }
        virtual public void  Update(GameTime gameTime)
        {
            switch (liveStatus)
            {
                case LiveStatus.alive:
                    myPosition();
                    gravity();
                    int healthBarShift = (int)((double)healthBar.fullHealth / 2 * scale);
                    healthBar.Update(new Vector2(rectangle.X + (texture.Width / 2) - healthBarShift, rectangle.Y - 15));
                    checkCurrentHealthStatus();
                    break;
                case LiveStatus.dead:
                    if (!isDead)
                    {
                        isDead = true;
                    }
                    break;
            }
        }

        private void myPosition()
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        private void gravity()
        {
            //grawitacja
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }
        private void checkCurrentHealthStatus()
        {
            if (livePoints <= 0)
            {
                liveStatus = LiveStatus.dead;
            }
        }
        public void CollisionMap(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                canTeleport = false;
                hasJumped = false;
            }
            else if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
                if (hasJumped == false)
                {
                    position.Y -= 5f;
                    velocity.Y = -12f;
                    hasJumped = true;
                }
            }
            else if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (!rectangle.TouchTopOf(newRectangle))
            {
                canTeleport = true;
            }
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
        }
        virtual public void restart()
        {
            position = new Vector2(10, 10);
            this.velocity = new Vector2();
            this.rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            hasJumped = false;
            canTeleport = false;
            bulletList = new List<Bullet>();

        }
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            switch (liveStatus)
            {
                case LiveStatus.alive:
                    spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                    healthBar.Draw(spriteBatch, scale);
                    break;
                case LiveStatus.dead:
                    break;
            }
        }
        public void allCollisionWithPlayer(MarcoPlayer player)
        {
            if (bulletCollisionWithPlayer())
            {
                if (player.lives > 0)
                    player.playerGotHurt(bulletStrengthHit);
            }
            if (MarcoPlayer.rectangle.Intersects(this.rectangle))
            {
                player.playerGotHurt(bulletStrengthHit);
                player.knockBack(position);
            }
        }
        public void enemyGotHurt(int hurtAmount)
        {
            healthBar.updateHealthStatus(hurtAmount);
            livePoints = healthBar.currentHealth;
        }
        private bool bulletCollisionWithPlayer()
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].rectangle.Intersects(MarcoPlayer.rectangle))
                {
                    bulletList.RemoveAt(i);
                    
                    return true;
                }
            }
            return false;
        }
    }
}
