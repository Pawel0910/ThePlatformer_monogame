using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using ThePlatformer.Characters.Enemies.EnemiesManager;
using ThePlatformer.Enemies;

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

        private ManualResetEvent startDrawing;
        private ManualResetEvent waitForEndDrawing;
        private ManualResetEvent buffor;
        // private Raining rainTest = new Raining();

        public static bool TEST = false;

        private MarcoPlayer player;
        private EnemiesManager enemyManager;
        private MapManager mapManager = MapManager.getInstance();

        private GraphicsDevice graphics;
        private ContentManager Content;
        private int dropAmount = 300;
        private Stopwatch stopwatch;
        public RainManager(MarcoPlayer player, EnemiesManager enemyManager)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            stopwatch = new Stopwatch();
            endComputing = new ManualResetEvent(true);
            waitForComputing = new AutoResetEvent(false);
            blockIfMainThreadFirst = new ManualResetEvent(false);

            startDrawing = new ManualResetEvent(false);
            waitForEndDrawing = new ManualResetEvent(false);
            buffor = new ManualResetEvent(false);
        }
        public void Load(ContentManager Content, GraphicsDevice graphics)
        {

            this.graphics = graphics;
            Raining.Load(Content);
            setDropAmount();
            this.Content = Content;
            loadList();

            for (int i = 0; i < rainList.Count; i++)
            {
                rainList[i].LoadContent(Content, graphics);
            }

        }
        private void setDropAmount()
        {
            if (graphics.Viewport.Width <= 800 && graphics.Viewport.Height <= 600)
            {
                dropAmount = 150;
            }
        }
        public void UpdateTest(long totalGameTime, long elapsedGameTime)
        {
            for (int i = 0; i < rainList.Count; i++)
            {
                if (stopwatch.ElapsedMilliseconds > 0)
                    createWind(rainList[i], stopwatch.ElapsedMilliseconds);

                rainList[i].Update(totalGameTime, elapsedGameTime);
                if (player.isCollisionWithSprite(rainList[i]))
                {
                    rainList.RemoveAt(i);
                    addOne();
                }

                for (int j = 0; j < enemyManager.getEnemies().Count; j++)
                {
                    EnemyBase enemy = enemyManager.getEnemies()[j];
                    if (enemy != null && enemy.isCollisionWithSprite(rainList[i]))
                    {
                        rainList.RemoveAt(i);
                        addOne();
                    }
                }
                foreach (CollisionTile tile in mapManager.getMap().CollisionTiles)
                {
                    if (rainList[i]._rectangle.Intersects(tile.Rectangle))
                    {
                        rainList.RemoveAt(i);
                        addOne();
                    }
                }
                if (rainList[i].outOfBound(graphics.Viewport.Bounds, player._position))
                {
                    rainList.RemoveAt(i);
                    addOne();
                }

            }

            EndFrame();
        }
        public void EndFrame()
        {
            blockIfMainThreadFirst.WaitOne();
            blockIfMainThreadFirst.Reset();
            endComputing.Reset();
            waitForComputing.Set();
            endComputing.WaitOne();
        }
        public void createWind(Raining drop, long gameTime)
        {

            if (gameTime > drop.delay && drop.dustWinds < 10)
            {
                drop._position.X += 10f;
                drop._position.Y += 1f;
                drop.dustWinds++;

            }
        }
        public void waitForEndOfUpdate()
        {
            blockIfMainThreadFirst.Set();
            waitForComputing.WaitOne();
            endComputing.Set();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < rainList.Count; i++)
            {
                try
                {
                    rainList[i].Draw(spriteBatch);
                }
                catch (Exception e)
                {

                }
            }
        }
        public void DrawOrigin(SpriteBatch spriteBatch)
        {
            startDrawing.WaitOne();
            startDrawing.Reset();

            foreach (Raining rain in rainList)
            {
                rain.Draw(spriteBatch);
            }
            endDraw();
        }
        public void endDraw()//currentThread
        {
            waitForEndDrawing.Set();
        }
        public void waitForEndDraw()//mainThread
        {
            waitForEndDrawing.WaitOne();
            waitForEndDrawing.Reset();
        }
        public void resetDrawEvent()
        {
            startDrawing.Set();
        }


        private void loadList()
        {
            int xStart = (int)(player._position.X - graphics.Viewport.Width / 2);
            int xEnd = xStart + graphics.Viewport.Width;
            int yStart = (int)(player._position.Y - graphics.Viewport.Height / 2) + (graphics.Viewport.Height - 700);
            int yEnd = yStart + graphics.Viewport.Height;
            if (rainList.Count < dropAmount)
            {
                int size = rainList.Count;
                for (int i = 0; i < dropAmount - size; i++)
                {
                    Raining rain = new Raining(new Vector2(randInt(xStart, xEnd), randInt(yStart, yEnd)));
                    rain.LoadContent(Content, graphics);
                    rainList.Add(rain);
                }
            }
        }
        private void addOne()
        {
            int xStart = (int)(player._position.X - graphics.Viewport.Width / 2);
            int xEnd = xStart + graphics.Viewport.Width;
            int yStart = (int)(player._position.Y - graphics.Viewport.Height);
            int yEnd = yStart + graphics.Viewport.Height / 2;
            Raining rain = new Raining(new Vector2(randInt(xStart - graphics.Viewport.Width / 4, xEnd + graphics.Viewport.Width / 4), randInt(yStart, yEnd)));
            rain.LoadContent(Content, graphics);
            rainList.Add(rain);
        }

        private int randInt(int minRange, int maxRange)
        {
            return random.Next(minRange, maxRange);
        }
    }
}
