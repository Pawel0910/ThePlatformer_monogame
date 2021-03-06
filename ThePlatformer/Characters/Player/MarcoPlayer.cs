﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Enemies;
using ThePlatformer.Health;
using ThePlatformer.SpriteBase.Animation;

namespace ThePlatformer
{
    public class MarcoPlayer : CustomSprite
    {
        private TouchCollection touchCollection = TouchPanel.GetState();
        // public Texture2D texture{ get; set; }
        //private static Vector2 position;
        //public Vector2 position;
        public Vector2 velocity;
        private SpriteEffects flip;
        private bool isLeft = false, isRight = true;
        public int bulletDistance = 300;
        public static Rectangle rectangleStatic;
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public bool hasJumped = false, dead = false;
        public List<Bullet> bulletList = new List<Bullet>();
        public float startTime = 0, delayBetweenBulletShots = 450;
        private int screenWidth, screenHeight;
        private SpriteFont font;
        public static int score;
        public int currentLifeNumber { get; set; }
        public int lives = 3;
        public int livePoints;// pkt życia w jednym życiu :P
                              //do testu:::
        private readonly Color _rectangleColor = Color.Black;
        private Texture2D _rectangleTexture;
        public static HealthBar healthBar;
        private IAnimation animation;
        private bool isShoot;
        public void restartMarco()
        {
            _position = new Vector2(16, 38);
            lives = 3;
            score = 0;
            bulletDistance = 200;
            healthBar.restartHealthBar();
        }
        enum Checkpoint
        {
            Checkpoint1,
            Checkpoint2,
            Checkpoint3,
            Checkpoint4
        }
        Checkpoint currentCheckpoint = Checkpoint.Checkpoint1;
        public Vector2 Position
        {
            get { return _position; }
        }
        public MarcoPlayer(Vector2 position, float scale = 1f)
        : base(position, scale)
        {
            currentLifeNumber = lives;
            animation = new AnimationImpl(10, this, "Player/Run/Run", "Player/Shoot/Shoot", "Player/Idle/Idle", "Player/Jump/Jump");

        }

        public void Load(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            Color color = Color.Black;
            animation.LoadConent(Content);
            animation.setEventOnAnimation("Player/Shoot/Shoot", "shoot", 2);
            animation.setDelayBeetwenAnim("Player/Jump/Jump", 100);
            animation.setDelayBeetwenAnim("Player/Shoot/Shoot", 50);
            //animation.setCurrentAnimation("Player/Run/Run");
            base.LoadContent(Content, "idle1");
            Bullet bullet1 = new Bullet();
            bullet1.Load(Content);
            healthBar = new HealthBar(Content);
            livePoints = healthBar.fullHealth;
            font = Content.Load<SpriteFont>("healthsFont");
            var colors = new Color[_texture.Width * _texture.Height];

            colors[0] = color;
            colors[1] = color;
            colors[_texture.Width - 1] = color;
            colors[_texture.Width - 2] = color;
            colors[(_texture.Width * _texture.Height) - _texture.Width] = color;
            colors[(_texture.Width * _texture.Height) - _texture.Width + 1] = color;


            colors[(_texture.Width * _texture.Height) - 1] = color;
            colors[(_texture.Width * _texture.Height) - 2] = color;


            _rectangleTexture = new Texture2D(graphicsDevice, _texture.Width, _texture.Height);
            _rectangleTexture.SetData(colors);
        }
        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            updateScreenInfo(graphics);
            isCrossedMap();
            updatePosition();
            Texture2D text = animation.changeTextureOnAnimation(gameTime);
            base.Update(gameTime, text);
            Input(gameTime);
            gravity();
            checkpointManager();
            destroyBullet();

            checkCurrentLifeStatus();
            animator();
        }

