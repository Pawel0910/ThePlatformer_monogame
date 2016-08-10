using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TexturePackerLoader;
using ThePlatformer.View.Menu;
using ThePlatformer.Characters.Enemies.EnemiesManager;
using ThePlatformer.Characters.Player;
using ThePlatformer.Rain;
namespace ThePlatformer
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteSheet spriteSheet;
        SpriteRender spriteRender;
        private Player player;
        private PlayerTexturePackerTest playerTxtPacker;
        private MenuViewManager menuManager= new MenuViewManager();
        private PlayerManager playerManager = new PlayerManager();
        private EnemiesManager enemiesManager = new EnemiesManager();
        private MapManager mapManager = MapManager.getInstance();
        //TEST
        private RainManager rainManager = new RainManager();
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
            mapManager.Initialize();
            enemiesManager.Initialize();
            base.Initialize();
            CurrentGameState = GameState.MainMenu;
        }

        protected override void LoadContent()
        {
            rainManager.Load(Content);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D texturePlayer = Content.Load<Texture2D>("Images/idle");
            player = new Player(texturePlayer, 1, 4);
            playerTxtPacker = new PlayerTexturePackerTest(texturePlayer, 1, 4);
            menuManager.LoadContent(Content,this);
            #region Map initialize
            mapManager.LoadContent(Content);
            #endregion

            playerManager.LoadContent(Content, GraphicsDevice.Viewport);
            enemiesManager.LoadContent(Content);

            SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader(this.Content);
            this.spriteSheet = spriteSheetLoader.Load("CapGuyDemo.png");
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spriteRender = new SpriteRender(this.spriteBatch);
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            // graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            // graphics.ApplyChanges();
        }
        protected override void Update(GameTime gameTime)
        {
            //TEST
            rainManager.Update(gameTime);
            //TEST
            switch (CurrentGameState)
            {
                #region MainMen update
                case GameState.MainMenu:
                    playerTxtPacker.Update(gameTime);
                   
                    menuManager.Update(gameTime, GraphicsDevice);
                    menuManager.UpdateMainMenu(gameTime);
                    break;
                #endregion
                #region Playing update
                case GameState.Playing:
                    IsMouseVisible = false;

                    menuManager.UpdatePlaying();

                    playerManager.Update(gameTime, GraphicsDevice);

                    enemiesManager.Update(gameTime);
                    enemiesManager.CollisionsWithMap(mapManager.getMap());
                    
                    enemiesManager.collisionsWithPlayer(playerManager.getPlayer());

                    mapManager.Update(gameTime,playerManager);

                    player.Update(gameTime);
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

            base.Update(gameTime);
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
                    menuManager.DrawBegin(spriteBatch);
                    break;
                #endregion
                #region Pause Draw
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    menuManager.DrawPause(spriteBatch);
                    break;
                #endregion
                #region DeadMenu Draw
                case GameState.DeadMenu:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    menuManager.DrawDeadMenu(spriteBatch);
                    break;
                #endregion
                #region Playing Draw
                case GameState.Playing:
                    playerManager.Draw(spriteBatch);//to musi być pierwsze bo kamera używa begin by się dodać
                    //a to moze byc wywolane tylko raz
                    mapManager.Draw(spriteBatch);
                    enemiesManager.Draw(spriteBatch);
                    player.Draw(spriteBatch, new Vector2(200, 200));
                    //TEST
                    rainManager.Draw(spriteBatch);
                    //TEST
                    break;
                    #endregion
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        public void restart()
        {
            enemiesManager.restartEnemies();
            playerManager.restart();
            Initialize();
            LoadContent();
        }
    }
}