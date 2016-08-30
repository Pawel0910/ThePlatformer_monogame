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
        public Vector2 position{ get; set; }
        public float rotation { get; set; }
        public Vector2 velocity;
        private float scale = 1f;

        public Raining(Vector2 position)
            :base(position)
        {
            this.position = position;
            animation = new AnimationImpl(200, this, "CustomDrop");
        }

        public static void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("CustomDrop");
        }
        public new void LoadContent(ContentManager Content, GraphicsDevice graphics)
        {
            //animation.LoadConent(Content);
            base.LoadStaticContent(texture, Content, graphics);
            // base.OnContentLoaded(Content, graphics);
        }
        public void Update(long totalGameTime, long elapsedGameTime)
        {
           
            velocity.X = 0.01f;
            velocity.Y = 0.1f;
         
            _position += velocity;
            GameTime gameTime1 = new GameTime(new TimeSpan(totalGameTime), TimeSpan.FromMilliseconds(elapsedGameTime));
            base.Update(gameTime1, animation.changeTextureOnAnimation(gameTime1));


        }
        public bool collision(MarcoPlayer player)
        {
          //  var intersects = perPixelCollision(player);


            return false;
        }
        //private bool perPixelCollision(MarcoPlayer player)
        //{
            
        //    var sourceColors = new Color[rectangle.Width * rectangle.Height];
        //    texture.GetData(sourceColors);

        //    var targetColors = new Color[player.texture.Width*player.texture.Height];
        //    player.texture.GetData(targetColors);

        //    var left = Math.Max(rectangle.Left, MarcoPlayer.rectangle.Left);
        //    var top = Math.Max(rectangle.Top, MarcoPlayer.rectangle.Top);
        //    var width = Math.Min(rectangle.Right, MarcoPlayer.rectangle.Right) - left;
        //    var height = Math.Min(rectangle.Bottom, MarcoPlayer.rectangle.Bottom) - top;

        //    var intersectingRectangle = new Rectangle(left, top, width, height);

        //    for (var x = intersectingRectangle.Left; x < intersectingRectangle.Right; x++)
        //    {
        //        for (var y = intersectingRectangle.Top; y < intersectingRectangle.Bottom; y++)
        //        {
        //            var sourceColor = sourceColors[(x - rectangle.Left) + (y - rectangle.Top) * rectangle.Width];
        //            var targetColor = targetColors[(x - MarcoPlayer.rectangle.Left) + (y - MarcoPlayer.rectangle.Top) * player.texture.Width];

        //            if (sourceColor.A > 0 && targetColor.A > 0)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

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
