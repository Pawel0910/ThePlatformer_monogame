using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThePlatformer.SpriteBase.Animation;

namespace ThePlatformer.Rain
{
    public class Raining : CustomSprite
    {
        private static Texture2D texture { get; set; }
        private IAnimation animation;
        public Vector2 position { get; set; }
        public float rotation { get; set; }
        public Vector2 velocity;
        private float scale = 1f;
        public int delay = 100;
        public int dustWinds;
        public Raining(Vector2 position, float scale = 0.7f)
            : base(position, 0, 0, 0, 0, scale)
        {
            this.position = position;
            animation = new AnimationImpl(200, this, "Drop1");
        }

        public static void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("CustomDrop");
        }
        public void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            delay += (int)_position.X;

            //animation.LoadConent(Content);
            base.LoadStaticContent(texture, Content, graphics);
            // base.OnContentLoaded(Content, graphics);
        }
        public void Update(long totalGameTime, long elapsedGameTime)
        {

            velocity.X = 2f;
            velocity.Y = 8f;

            _position += velocity;
            GameTime gameTime1 = new GameTime(new TimeSpan(totalGameTime), TimeSpan.FromMilliseconds(elapsedGameTime));
            base.Update(gameTime1);


        }
        public bool outOfBound(Rectangle screenBound, Vector2 middleScreen)
        {
            int xStart = (int)(middleScreen.X - screenBound.Width / 2);
            int xEnd = xStart + screenBound.Width;
            int yStart = (int)(middleScreen.Y - screenBound.Height / 2);
            int yEnd = yStart + screenBound.Height;

            if (_position.X < xStart - screenBound.Width / 4)//lewa strona
            {
                return true;
            }
            else if (_position.X > xEnd + screenBound.Width / 4)//prawa strona
            {
                return true;
            }
            else if (_position.Y < yStart - screenBound.Height / 4)//- screenBound.Height/4 buffor na krople spadajace od gory :) 
            {
                return true;
            }
            else if (_position.Y > yEnd)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public bool collision(MarcoPlayer player)
        {
            //  var intersects = perPixelCollision(player);


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
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
