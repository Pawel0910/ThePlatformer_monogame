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

        private ManualResetEvent _renderActive;
        private ManualResetEvent _renderComandsReady;
        private ManualResetEvent _renderCompleted;
        private Raining rain;
        public RainManager()
        {
            rain = new Raining();
            //_renderComandsReady = new ManualResetEvent(false);

            //_renderActive = new ManualResetEvent(false);
            //_renderCompleted = new ManualResetEvent(true);
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
            //rain.Update(elapsedTime);
            //EndFrame();
        }
        public void resetEvents()
        {
            _renderActive.Reset();
            _renderCompleted.Set();
            _renderComandsReady.WaitOne();

            _renderCompleted.Reset();
            _renderComandsReady.Reset();
            _renderActive.Set();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _renderActive.Reset();
            _renderCompleted.Set();
            _renderComandsReady.WaitOne();

            _renderCompleted.Reset();
            _renderComandsReady.Reset();
            _renderActive.Set();
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
            _renderCompleted.WaitOne();
            _renderComandsReady.Set();
            _renderActive.WaitOne();
        }
        private void populateList()
        {
            if (rainList.Count < 1)
            {
                    Raining rain = new Raining();
                    rain.position = new Vector2(20, 40);
                    rainList.Add(rain);
            }
        }
        private void loadList()
        {
            if (rainList.Count < 1000000)
            {
                int size = rainList.Count;
                for(int i = 0; i < 1000000 - size; i++)
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
