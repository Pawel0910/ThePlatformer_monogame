using Microsoft.Xna.Framework;
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

namespace ThePlatformer
{
    public class MarcoPlayer
    {
        private TouchCollection touchCollection = TouchPanel.GetState();
        private Texture2D texture;
        //private static Vector2 position;
        public Vector2 position;
        public Vector2 velocity;
        private Vector2 origin;
        private SpriteEffects flip;
        private bool isLeft = false, isRight = true;
        public int bulletDistance = 200;
        public static Rectangle rectangle;
        public static int mapWidth, mapHeight;
        public bool hasJumped = false, dead = false;
        public List<Bullet> bulletList = new List<Bullet>();
        public float startTime = 0, delayBetweenBulletShots = 100;
        private int screenWidth, screenHeight;
        private SpriteFont font;
        public int score;
        public int currentLifeNumber { get; set; }
        public int lives = 3;
        public int livePoints;// pkt życia w jednym życiu :P
      
        public static HealthBar healthBar;
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
            get { return position; }
        }
        public MarcoPlayer(int mapWidth1, int mapHeight1) {
            mapHeight = mapHeight1;
            mapWidth = mapWidth1;
            currentLifeNumber = lives;
        }

        public void Load(ContentManager Content)
        {
            position = new Vector2(16, 38);
            texture = Content.Load<Texture2D>("idle1");

            Bullet bullet1 = new Bullet();
            bullet1.Load(Content);
            healthBar = new HealthBar(Content);
            livePoints = healthBar.fullHealth;
            font = Content.Load<SpriteFont>("healthsFont");
        }
        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            updateScreenInfo(graphics);
            isCrossedMap();
            updatePosition();
            Input(gameTime);
            gravity();
            checkpointManager();
            destroyBullet();
            
            checkCurrentLifeStatus();

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
            return new Vector2(-screenWidth / 2 + position.X + 10, -screenHeight / 2 + position.Y + 20);  // dzielnik 2 bo camera ma zooma : )
        }
        private void updatePosition()
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
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
            //Console.Writeline("witaj");
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (isLeft)
                {
                    flip = SpriteEffects.None;
                }

                velocity.X = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 4;
                isLeft = false;
                isRight = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (isRight)
                {
                    flip = SpriteEffects.FlipHorizontally;
                    
                }
                velocity.X = -(float)gameTime.ElapsedGameTime.TotalMilliseconds / 4;
                isRight = false;
                isLeft = true;
            }
            else velocity.X = 0f;
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 5f;
                velocity.Y = -9f;
                hasJumped = true;
            }
            //strzelanie
            startTime += gameTime.ElapsedGameTime.Milliseconds;
            if (Keyboard.GetState().IsKeyDown(Keys.LeftControl))
            {
                if (startTime > delayBetweenBulletShots)
                {
                    
                    Bullet bullet = new Bullet(position,isLeft);
                    bulletList.Add(bullet);
                    startTime = 0;
                }
               
            }
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
                if (bulletList[i].rectangle.Intersects(enemy.rectangle))
                {
                    bulletList.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }
            if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;
            }
            if (rectangle.TouchRightOf(newRectangle))
            {   
                position.X = newRectangle.X + newRectangle.Width + 2;
            }
            if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
            // if (position.Y < 0) velocity.Y = 1f;
            //isCrossedMap(xOffset,yOffset);
            // if (position.Y > yOffset - rectangle.Height) position.Y = yOffset - rectangle.Height;
        }
        public void isCrossedMap()
        {
            if (position.Y > mapHeight - rectangle.Height)
            {
                position.Y = 20;
                position.X = 38;
                lives--;
                livePoints = healthBar.fullHealth;
                healthBar.restartHealthBar();
                // dead = true;
            }

        }
        public void knockBack(Vector2 enemyPosition)
        {
            if (enemyPosition.X > position.X)
            {
                velocity += new Vector2(-80, 0);
            }
            else
            {
                velocity += new Vector2(80, 0);
            }
        }
        private Vector2 setFontPosition(int shiftX,int shiftY)
        {
            return new Vector2(-screenWidth / 2 + position.X + shiftX, -screenHeight / 2 + position.Y + shiftY);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position,null, Color.White,0f,Vector2.Zero,1,flip,0);
            healthBar.Draw(spriteBatch,setHealthBarPosition());
            spriteBatch.DrawString(font, "Lifes: " + lives, setFontPosition(30,60), Color.Black);
            spriteBatch.DrawString(font, "Score: " + score, setFontPosition(30,90), Color.Black);
            foreach(Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}
