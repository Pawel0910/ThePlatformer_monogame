using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Characters.Enemies;
using ThePlatformer.Enemies;
using ThePlatformer.Treasures;

namespace ThePlatformer
{
    public class RunningEnemy : EnemyBase
    {
        private int distanceToPlayer = 100;
        private float startTime = 0, endTime = 0;

        public RunningEnemy(Vector2 position)
            : base(position)
        {
        }
        public override void Update(GameTime gameTime)
        {
            if (_position.Y > mapManager.getMapHeight() - _rectangle.Height)//"teleportacja"
            {
                _position.X = MarcoPlayer.rectangleStatic.X + 300;
                _position.Y = -20;
            }
            if (_rectangle.X - MarcoPlayer.rectangleStatic.X < distanceToPlayer)
            {
                _position += new Vector2(4, 0);
            }
            else if (_rectangle.X < MarcoPlayer.rectangleStatic.X)
            {
                _position += new Vector2(0, 0);
            }

            spawnTreasure();

            base.Update(gameTime);
        }
        public void spawnTreasure()
        {
            if (isDead)
            {
                BaseTreasureAbstract treasure = new TreasureChest(_position);
                TreasureManager.addTreasure(treasure);
            }
        }
    }
}
