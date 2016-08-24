using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ThePlatformer.Sprite;
using ThePlatformer.SpriteBase.Animation;

namespace ThePlatformer
{
    public class CustomSprite
    {
        private readonly Color color;
        private readonly Vector2 velocity;
        public float angle;
        public float rotation;
        private readonly float angularVelocity;
        public float scale;
        public Vector2 scaleVector;
        public Vector2 position;
        public Rectangle rectangle;
        public Texture2D texture;
        public Matrix transform;
        public Vector2 origin;
        public Animation animation;
        private bool moveAble;
        //private List<Texture2D> textureList;
        public SpriteEffects spriteEffect = SpriteEffects.None;
        public int currentFrame = 0;
        public CustomSprite(Vector2 position, float speed = 0, float angle = 0, float rotation = 0, float angularVelocity = 0, float scale = 1.0f,bool moveAble=false)
        {
            this.position = position;
            this.angle = 0;
            this.angularVelocity = 0;
            this.scale = scale;
            this.moveAble = moveAble;
            scaleVector = new Vector2(scale, scale);
            animation = new Animation(200, "marco", "arrow");


            velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));

            texture = null;
            color = Color.White;
        }

        protected Texture2D Texture => texture;

        public Vector2 Position => position;
        public Rectangle Rectangle => rectangle;

        public bool Collided { get; private set; }

        public void LoadContent(ContentManager content, string assetName)
        {
            texture = content.Load<Texture2D>(assetName);
            animation.LoadConent(content);
            if (!moveAble)
                animation.setCurrentAnimation("marco");
            else
                animation.setCurrentAnimation("arrow");
            OnContentLoaded(content);
        }

        protected void OnContentLoaded(ContentManager content)
        {
            origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
            UpdateTransformMatrix();
            UpdateRectangle();
        }
        protected virtual void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        {
            origin = new Vector2(texture.Width / 2.0f, texture.Height / 2.0f);
            UpdateTransformMatrix();
            UpdateRectangle();
        }

        private void UpdateRectangle()
        {
            Rectangle rectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            this.rectangle = rectangle.Transform(transform);
        }

        public virtual void Unload()
        {
            texture.Dispose();
        }

        public virtual void Update(GameTime gameTime)
        {
            //UpdatePosition(gameTime);

            UpdateRotation(gameTime);
            UpdatePosition(gameTime);
            UpdateTransformMatrix();
            UpdateRectangle();
            texture = animation.proceedAnimation(gameTime) ?? texture;
        }
        private void UpdatePosition(GameTime gameTime)
        {
            if (moveAble)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    position += new Vector2(-2, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    position += new Vector2(2, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    position += new Vector2(0, -2);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    position += new Vector2(0, 2);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    rotation = MathHelper.TwoPi;
                    spriteEffect = SpriteEffects.None;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.T))
                {

                    rotation = MathHelper.Pi;
                    spriteEffect = SpriteEffects.FlipVertically;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Q))
                {
                    animation.setCurrentAnimation("arrow");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    animation.setCurrentAnimation("marco");
                }
            }
        }
        private void UpdateTransformMatrix()
        {
            if (moveAble)
            {
                transform = Matrix.CreateTranslation(new Vector3(-origin, 0)) *
               Matrix.CreateRotationZ(rotation) *
               Matrix.CreateScale(scale) *
               Matrix.CreateTranslation(new Vector3(Position, 0));
            }
            else
            {
                transform = Matrix.CreateTranslation(new Vector3(-origin, 0)) *
               Matrix.CreateRotationY(rotation) *
               Matrix.CreateScale(scale) *
               Matrix.CreateTranslation(new Vector3(Position, 0));
            }
                
           
               // _transform = Matrix.CreateTranslation(new Vector3(-_origin, 0)) *
               //Matrix.CreateRotationZ(_rotation) *
               //Matrix.CreateScale(_scale) *
               //Matrix.CreateTranslation(new Vector3(Position, 0));
        }

        private void UpdateRotation(GameTime gameTime)
        {
            if (rotation < 0)
            {
                rotation = MathHelper.TwoPi - Math.Abs(rotation);
            }
            else if (rotation > MathHelper.TwoPi)
            {
                rotation = rotation - MathHelper.TwoPi;
            }
        }

        public bool Collision(CustomSprite target)
        {
            Color[,] sourceColor = TextureTo2DArray(this);
            Color[,] targetColor = TextureTo2DArray(target);
            var collides = rectangle.Intersects(target.rectangle) && TexturesCollide(sourceColor, transform, targetColor, target.transform);
            Collided = collides;
            target.Collided = collides;
            return collides;
        }
        private bool TexturesCollide(Color[,] tex1, Matrix mat1, Color[,] tex2, Matrix mat2)
        {
            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);
            int width1 = tex1.GetLength(0);
            int height1 = tex1.GetLength(1);
            int width2 = tex2.GetLength(0);
            int height2 = tex2.GetLength(1);

            for (int x1 = 0; x1 < width1; x1++)
            {
                for (int y1 = 0; y1 < height1; y1++)
                {
                    Vector2 pos1 = new Vector2(x1, y1);
                    Vector2 pos2 = Vector2.Transform(pos1, mat1to2);

                    int x2 = (int)pos2.X;
                    int y2 = (int)pos2.Y;
                    if ((x2 >= 0) && (x2 < width2))
                    {
                        if ((y2 >= 0) && (y2 < height2))
                        {
                            if (tex1[x1, y1].A > 0)
                            {
                                if (tex2[x2, y2].A > 0)
                                {
                                    Vector2 screenPos = Vector2.Transform(pos1, mat1);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        private Color[,] TextureTo2DArray(CustomSprite sprite)
        {
            Color[] colors1D = new Color[sprite.texture.Width * sprite.texture.Height];
            sprite.texture.GetData(colors1D);

            Color[,] colors2D = new Color[sprite.texture.Width, sprite.texture.Height];
            for (int x = 0; x < sprite.texture.Width; x++)
                for (int y = 0; y < sprite.texture.Height; y++)
                    colors2D[x, y] = colors1D[x + y * sprite.texture.Width];

            return colors2D;
        }


        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(texture, position, null, null,origin, rotation, scaleVector, color, spriteEffect);

        }
    }
}
