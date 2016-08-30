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
    public abstract class EnemyBase : CustomSprite
    {
        public float scale = 0.2f;
       // public Rectangle _rectangle;
        public Texture2D texture;
        public Vector2 velocity;//, _position = new Vector2(10, 10);
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

        public EnemyBase(Vector2 position)
            : base(position)
        {
        }

        public void Load(ContentManager Content,String path)
        {
            texture = Content.Load<Texture2D>(path);
            base.LoadContent(Content, path);
        }
        public void Load(ContentManager Content,String path, Vector2 startPosition)
        {
                    Load(Content, path);
                    this._position = startPosition;
                    //_rectangle = new Rectangle((int)_position.X, (int)_position.Y, texture.Width, texture.Height);
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
                    base.Update(gameTime);
                    int healthBarShift = (int)((double)healthBar.fullHealth / 2 * scale);
                    healthBar.Update(new Vector2(_rectangle.X + (texture.Width / 2) - healthBarShift, _rectangle.Y - 15));
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
            _position += velocity;
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, texture.Width, texture.Height);
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
            if (_rectangle.TouchTopOf(newRectangle))
            {
               // _rectangle.Y = newRectangle.Y - _rectangle.Height;
                velocity.Y = 0f;
                canTeleport = false;
                hasJumped = false;
            }
            else if (_rectangle.TouchLeftOf(newRectangle))
            {
                _position.X = newRectangle.X - _rectangle.Width - 2;
                if (hasJumped == false)
                {
                    _position.Y -= 5f;
                    velocity.Y = -12f;
                    hasJumped = true;
                }
            }
            else if (_rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (!_rectangle.TouchTopOf(newRectangle))
            {
                canTeleport = true;
            }
            if (_position.X < 0) _position.X = 0;
            if (_position.X > xOffset - _rectangle.Width) _position.X = xOffset - _rectangle.Width;
        }
        virtual public void restart()
        {
            _position = new Vector2(10, 10);
            this.velocity = new Vector2();
            //this._rectangle = new Rectangle((int)_position.X, (int)_position.Y, texture.Width, texture.Height);
            hasJumped = false;
            canTeleport = false;
            bulletList = new List<Bullet>();

        }
        virtual public void Draw(SpriteBatch spriteBatch)
        {
            switch (liveStatus)
            {
                case LiveStatus.alive:
                   // spriteBatch.Draw(texture, _position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
                    spriteBatch.Draw(_texture, _position, null, null, _origin, rotation, scaleVector, Color.White, SpriteEffects.None);

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
            if (MarcoPlayer.rectangleStatic.Intersects(this._rectangle))
            {
                player.playerGotHurt(bulletStrengthHit);
                player.knockBack(_position);
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
                if (bulletList[i].rectangle.Intersects(MarcoPlayer.rectangleStatic))
                {
                    bulletList.RemoveAt(i);
                    
                    return true;
                }
            }
            return false;
        }
    }
}
