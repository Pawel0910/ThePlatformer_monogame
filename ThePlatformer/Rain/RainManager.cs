using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ThePlatformer.Rain
{
    public class RainManager
    {
        private List<Raining> rainList = new List<Raining>();
        private Random random = new Random();

        private ManualResetEvent endComputing;//zatrzymuje wątek RainMangera po ukończeniu jego pracy   
        private ManualResetEvent blockIfMainThreadFirst;//w przypadku gdyby thread RainManagera uzyskał dostęp
        //jako pierwszy musi być ustawiony event który to zdarzenie obsłuży i zablokuje wątek
        private AutoResetEvent waitForComputing;//zatrzymuje główny wątek gdyby ten skończy pracę pierwszy
        private Raining rain;
        public RainManager()
        {
            rain = new Raining();
            endComputing = new ManualResetEvent(true);
            waitForComputing = new AutoResetEvent(false);
            blockIfMainThreadFirst = new ManualResetEvent(false);
        }
        public void Load(ContentManager Content)
        {
            Raining.Load(Content);
        }    
        public void Update(GameTime gameTime)
        {
            
            loadList();
            foreach (Raining rain in rainList)
            {
                rain.Update(gameTime);
            }

        }
        public void UpdateTest(long elapsedTime)
        {
            loadList();
            foreach (Raining rain in rainList)
            {
                rain.Update(elapsedTime);
            }
            EndFrame();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Raining rain in rainList)
            {
                rain.Draw(spriteBatch);
            }
        }
        public void DrawOrigin(SpriteBatch spriteBatch)
        {
            foreach (Raining rain in rainList)
            {
                rain.Draw(spriteBatch);
            }
        }
        public void EndFrame()
        {
            blockIfMainThreadFirst.WaitOne();
            blockIfMainThreadFirst.Reset();
            endComputing.Reset();
            waitForComputing.Set();
            endComputing.WaitOne();
        }
        public void waitForEndOfUpdate()
        {

            blockIfMainThreadFirst.Set();
            waitForComputing.WaitOne();
            endComputing.Set();
        }
        private void loadList()
        {
            if (rainList.Count < 10000)
            {
                int size = rainList.Count;
                for(int i = 0; i < 10000 - size; i++)
                {
                    Raining rain = new Raining();
                    rain.position = new Vector2(randInt(20, 200), randInt(-100, 20));
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
