﻿using Microsoft.Xna.Framework;
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
using ThePlatformer.Characters.Enemies.EnemiesManager;

namespace ThePlatformer
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private EnemiesManager enemiesManager = new EnemiesManager();
        private Texture2D background;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        MapManager mapManager = new MapManager();
        private Player player;
        private PlayerTexturePackerTest playerTxtPacker;
        private MainMenu mainMenu;
        MarcoPlayer marcoPlayer;
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
        
        protected override void Initialize()
        {
            mapManager.Initialize();
            treasureChest = new TreasureChest();
            enemiesManager.Initialize();
            base.Initialize();
            CurrentGameState = GameState.MainMenu;
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D texturePlayer = Content.Load<Texture2D>("Images/idle");
            player = new Player(texturePlayer, 1, 4);
            playerTxtPacker = new PlayerTexturePackerTest(texturePlayer, 1, 4);
            mainMenu = new MainMenu();
            camera = new Camera(GraphicsDevice.Viewport);
            #region Map initialize
            mapManager.LoadContent(Content);
            #endregion
           
            marcoPlayer = new MarcoPlayer(mapManager.getMapWidth(),mapManager.getMapHeight());
            marcoPlayer.Load(Content);
            enemiesManager.LoadContent(Content);

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
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
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
                    enemiesManager.Update(gameTime);
                    enemiesManager.CollisionsWithMap(mapManager.getMap());

                    foreach (CollisionTile tile in mapManager.getMap().CollisionTiles)
                    {
                        marcoPlayer.Collision(tile.Rectangle, mapManager.getMapWidth(), mapManager.getMapHeight());
                       
                        treasureChest.CollisionMap(tile.Rectangle, mapManager.getMapWidth(), mapManager.getMapHeight());
                        camera.Update(marcoPlayer.Position, mapManager.getMapWidth(), mapManager.getMapHeight());
                    }
                        treasureChest.Update(gameTime,marcoPlayer);

                    player.Update(gameTime);
                    enemiesManager.collisionsWithPlayer(marcoPlayer);
                    #region Bullet collisiona with Player
                    
                   // marcoPlayer.allCollisionsWithEnemies(enemy);!!!!!!!!!!!!!!!!!!!!!!!!!


                    #endregion
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
                        enemiesManager.restartEnemies();
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
                    treasureChest.Draw(spriteBatch);
                    mapManager.Draw(spriteBatch);
                    marcoPlayer.Draw(spriteBatch);
                    enemiesManager.Draw(spriteBatch);
                    player.Draw(spriteBatch, new Vector2(200, 200));
                    break;
                    #endregion
            }
            
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
