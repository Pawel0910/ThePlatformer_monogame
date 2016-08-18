using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Rain
{
    public class UpdateLoop
    {
        private readonly Stopwatch stopwatch;
        private long lastElapsed;

        private RainManager rainManager;

        public UpdateLoop(RainManager rainManager)
        {
            this.rainManager = rainManager;
            stopwatch = new Stopwatch();
        }

        public void Loop()
        {
            stopwatch.Start();
            while (true)
            {
                Update();
            }
        }
        public void LoopDraw(SpriteBatch spriteBatch)
        {
            while (true)
            {
                rainManager.DrawOrigin(spriteBatch);
            }
        }

        private void Update()
        {
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            var elapsed = elapsedMilliseconds - lastElapsed;
            lastElapsed = elapsedMilliseconds;
            rainManager.UpdateTest(elapsed);
        }
    }
}
