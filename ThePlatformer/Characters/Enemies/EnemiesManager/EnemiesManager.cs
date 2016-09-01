﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private Random random = new Random();

        private MapManager mapManager = MapManager.getInstance();
        private long elapsed, nextWave = 3000;
        public int wave = 1, waveMultipier = 2;
        public EnemiesManager()
        {
        }
        //public void spawnEnemies()
        //{
        //    if (enemiesList.Count < enemiesAmountOnMap)
        //    {
        //        int size = enemiesList.Count;
        //        for (int i = 0; i < enemiesAmountOnMap-size; i++)
        //        {
        //            EnemyBase enemy = new ShootingEnemy(new Vector2(randInt(0, mapManager.getMapWidth()),10));
        //        }
        //    }
        //}
        public void addEnemy()
        {
            EnemyBase enemy = new ShootingEnemy(new Vector2(randInt(0, mapManager.getMapWidth() - 200), -500));
            enemy.Load(EnemyTextures.idle);
            enemiesList.Add(enemy);
        }
        private int randInt(int minRange, int maxRange)
        {
            return random.Next(minRange, maxRange);
        }
        public void Restart()
        {
            restartEnemies();
            enemiesList.Add(new RunningEnemy(new Vector2(150, 10)));
            enemiesList.Add(new ShootingEnemy(new Vector2(190, 10)));
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.Load(EnemyTextures.idle);
            }
        }
        public List<EnemyBase> getEnemies()
        {
            return enemiesList;
        }
        public void Initialize()
        {
            enemiesList.Add(new RunningEnemy(new Vector2(150, -20)));
            enemiesList.Add(new ShootingEnemy(new Vector2(150, -500)));
        }

        public void LoadContent(ContentManager content)
        {
            EnemyTextures.Load(content);
            EnemyBase.setContent(content);
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.Load(EnemyTextures.idle);
            }
        }
        private int test = 0;
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.T) && test < 5)
            {
                addEnemy();
                test++;

            }
            foreach (EnemyBase enemy in enemiesList)
            {
                enemy.Update(gameTime);
            }
            spawnNewEnemies(gameTime);

        }
        private void spawnNewEnemies(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > nextWave)
            {
                for (int i = 0; i < 1; i++)
                {
                    addEnemy();
                }
                waveAnalyzer();
                wave++;
                elapsed = 0;
            }
        }
        private void waveAnalyzer()
        {
            if (wave > 4 * waveMultipier)
            {
                nextWave /= 2;
                waveMultipier++;
            }
        }
        public void CollisionsWithMap(Map map)
        {
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
                    if (enemiesList[i].GetType() == typeof(RunningEnemy))
                    {
                        RunningEnemy enemy = (RunningEnemy)enemiesList[i];
                        enemy.spawnTreasure();
                    }
                    enemiesList.RemoveAt(i);
                }
            }
        }

    }
}
