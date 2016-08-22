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
        public Rectangle rectangleBase;
        public Texture2D texture;
        public float rotationSpeed;
        public float angle=0f;
        public Vector2 origin;
        public Vector2 scale;
        public Matrix transform;
        public float scaleValue = 1;


        public SpriteCollision(Vector2 position)
        {
            this.position = position;
            rotationSpeed=2.0f;
            texture = null;
            origin = Vector2.Zero;
            color = Color.White;
            scale = new Vector2(scaleValue, scaleValue);
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
            origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
           // Vector2 topLeft = position - origin;
            rectangleBase = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public virtual void Update(GameTime gameTime)
        {
            //_position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
           // UpdateRotation(gameTime);
            UpdateRectangle();


            //CheckBounds();
        }
        public void UpdateRotation(GameTime gameTime)
        {
            angle += (float)(rotationSpeed * gameTime.ElapsedGameTime.TotalSeconds);
            if (angle < 0)
            {
                angle = MathHelper.TwoPi - Math.Abs(angle);
            }
            else if(angle>MathHelper.TwoPi)
            {
                angle = angle - MathHelper.TwoPi;
            }
        }
        private void UpdateTransformMatrix()
        {

        }
        public bool Collision(Raining target)
        {
            var intersects = rectangleBase.Intersects(target.rectangle) && PerPixelCollision(target);
            
            return intersects;
        }
        private bool PerPixelCollision(Raining target)
        {
            var sourceColors = new Color[(int)(texture.Width * texture.Height)];
            texture.GetData(sourceColors);

            var targetColors = new Color[Raining.texture.Width * Raining.texture.Height];
            Raining.texture.GetData(targetColors);

            var left = Math.Max(rectangleBase.Left, target.rectangle.Left);
            var top = Math.Max(rectangleBase.Top, target.rectangle.Top);
            var width = Math.Min(rectangleBase.Right, target.rectangle.Right) - left;
            var height = Math.Min(rectangleBase.Bottom, target.rectangle.Bottom) - top;

            var intersectingRectangle = new Rectangle(left, top, width, height);

            for (var x = intersectingRectangle.Left; x < intersectingRectangle.Right; x++)
            {
                for (var y = intersectingRectangle.Top; y < intersectingRectangle.Bottom; y++)
                {
                    var sourceColor = sourceColors[(x - rectangleBase.Left) + (y - rectangleBase.Top) * texture.Width];
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
