using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer
{
    class MapManager
    {
        Map map;
        public void Initialize()
        {
            map = new Map();
        }
        public void LoadContent(ContentManager Content)
        {
            Tile.Content = Content;
            map.Generate(new int[,]
           {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,0,0,0,0,0,1,0,1,1,1,0,0,0,1},
                {2,2,2,2,2,2,2,1,0,1,1,1,2,0,2,2,2,1,1,0,2},
                {2,2,2,2,2,2,2,2,1,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
           }, 80);
        }
        public int getMapWidth()
        {
            return map.Width;
        }
        public int getMapHeight()
        {
            return map.Height;
        }
        public Map getMap()
        {
            return map;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch);
        }
    }
}
