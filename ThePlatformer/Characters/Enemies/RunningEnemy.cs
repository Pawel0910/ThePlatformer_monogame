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
using ThePlatformer.SpriteBase.Animation;
using ThePlatformer.Treasures;

namespace ThePlatformer
{
    public class RunningEnemy : EnemyBase
    {
        private int distanceToPlayer = 100;
        private float startTime = 0, endTime = 0;
        private Random rand;
        public RunningEnemy(Vector2 position)
            : base(position)
        {
            rand = new Random();
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
                velocity.X = 4;
                //_position += new Vector2(4, 0);
            }
            else if (_rectangle.X < MarcoPlayer.rectangleStatic.X)
            {
                velocity.X = 0;
                _position += new Vector2(0, 0);
            }
            if (_rectangle.X - MarcoPlayer.rectangleStatic.X > distanceToPlayer)
            {
                velocity.X = 0;


            }

            spawnTreasure();

            base.Update(gameTime);
        }
        public void spawnTreasure()
        {
            if (isDead)
            {
                BaseTreasureAbstract treasure;
                switch (randTreasure())
                {
                    case 0:
                        treasure = new TreasureUpgrade(new Vector2(_position.X, _position.Y - 50));
                        break;
                    case 1:
                        treasure = new TreasureHealth(new Vector2(_position.X, _position.Y - 50));
                        break;
                    case 2:
                        treasure = new TreasureTime(new Vector2(_position.X, _position.Y - 50));
                        break;
                    default:
                        treasure = new TreasureUpgrade(new Vector2(_position.X, _position.Y - 50));
                        break;
                }
                TreasureManager.addTreasure(treasure);
            }
        }
        private int randTreasure()
        {
            return rand.Next(0, 3);
        }
    }
}