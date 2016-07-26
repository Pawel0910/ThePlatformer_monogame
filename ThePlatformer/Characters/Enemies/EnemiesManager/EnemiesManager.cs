using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Enemies;

namespace ThePlatformer.Characters.Enemies.EnemiesManager
{
    public class EnemiesManager
    {
        public List<EnemyBase> enemiesList = new List<EnemyBase>();

        public EnemiesManager()
        {
        }
        public void Initialize()
        {
            enemiesList.Add(new NormalEnemy());
            enemiesList.Add(new ShootingEnemy());
        }

        public void LoadContent(ContentManager content)
        {
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.Load(content, "idle3", new Vector2(150, 10));
            }
        }

        public void CollisionsAndUpdate(GameTime gameTime, Map map)
        {
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.Update(gameTime);
            }
            foreach (CollisionTile tile in map.CollisionTiles)
            {
                foreach (EnemyBase enemy in enemiesList)
                {
                    enemy.CollisionMap(tile.Rectangle, map.Width, map.Height);
                }
            }
        }
        public void collisionsWithPlayer(MarcoPlayer player)
        {
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.allCollisionWithPlayer(player);
                player.allCollisionsWithEnemies(enemy);//!!!!!!!!!!!!!!!!!!!!!!!
            }
            deleteDeadEnemiesFromGame();

        }
        public void restartEnemies()
        {
            enemiesList.Clear();    
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.Draw(spriteBatch);
            }
        }
        private void deleteDeadEnemiesFromGame()
        {
            for (int i = 0; i < enemiesList.Count; i++)
            {
                if (enemiesList[i].isDead)
                {
                    enemiesList.RemoveAt(i);
                }
            }
        }

    }
}
