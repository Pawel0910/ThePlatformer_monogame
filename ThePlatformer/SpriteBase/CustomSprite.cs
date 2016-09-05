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
        public Vector2 _position;
        public Rectangle _rectangle;
        public Texture2D _texture;
        public Matrix transform;
        public Vector2 _origin;
        ///public IAnimation animation;
        private bool moveAble;
        //private List<Texture2D> textureList;
        public SpriteEffects spriteEffect = SpriteEffects.None;
        public int currentFrame = 0;
        public CustomSprite()
        {

        }
        public CustomSprite(Vector2 position, float speed = 0, float angle = 0, float rotation = 0, float angularVelocity = 0, float scale = 1.0f, bool moveAble = false)
        {
            this._position = position;
            this.angle = 0;
            this.angularVelocity = 0;
            this.scale = scale;
            this.moveAble = moveAble;
            scaleVector = new Vector2(scale, scale);
            //animation = new AnimationImpl(200,this, "marco", "arrow");


            velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));

            _texture = null;
            color = Color.White;
        }

        protected Texture2D Texture => _texture;

        public Vector2 Position => _position;
        public Rectangle Rectangle => _rectangle;

        public bool Collided { get; private set; }
        public void LoadContent(ContentManager content, string assetName)
        {
            _texture = content.Load<Texture2D>(assetName);
            //  animation.LoadConent(content);
            //  animation.setCurrentAnimation(assetName);
            OnContentLoaded();
        }
        public void LoadContent(Texture2D texture)
        {
            _texture = texture;
            OnContentLoaded();
        }
        protected void OnContentLoaded()
        {
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            UpdateTransformMatrix();
            UpdateRectangle();
        }
        protected virtual void OnContentLoaded(GraphicsDevice graphicsDevice)
        {
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
            UpdateTransformMatrix();
            UpdateRectangle();
        }
        public void LoadStaticContent(Texture2D texture, GraphicsDevice graphicsDevice)
        {
            this._texture = texture;
            OnContentLoaded(graphicsDevice);
        }

        private void UpdateRectangle()
        {
            Rectangle rectangle = new Rectangle(0, 0, _texture.Width, _texture.Height);

            this._rectangle = rectangle.Transform(transform);
        }

        public virtual void Unload()
        {
            _texture.Dispose();
        }

        public virtual void Update(GameTime gameTime)
        {
            UpdateRotation(gameTime);
            UpdatePosition(gameTime);
            UpdateTransformMatrix();
            UpdateRectangle();
            // _texture = animation.changeTextureOnAnimation(gameTime) ?? _texture;
        }
        public virtual void Update(GameTime gameTime, Texture2D newTexture)
        {
            UpdatePosition(gameTime);
            UpdateOrigin();
            // UpdateRotation(gameTime);
            UpdateTransformMatrix();
            UpdateRectangle();


            _texture = newTexture ?? _texture;
        }
        public void UpdateOrigin()
        {
            _origin = new Vector2(_texture.Width / 2.0f, _texture.Height / 2.0f);
        }
        private void UpdatePosition(GameTime gameTime)
        {
            if (moveAble)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    _position += new Vector2(-2, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    _position += new Vector2(2, 0);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    _position += new Vector2(0, -2);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    _position += new Vector2(0, 2);
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
                    //    animation.setCurrentAnimation("arrow");
                }
                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    //      animation.setCurrentAnimation("marco");
                }
            }
        }
        private void UpdateTransformMatrix()
        {
            if (moveAble)
            {
                transform = Matrix.CreateTranslation(new Vector3(-_origin, 0)) *
               Matrix.CreateRotationZ(rotation) *
               Matrix.CreateScale(scale) *
               Matrix.CreateTranslation(new Vector3(Position, 0));
            }
            else
            {
                transform = Matrix.CreateTranslation(new Vector3(-_origin, 0)) *
               Matrix.CreateRotationY(rotation) *
               Matrix.CreateScale(scale) *
               Matrix.CreateTranslation(new Vector3(Position, 0));
            }
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

            var collides = _rectangle.Intersects(target._rectangle) && TexturesCollide(transform, target.transform, target);
            Collided = collides;
            target.Collided = collides;
            return collides;
        }
        private bool TexturesCollide(Matrix mat1, Matrix mat2, CustomSprite target)
        {
            Color[,] tex1 = TextureTo2DArray(this);
            Color[,] tex2 = TextureTo2DArray(target);

            Matrix mat1to2 = mat1 * Matrix.Invert(mat2);

            if (tex1 != null && tex2 != null)
            {
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
            }
            return false;
        }
        private Color[,] TextureTo2DArray(CustomSprite sprite)
        {
            Color[,] colors2D;
            try
            {
                Color[] colors1D = new Color[sprite._texture.Width * sprite._texture.Height];

                sprite._texture.GetData(colors1D);
                colors2D = new Color[sprite._texture.Width, sprite._texture.Height];

                for (int x = 0; x < sprite._texture.Width; x++)
                    for (int y = 0; y < sprite._texture.Height; y++)
                        colors2D[x, y] = colors1D[x + y * sprite._texture.Width];
            }
            catch (IndexOutOfRangeException e)
            {
                colors2D = null;
            }

            return colors2D;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, null, null, _origin, rotation, scaleVector, color, spriteEffect);

        }
    }
}
