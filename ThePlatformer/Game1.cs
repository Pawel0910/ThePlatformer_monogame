using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;
using TexturePackerLoader;
using ThePlatformer.Enemies;
using ThePlatformer.Health;
using ThePlatformer.View.Menu;
using System.Collections.Generic;
using ThePlatformer.Treasures;

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
        Map map;
        Camera camera;
        private Player player;
        private PlayerTexturePackerTest playerTxtPacker;
        private MainMenu mainMenu;
        MarcoPlayer marcoPlayer;
        public List<EnemyBase> enemiesList = new List<EnemyBase>();
        private BaseTreasureAbstract treasureChest;
        SpriteSheet spriteSheet;
        SpriteRender spriteRender;
        bool pause = false;
        int screenWidth, screenHeight;
        SpriteFont font;
        int score;
        
        public enum GameState
        {
            MainMenu,
            Options,
            Playing,
            Pause,
            DeadMenu
        }
        public static GameState CurrentGameState;

        cButton btnPlay;
        cButton backToGameButton,exitButton;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            map = new Map();
           // normalEnemy = new NormalEnemy();
           // mojEnemy = new ShootingEnemy();
           // moj1Enemy = new ShootingEnemy();
            enemiesList.Add(new NormalEnemy());
            enemiesList.Add(new ShootingEnemy());
            treasureChest = new TreasureChest();
            base.Initialize();
            CurrentGameState = GameState.MainMenu;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader(this.Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D texturePlayer = Content.Load<Texture2D>("Images/idle");
            player = new Player(texturePlayer, 1, 4);
            playerTxtPacker = new PlayerTexturePackerTest(texturePlayer, 1, 4);
            mainMenu = new MainMenu();
           // mainMenu = new MainMenu(Content.Load<Texture2D>("menu/"));
            camera = new Camera(GraphicsDevice.Viewport);
            #region Map initialize
            Tile.Content = Content;
            map.Generate(new int[,]
            {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,1,1,1,1,1,1,0,0,0,0,0,1,0,1,1,1,0,0,0,1},
                {2,2,2,2,2,2,2,1,0,1,1,1,2,0,2,2,2,1,1,0,2},
                {2,2,2,2,2,2,2,2,1,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
            }, 80);
            #endregion
            marcoPlayer = new MarcoPlayer(map.Width,map.Height);
            marcoPlayer.Load(Content);
           // normalEnemy.Load(Content,"idle2", new Vector2(60, 10));
            //mojEnemy.Load(Content, "idle1", new Vector2(120, 10));
            //moj1Enemy.Load(Content, "idle1", new Vector2(200, 10));
            foreach(EnemyBase enemy in enemiesList)
            {
                enemy.Load(Content, "idle3", new Vector2(150, 10));
            }
            treasureChest.Load(Content, "idle2");
            SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader(this.Content);
            this.spriteSheet = spriteSheetLoader.Load("CapGuyDemo.png");
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spriteRender = new SpriteRender(this.spriteBatch);
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Debug.Write(graphics.PreferredBackBufferWidth);
            // graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
           // graphics.ApplyChanges();
           // IsMouseVisible = true;

            btnPlay = new cButton(Content.Load<Texture2D>("button"),graphics.GraphicsDevice);
            backToGameButton = new cButton(Content.Load<Texture2D>("button"), graphics.GraphicsDevice);
            exitButton = new cButton(Content.Load<Texture2D>("button"), graphics.GraphicsDevice);

            font = Content.Load<SpriteFont>("healthsFont");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();

            switch (CurrentGameState)
            {
                #region MainMen update
                case GameState.MainMenu:
                    playerTxtPacker.Update(gameTime);

                    IsMouseVisible = true;
                    var touchPanelState = TouchPanel.GetState();
                    if (touchPanelState.Count >= 1)
                    {
                        CurrentGameState = GameState.Playing;

                    }
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                    mainMenu.Update(gameTime);
                    // camera.Update(new Vector2(screenWidth/2, screenHight/2), map.Width, map.Height);
                    break;
#endregion
                #region Playing update
                case GameState.Playing:
                    IsMouseVisible = false;
                    Mouse.SetPosition(0, 0);
                    if (Keyboard.GetState().IsKeyDown(Keys.P)&&pause==false)
                    {
                        pause = true;
                        CurrentGameState = GameState.Pause;
                    }
                    marcoPlayer.Update(gameTime,GraphicsDevice);
                    //marcoPlayer.isCrossedMap(map.Width, map.Height);
                   // normalEnemy.Update(gameTime);
                   // mojEnemy.Update(gameTime);
                   // moj1Enemy.Update(gameTime);
                    foreach(EnemyBase enemy in enemiesList)
                    {
                        enemy.Update(gameTime);
                    }
                    foreach (CollisionTile tile in map.CollisionTiles)
                    {
                        marcoPlayer.Collision(tile.Rectangle, map.Width, map.Height);
                       // normalEnemy.CollisionMap(tile.Rectangle, map.Width, map.Height);
                      //  mojEnemy.CollisionMap(tile.Rectangle, map.Width, map.Height);
                       // moj1Enemy.CollisionMap(tile.Rectangle, map.Width, map.Height);
                        foreach (EnemyBase enemy in enemiesList)
                        {
                            enemy.CollisionMap(tile.Rectangle, map.Width, map.Height);
                        }
                        treasureChest.CollisionMap(tile.Rectangle, map.Width, map.Height);
                        camera.Update(marcoPlayer.Position, map.Width, map.Height);
                    }
                        treasureChest.Update(gameTime);

                    player.Update(gameTime);
                    #region Bullet collisiona with Player
                   // mojEnemy.allCollisionWithPlayer(marcoPlayer);
                   // moj1Enemy.allCollisionWithPlayer(marcoPlayer);
                    foreach (EnemyBase enemy in enemiesList)
                    {
                        enemy.allCollisionWithPlayer(marcoPlayer);
                        marcoPlayer.allCollisionsWithEnemies(enemy);
                    }
                    // marcoPlayer.allCollisionsWithEnemies(mojEnemy);

                    #endregion
                    deleteDeadEnemiesFromGame();
                    break;
#endregion
                #region Pause update
                case GameState.Pause:
                    IsMouseVisible = true;
                    if (backToGameButton.isClicked == true)
                    {
                        pause = false;
                        CurrentGameState = GameState.Playing;
                    } 
                    backToGameButton.Update(mouse);
                    if (exitButton.isClicked == true) Exit();
                    exitButton.Update(mouse);
                    break;
                #endregion
                #region DeadMenu update
                case GameState.DeadMenu:
                    IsMouseVisible = true;
                    if (backToGameButton.isClicked == true)
                    {
                        enemiesList.Clear();
                        marcoPlayer = null;
                        camera = null;
                        Initialize();
                        LoadContent();
                    }
                    backToGameButton.Update(mouse);
                    if (exitButton.isClicked == true) Exit();
                    exitButton.Update(mouse);
                    break;
                    #endregion
            }

            base.Update(gameTime);
        }
        private void deleteDeadEnemiesFromGame()
        {
            for(int i = 0; i < enemiesList.Count; i++)
            {
                if (enemiesList[i].isDead)
                {
                    enemiesList.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            switch (CurrentGameState)
            {
                #region MainMenu Draw
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    Vector2 vector=getXYtoDrawMenu();

                    // mainMenu.Draw(this.spriteRender, this.spriteSheet);
                    spriteBatch.Draw(Content.Load<Texture2D>("mainMenu"), new Rectangle((int)vector.Y,(int)vector.X, 800, 600), Color.White);
                    //playerTxtPacker.DrawMoja(this.spriteRender, this.spriteSheet);

                    btnPlay.setPosition(new Vector2(330+ (int)vector.Y, 300+ (int)vector.X));
                    btnPlay.Draw(spriteBatch);
                    //this.spriteSheet.Sprite(PlayerAnimationLists.drawMenuStart(), new Vector2(200, 200));
                    break;
                #endregion
                #region Pause Draw
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    Vector2 vector1 = getXYtoDrawMenu();
                    backToGameButton.setPosition(new Vector2(330 + (int)vector1.Y, 300 + (int)vector1.X));

                    backToGameButton.Draw(spriteBatch);
                    exitButton.setPosition(new Vector2(330 + (int)vector1.Y, 350 + (int)vector1.X));

                    exitButton.Draw(spriteBatch);
                    break;
                #endregion
                #region DeadMenu Draw
                case GameState.DeadMenu:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    Vector2 vector2 = getXYtoDrawMenu();
                    backToGameButton.setPosition(new Vector2(330 + (int)vector2.Y, 300 + (int)vector2.X));

                    backToGameButton.Draw(spriteBatch);
                    exitButton.setPosition(new Vector2(330 + (int)vector2.Y, 350 + (int)vector2.X));

                    exitButton.Draw(spriteBatch);
                    break;
                #endregion
                #region Playing Draw
                case GameState.Playing:
                    
                    spriteBatch.Begin(SpriteSortMode.Deferred,
                       BlendState.AlphaBlend,
                       null, null, null, null,
                       camera.Transform);
                    //pauseButton.setPosition(new Vector2(0, 0));
                    //pauseButton.Draw(spriteBatch);
                    //this.spriteRender.Draw(
                    // this.spriteSheet.Sprite(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002),
                    //    new Vector2(350, 530));
                    treasureChest.Draw(spriteBatch);
                    map.Draw(spriteBatch);
                    marcoPlayer.Draw(spriteBatch);
                    //normalEnemy.Draw(spriteBatch);
                   // mojEnemy.Draw(spriteBatch);
                   // moj1Enemy.Draw(spriteBatch);
                    foreach (EnemyBase enemy in enemiesList)
                    {
                        enemy.Draw(spriteBatch);
                    }
                    player.Draw(spriteBatch, new Vector2(200, 200));
                   // spriteBatch.DrawString(font, "Score: " + score, new Vector2(50, 50), Color.Black);
                   // playerTxtPacker.DrawMoja(spriteBatch, new Vector2(100, 100));
                    break;
                    #endregion
            }

            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected Vector2 getXYtoDrawMenu()
        {
            
            int a = GraphicsDevice.Viewport.Height;
            screenHeight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;
            Vector2 vector = new Vector2();
            if (screenHeight > 600)
            {
                vector.X = (screenHeight - 600) / 2;
            }
            else vector.X = 0;
            if (screenWidth > 800)
            {
                vector.Y = (screenWidth - 800) / 2;
            }
            else
                vector.Y = 0;
            return vector;
        }
    }
}
