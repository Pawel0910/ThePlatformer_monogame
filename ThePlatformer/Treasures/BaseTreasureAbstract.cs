using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Health;

namespace ThePlatformer.Treasures
{
    public class BaseTreasureAbstract
    {
        public bool upgradeShooting = false;
        public bool isExist = true;
        public Rectangle rectangle;
        public Texture2D texture;
        public Vector2 velocity, position = new Vector2(100, 10);//velocity jest, by działała na niego grawitacja
        public BaseTreasureAbstract(Vector2 position)
        {
            this.position = position;
        }
        public void Load(ContentManager Content, String path)
        {
            texture = Content.Load<Texture2D>(path);
        }
        public void Update(GameTime gameTime, MarcoPlayer player)
        {
            position += velocity;
            rectangle = new Rectangle((int)position.X + 10, (int)position.Y, texture.Width - 20, texture.Height);
            gravity();
            treasureCollectCollision(player);
            upgrade(player);
        }
        public virtual void upgrade(MarcoPlayer player) { }
        public void CollisionMap(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
            }
            else if (rectangle.TouchLeftOf(newRectangle))
            {
                position.X = newRectangle.X - rectangle.Width - 2;

            }
            else if (rectangle.TouchBottomOf(newRectangle))
            {
                velocity.Y = 1f;
            }
            if (position.X < 0) position.X = 0;
            if (position.X > xOffset - rectangle.Width) position.X = xOffset - rectangle.Width;
        }
        private void gravity()
        {
            //grawitacja
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
        }

        public void treasureCollectCollision(MarcoPlayer player)
        {
            if (player._rectangle.TouchTopOf(this.rectangle))
            {
                player.velocity += new Vector2(0, -10);
                destroyTreasure();
            }
            else if (player._rectangle.TouchLeftOf(this.rectangle))
            {
                if (player.velocity.X > 0)
                    player.velocity.X = 0;
            }
            else if (player._rectangle.TouchRightOf(this.rectangle))
            {
                if (player.velocity.X < 0)
                    player.velocity.X = 0;
                // player._position.X = position.X + rectangle.Width + 2;
            }
        }
        public void downGradePlayerShooting(MarcoPlayer player)
        {

        }
        private void destroyTreasure()
        {
            isExist = false;
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            if (isExist)
            {
                spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            if (upgradeShooting)
            {

            }
        }
    }
}
