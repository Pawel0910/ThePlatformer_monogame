﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Characters.Enemies;
using ThePlatformer.Health;
using ThePlatformer.SpriteBase.Animation;

namespace ThePlatformer.Enemies
{
    public abstract class EnemyBase : CustomSprite
    {
        public float scale = 0.2f;
        // public Rectangle _rectangle;
        // public Texture2D texture;
        public Vector2 velocity;//, _position = new Vector2(10, 10);
        public bool hasJumped = false, canTeleport = false;
        public List<Bullet> bulletList = new List<Bullet>();
        public int bulletStrengthHit;
        public int livePoints;
        public HealthBar healthBar;
        public bool isDead = false;
        public MapManager mapManager = MapManager.getInstance();
        private static ContentManager Content;
        private bool parachute = true;
        public IAnimation animation;
        public bool isShoot;
        public enum LiveStatus
        {
            alive,
            dead
        }
        public LiveStatus liveStatus = LiveStatus.alive;

        public EnemyBase(Vector2 position)
            : base(position)
        {
            // animation = new AnimationImpl(200, this, "marco", "arrow");
            this.animation = new AnimationImpl(200, this);
        }
        public static void setContent(ContentManager Content)
        {
            EnemyBase.Content = Content;

        }
        virtual public void Load(Texture2D texture, IAnimation animation = null)
        {
            if (this.animation != null)
            {
                this.animation.setFromDifferentAnimation(animation);
            }
            //animation.LoadConent(Content);
            bulletStrengthHit = (int)((double)MarcoPlayer.healthBar.fullHealth / 5);
            healthBar = new HealthBar(Content, _position);

            livePoints = healthBar.fullHealth;
            animation.setCurrentAnimation("Soldier/Parachute/soldierParachute");
            this.animation.setEventOnAnimation("Soldier/Shooting/Shoot", "myShoot", 3);

            base.LoadContent(texture);
        }
        virtual public void Update(GameTime gameTime)
        {
            switch (liveStatus)
            {
                case LiveStatus.alive:
                    animator();
                    myPosition();
                    gravity();
                    Texture2D anim = animation.changeTextureOnAnimation(gameTime);
                    base.Update(gameTime, anim);
                    int healthBarShift = (int)((double)healthBar.fullHealth / 2 * scale);
                    healthBar.Update(new Vector2(_rectangle.X + (_texture.Width / 2) - healthBarShift, _rectangle.Y - 15));
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
        private void animator()
        {
            if ((velocity.X == 0 && !parachute && !hasJumped) && !isShoot)
            {
                animation.setCurrentAnimation("Soldier/Idle/Idle");
            }
            if (!hasJumped && !parachute && velocity.X != 0 && !isShoot)
            {
                animation.setCurrentAnimation("Soldier/Running/Run");
            }
            if (parachute && !isShoot)
            {
                animation.setCurrentAnimation("Soldier/Parachute/soldierParachute");
            }
            if (hasJumped && !parachute && (velocity.Y < 0 || velocity.Y > 1) && !isShoot)
            {
                animation.setCurrentAnimation("Soldier/Jump/Jump");
            }
            if (isShoot && !parachute)
            {
                animation.setCurrentAnimation("Soldier/Shooting/Shoot");
            }
        }
        private void myPosition()
        {
            _position += velocity;
            _rectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
        }
        private void gravity()
        {

            //grawitacja
            if (velocity.Y < 10)
            {
                if ((GetType() == typeof(ShootingEnemy)))//warunek by pozniej runningEnemy mogl podskakiwac.
                {
                    velocity.Y += 0.01f;
                    if (parachute)
                    {
                        velocity.X += 0.005f;
                    }
                    else
                    {
                        velocity.X = 0;
                    }
                }
                else
                {
                    velocity.Y += 0.5f;
                }
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
                parachute = false;
            }
            else if (_rectangle.TouchLeftOf(newRectangle))
            {
                _position.X = newRectangle.X - _rectangle.Width - 2;
                if (hasJumped == false && !parachute)
                {
                    _position.Y -= 5f;
                    velocity.Y = -12f;
                    hasJumped = true;
                }
            }
            else if (_rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
                parachute = false;

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
                if ((GetType() == typeof(ShootingEnemy)))
                {
                    player.playerGotHurt(bulletStrengthHit);
                }
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
        public bool isCollisionWithSprite(CustomSprite sprite)
        {
            if (sprite.Collision(this))
            {
                return true;
            }
            return false;
        }
    }
}