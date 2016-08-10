using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Rain
{
    public class RainManager
    {
        private List<Raining> rainList = new List<Raining>();
        private Random random = new Random();

        public void Load(ContentManager Content)
        {
            Raining.Load(Content);
        }    
        public void Update(GameTime gameTime)
        {
            loadList();
            foreach(Raining rain in rainList)
            {
                rain.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Raining rain in rainList)
            {
                rain.Draw(spriteBatch);
            }
        }
        private void loadList()
        {
            if (rainList.Count < 1000)
            {
                for(int i = 0; i < 1000 - rainList.Count; i++)
                {
                    Raining rain = new Raining();
                    rain.position = new Vector2(randInt(20, 200), randInt(-1000, 20));
                    rainList.Add(rain);
                }
            }
        }
        private int randInt(int minRange,int maxRange)
        {
            return random.Next(minRange, maxRange);
        }
    }
}
