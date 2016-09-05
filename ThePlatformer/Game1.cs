using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TexturePackerLoader;
using ThePlatformer.View.Menu;
using ThePlatformer.Characters.Enemies.EnemiesManager;
using ThePlatformer.Characters.Player;
using ThePlatformer.Rain;
using System.Threading;
using System.Threading.Tasks;
using ThePlatformer.SpriteBase;
using ThePlatformer.View.Background;
using System.Diagnostics;
using ThePlatformer.Treasures;
using ThePlatformer.Health;

namespace ThePlatformer
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private MapManager mapManager = MapManager.getInstance();
        private MenuViewManager menuManager = new MenuViewManager();
        private PlayerManager playerManager = new PlayerManager();
        private EnemiesManager enemiesManager = new EnemiesManager();
        private TreasureManager treasureManager;
        private Stopwatch clock = new Stopwatch();
        private SpriteFont font;
        public static long EndTime = 60 * 1000;//czas odliczany do konca gry

        private RainManager rainManager;
        private bool firsLoad = true;
        public enum GameState
        {
            MainMenu,
            Options,
            Playing,
            Pause,
            DeadMenu
        }
        public static GameState CurrentGameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            // graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            // graphics.IsFullScreen = true;
            //if (!graphics.IsFullScreen)
            //    graphics.ToggleFullScreen();
            graphics.ApplyChanges();
            if (firsLoad)
                mapManager.Initialize();
            playerManager.Initialize();
            enemiesManager.Initialize();
            treasureManager = new TreasureManager(playerManager);
            rainManager = new RainManager(playerManager.getPlayer(), enemiesManager);
            base.Initialize();

            CurrentGameState = GameState.MainMenu;
            if (firsLoad)
            {
                Task.Factory.StartNew(() =>
                {
                    var gl = new UpdateLoop(rainManager);
                    gl.Loop();
                });
            }

            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            // _arrow1.LoadContent(Content, "arrow1");
            rainManager.Load(Content, GraphicsDevice);
            font = Content.Load<SpriteFont>("healthsFont");

            treasureManager.Load(Content, GraphicsDevice);
            menuManager.LoadContent(Content, this);
            #region Map initialize
            if (firsLoad)
                mapManager.LoadContent(Content);
            #endregion

            playerManager.LoadContent(Content, GraphicsDevice.Viewport, GraphicsDevice);
            enemiesManager.LoadContent(Content);

        }
        protected override void Update(GameTime gameTime)
        {
            // _arrow1.Update(gameTime);

            switch (CurrentGameState)
            {
                #region MainMen update
                case GameState.MainMenu:

                    menuManager.Update(gameTime, GraphicsDevice);
                    menuManager.UpdateMainMenu(gameTime);
                    break;
                #endregion
                #region Playing update
                case GameState.Playing:
                    clock.Start();
                    menuManager.UpdatePlaying();

                    playerManager.Update(gameTime, GraphicsDevice);

                    enemiesManager.Update(gameTime);
                    enemiesManager.CollisionsWithMap(mapManager.getMap());

                    enemiesManager.collisionsWithPlayer(playerManager.getPlayer());

                    treasureManager.Update(gameTime);
                    rainManager.waitForEndOfUpdate();
                    checkIfNotExceedEndTime();
                    break;
                #endregion
                #region Pause update
                case GameState.Pause:
                    menuManager.UpdatePause(gameTime);
                    break;
                #endregion
                #region DeadMenu update
                case GameState.DeadMenu:
                    menuManager.UpdateDeadMenu(gameTime);
                    break;
                    #endregion
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (CurrentGameState)
            {
                #region MainMenu Draw
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    menuManager.DrawMainMenu(spriteBatch);
                    break;
                #endregion
                #region Pause Draw
                case GameState.Pause:
                    clock.Stop();
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    menuManager.DrawPause(spriteBatch);
                    break;
                #endregion
                #region DeadMenu Draw
                case GameState.DeadMenu:
                    clock.Stop();

                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    menuManager.DrawDeadMenu(spriteBatch);
                    break;
                #endregion
                #region Playing Draw
                case GameState.Playing:
                    playerManager.Draw(spriteBatch, gameTime);
                    rainManager.Draw(spriteBatch);
                    if (RainManager.TEST)
                    {
                        GraphicsDevice.Clear(Color.Red);
                    }

                    mapManager.Draw(spriteBatch);
                    enemiesManager.Draw(spriteBatch);
                    treasureManager.Draw(spriteBatch, gameTime);
                    spriteBatch.DrawString(font, "Operation time: " + EndTime / 1000, setRightCornerFontPosition(175, 30), Color.Black);

                    spriteBatch.DrawString(font, "Time: " + clock.ElapsedMilliseconds / 1000, setRightCornerFontPosition(175, 50), Color.Black);
                    break;
                    #endregion
            }

            spriteBatch.End();
        }
        private Vector2 setRightCornerFontPosition(int shiftX, int shiftY)
        {
            return new Vector2(playerManager.getPlayer()._position.X + graphics.PreferredBackBufferWidth / 2 - shiftX,
                playerManager.getPlayer()._position.Y - graphics.PreferredBackBufferHeight / 2 + shiftY);
        }
        private void checkIfNotExceedEndTime()
        {
            if (clock.ElapsedMilliseconds > EndTime)
            {
                CurrentGameState = GameState.DeadMenu;
            }
        }
        public void restart()
        {
            firsLoad = false;
            enemiesManager.Restart();
            playerManager.restart();
            CurrentGameState = GameState.Playing;
            clock.Reset();
            UpgradeBar.spawnUpgradeBar = false;
            EndTime = 40 * 1000;
        }
    }
}