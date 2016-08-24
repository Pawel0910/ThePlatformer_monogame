using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThePlatformer.Rain
{
    public class Raining
    {
        public static Texture2D texture;
        public Vector2 position{ get; set; }
        public float rotation { get; set; }
        public Vector2 velocity;
        public Rectangle rectangle;
        private float scale = 1f;

        public Raining(Vector2 position) 
        {
            this.position = position;
        }

        public static void Load(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("CustomDrop");
        }

        public void Update(long elapsedTime)
        {
           
            velocity.X = 0.01f;
            velocity.Y = (float)elapsedTime / 96;
            
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width),
                (int)(texture.Height));

            position += velocity;

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

        public bool isCollisionWithPlayer()
        {
            if (this.rectangle.Intersects(MarcoPlayer.rectangleStatic))
            {
                return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