        private void animator()
        {
            if ((velocity.X == 0 && velocity.Y > 0 && velocity.Y < 0.5f) && !isShoot)//tyle warunków ponieważ
                                                                                     //przez grawitacje velocity.Y niestety nie było zerowe zawsze
            {
                animation.setCurrentAnimation("Player/Idle/Idle");
            }
            if (velocity.X != 0 && !isShoot && !hasJumped)
            {
                animation.setCurrentAnimation("Player/Run/Run");
            }
            if ((velocity.Y < 0 || velocity.Y > 1) && !isShoot)
            {
                animation.setCurrentAnimation("Player/Jump/Jump");
            }
            if (isShoot)
            {
                animation.setCurrentAnimation("Player/Shoot/Shoot");
            }
        }
        private void checkCurrentLifeStatus()
        {
            if (livePoints <= 0 && lives > 0)
            {
                lives--;
                livePoints = healthBar.fullHealth;
                healthBar.restartHealthBar();
            }
            else if (lives == 0)
            {
                Game1.CurrentGameState = Game1.GameState.DeadMenu;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hurtAmount">Ilość procentowa do odjęcia z healthBara</param>
        public void playerGotHurt(int hurtAmount)
        {
            healthBar.updateHealthStatus(hurtAmount);
            livePoints = healthBar.currentHealth;
        }
        private Vector2 setHealthBarPosition()
        {
            return new Vector2(-screenWidth / 2 + _position.X + 10, -screenHeight / 2 + _position.Y + 20);  // dzielnik 2 bo camera ma zooma : )
        }
        private void updatePosition()
        {
            _position += velocity;
            rectangleStatic = Rectangle;
            //rectangleStatic = new Rectangle((int)position.X, (int)position.Y, texture.Width/2, texture.Height/2);
        }
        private void checkpointManager()
        {
            switch (currentCheckpoint)
            {
                case Checkpoint.Checkpoint1:
                    break;
                case Checkpoint.Checkpoint2:
                    break;
            }
        }
        private void gravity()
        {
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;//grawitacja
            }
        }
        private void destroyBullet()
        {
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
        private void updateScreenInfo(GraphicsDevice graphics)
        {
            screenHeight = graphics.Viewport.Height;
            screenWidth = graphics.Viewport.Width;
        }
        private void Input(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (isLeft)
                {
                    flip = SpriteEffects.None;
                    rotation = MathHelper.TwoPi;
                }

                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 4;
                isLeft = false;
                isRight = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (isRight)
                {
                    flip = SpriteEffects.FlipVertically;
                    rotation = MathHelper.Pi;

                }
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 4;
                isRight = false;
                isLeft = true;
            }
            else velocity.X = 0f;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                _position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
            //strzelanie
            startTime += gameTime.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                if (startTime > delayBetweenBulletShots)
                {
                    isShoot = true;
                    startTime = 0;
                }

            }
        }
        public void shoot()
        {
            Vector2 ballPosition = new Vector2(_position.X + 8, _position.Y);
            Bullet bullet = new Bullet(ballPosition, isLeft, 5f);
            bulletList.Add(bullet);
            isShoot = false;
        }
        public void allCollisionsWithEnemies(EnemyBase enemy)
        {
            if (bulletCollisionWithNormalEnemy(enemy))
            {
                score += 20;
                int hurtAmount = enemy.healthBar.fullHealth / 2;
                enemy.enemyGotHurt(hurtAmount);
            }
        }
        private bool bulletCollisionWithNormalEnemy(EnemyBase enemy)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                if (bulletList[i].rectangle.Intersects(enemy._rectangle))
                {
                    bulletList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void giveMeHP()
        {
            livePoints = healthBar.fullHealth;
            healthBar.restartHealthBar();
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (_rectangle.TouchTopOf(newRectangle))
            {
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (_rectangle.TouchLeftOf(newRectangle))
            {
                _position.X = newRectangle.X - _rectangle.Width / 2 - 2;
            }
            if (_rectangle.TouchRightOf(newRectangle))
            {
                _position.X = newRectangle.X + newRectangle.Width + newRectangle.Width / 4 - 2;
            }
            if (_rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (_position.X < 5) _position.X = 5;
            if (_position.X > xOffset - _rectangle.Width) _position.X = xOffset - _rectangle.Width;
        }
        public void isCrossedMap()
        {
            if (_position.Y > mapHeight - _rectangle.Height)
            {
                restartPosition();
                lives--;
                livePoints = healthBar.fullHealth;
                healthBar.restartHealthBar();
            }

        }
        private void restartPosition()
        {
            _position.Y = 20;
            _position.X = 38;
        }
        public void knockBack(Vector2 enemyPosition)
        {
            if (enemyPosition.X > _position.X)
            {
                velocity += new Vector2(-20, 0);
            }
            else
            {
                velocity += new Vector2(20, 0);
            }
        }
        private Vector2 setLeftCornerFontPosition(int shiftX, int shiftY)
        {
            return new Vector2(-screenWidth / 2 + _position.X + shiftX, -screenHeight / 2 + _position.Y + shiftY);
        }

        public new void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_rectangleTexture, null, Rectangle, null, null, 0, null, Color.White);

            spriteBatch.Draw(_texture, _position, null, null, _origin, rotation, scaleVector, Color.White, flip);

            healthBar.Draw(spriteBatch, setHealthBarPosition());
            spriteBatch.DrawString(font, "Lifes: " + lives, setLeftCornerFontPosition(30, 60), Color.Black);
            spriteBatch.DrawString(font, "Score: " + score, setLeftCornerFontPosition(30, 90), Color.Black);

            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
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
