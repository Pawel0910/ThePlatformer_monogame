using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.SpriteBase
{
    public class DebugSprite : CustomSprite
    {
        private readonly Color _rectangleColor;
        private Texture2D _rectangleTexture;

        public DebugSprite(Vector2 position, Color rectangleColor, float speed = 0, float angle = 0, float rotation = 0, float angularVelocity = 0, float scale = 1.0f, bool moveAble = false)
            : base(position, speed, angle, rotation, angularVelocity, scale, moveAble)
        {
            _rectangleColor = rectangleColor;
        }

        //protected override void OnContentLoaded(ContentManager content, GraphicsDevice graphicsDevice)
        //{
        //    var colors = new Color[Texture.Width * Texture.Height];

        //    colors[0] = _rectangleColor;
        //    colors[Texture.Width - 1] = _rectangleColor;
        //    colors[(Texture.Width * Texture.Height) - Texture.Width] = _rectangleColor;
        //    colors[(Texture.Width * Texture.Height) - 1] = _rectangleColor;

        //    _rectangleTexture = new Texture2D(graphicsDevice, Texture.Width, Texture.Height);
        //    _rectangleTexture.SetData(colors);

        //    base.OnContentLoaded(content, graphicsDevice);
        //}

       public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_rectangleTexture, null, Rectangle, null, null, 0, null, Color.White);
           // spriteBatch.Draw(Texture, Position, null, null, Vector2.Zero, 0, null, Color.Black * 0.1f);

            base.Draw(spriteBatch);
        }
    }
}
