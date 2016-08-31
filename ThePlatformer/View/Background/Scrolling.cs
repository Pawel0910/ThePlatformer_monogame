using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.View.Background
{
    public class Scrolling : Background
    {
        public Scrolling() { }
        public void Update(Vector2 position, int screenWidth, int screenHeight)
        {
            rect.X = (int)position.X - screenWidth / 2;
            rect.Y = (int)position.Y - screenHeight / 2;
            // rect.X -= 3;
        }
    }
}
