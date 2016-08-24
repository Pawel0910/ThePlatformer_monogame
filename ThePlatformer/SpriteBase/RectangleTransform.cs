using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Sprite
{
    public static class RectangleTransform
    {
        public static Rectangle Transform(this Rectangle source, Matrix transform)
        {
            Vector2 topLeft = Vector2.Transform(new Vector2(source.Left, source.Top), transform);
            Vector2 topRight = Vector2.Transform(new Vector2(source.Right, source.Top), transform);
            Vector2 bottomLeft = Vector2.Transform(new Vector2(source.Left, source.Bottom), transform);
            Vector2 bottomRight = Vector2.Transform(new Vector2(source.Right, source.Bottom), transform);

            Vector2 min = new Vector2(MathEx.Min(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X), MathEx.Min(topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y));
            Vector2 max = new Vector2(MathEx.Max(topLeft.X, topRight.X, bottomLeft.X, bottomRight.X), MathEx.Max(topLeft.Y, topRight.Y, bottomLeft.Y, bottomRight.Y));

            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}
