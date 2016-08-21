using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Rain;

namespace ThePlatformer
{
    public class SpriteCollision
    {
        private readonly Color color;
        public Vector2 position;
        public Rectangle rectangle;
        public Texture2D texture;

        public SpriteCollision(Vector2 position)
        {
            this.position = position;
            texture = null;
            color = Color.White;
        }
        public void LoadContent(ContentManager content, string assetName)
        {
            if (assetName != null)
            {
                texture = content.Load<Texture2D>(assetName);
            }

            OnContentLoaded(content);
        }
        protected virtual void OnContentLoaded(ContentManager content)
        {
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
           // if()
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public virtual void Update()
        {
            //_position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateRectangle();

            //CheckBounds();
        }
        public bool Collision(Raining target)
        {
            var intersects = rectangle.Intersects(target.rectangle) && PerPixelCollision(target);
            
            return intersects;
        }
        private bool PerPixelCollision(Raining target)
        {
            var sourceColors = new Color[(int)(texture.Width * texture.Height)];
            texture.GetData(sourceColors);

            var targetColors = new Color[Raining.texture.Width * Raining.texture.Height];
            Raining.texture.GetData(targetColors);

            var left = Math.Max(rectangle.Left, target.rectangle.Left);
            var top = Math.Max(rectangle.Top, target.rectangle.Top);
            var width = Math.Min(rectangle.Right, target.rectangle.Right) - left;
            var height = Math.Min(rectangle.Bottom, target.rectangle.Bottom) - top;

            var intersectingRectangle = new Rectangle(left, top, width, height);

            for (var x = intersectingRectangle.Left; x < intersectingRectangle.Right; x++)
            {
                for (var y = intersectingRectangle.Top; y < intersectingRectangle.Bottom; y++)
                {
                    var sourceColor = sourceColors[(x - rectangle.Left) + (y - rectangle.Top) * texture.Width];
                    var targetColor = targetColors[(x - target.rectangle.Left) + (y - target.rectangle.Top) * Raining.texture.Width];

                    if (sourceColor.A > 0 && targetColor.A > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
