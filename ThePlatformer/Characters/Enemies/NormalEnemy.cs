using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Enemies;

namespace ThePlatformer
{
    public class NormalEnemy : EnemyBase
    {
        private int distanceToPlayer = 100;
        private float startTime = 0, endTime = 0;

        public override void Update(GameTime gameTime)
        {
            if (position.Y > mapManager.getMapHeight() - rectangle.Height)
            {
                position.X = MarcoPlayer.rectangle.X + 300;
                position.Y = -20;
            }
            if (rectangle.X - MarcoPlayer.rectangle.X < distanceToPlayer)
            {
                position += new Vector2(4, 0);
            }
            
            base.Update(gameTime);
        }
        
    }
}
