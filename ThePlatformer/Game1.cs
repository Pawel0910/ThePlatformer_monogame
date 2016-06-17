using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using TexturePackerLoader;

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
        MarcoPlayer marcoPlayer;
        SpriteSheet spriteSheet;
        SpriteRender spriteRender;
        enum GameState
        {
            MainMenu,
            Options,
            Playing,
            Pause,
        }
        GameState CurrentGameState = GameState.MainMenu;
        int screenWidth, screenHeight;

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
            marcoPlayer = new MarcoPlayer();
            base.Initialize();
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
            camera = new Camera(GraphicsDevice.Viewport);

            Tile.Content = Content;
            map.Generate(new int[,]
            {
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,1,2,2,2,2,0,0,0,0,0,1,0,2,2,2,0,0,0,1},
                {0,1,2,2,2,2,2,1,0,1,2,0,1,0,2,2,2,2,1,0,1},
                {1,2,2,2,2,2,2,2,1,2,1,2,2,0,2,2,2,2,1,2,1},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,0,2,2,2,2,2,2,2},
            }, 64);
            marcoPlayer.Load(Content);
            SpriteSheetLoader spriteSheetLoader = new SpriteSheetLoader(this.Content);
            this.spriteSheet = spriteSheetLoader.Load("CapGuyDemo.png");
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.spriteRender = new SpriteRender(this.spriteBatch);
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Debug.Write(graphics.PreferredBackBufferWidth);
            // graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            btnPlay = new cButton(Content.Load<Texture2D>("button"),graphics.GraphicsDevice);
            backToGameButton = new cButton(Content.Load<Texture2D>("button"), graphics.GraphicsDevice);
            exitButton = new cButton(Content.Load<Texture2D>("button"), graphics.GraphicsDevice);
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
            switch(CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                   // camera.Update(new Vector2(screenWidth/2, screenHight/2), map.Width, map.Height);
                    break;
                case GameState.Playing:
                    //if (pauseButton.isClicked == true) CurrentGameState = GameState.Pause;
                    //pauseButton.Update(mouse);
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        CurrentGameState = GameState.Pause;
                    }
                    marcoPlayer.Update(gameTime);
                    foreach (CollisionTile tile in map.CollisionTiles)
                    {
                        marcoPlayer.Collision(tile.Rectangle, map.Width, map.Height);
                        camera.Update(marcoPlayer.Position, map.Width, map.Height);

                    }

                    player.Update(gameTime);
                    playerTxtPacker.Update(gameTime);

                    break;
                case GameState.Pause:
                    if (backToGameButton.isClicked == true) CurrentGameState = GameState.Playing;
                    backToGameButton.Update(mouse);
                    if (exitButton.isClicked == true) Exit();
                    exitButton.Update(mouse);
                    break;

            }
            

            base.Update(gameTime);
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
                case GameState.MainMenu:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    Vector2 vector=getXYtoDrawMenu();
                    spriteBatch.Draw(Content.Load<Texture2D>("mainMenu"), new Rectangle((int)vector.Y,(int)vector.X, 800, 600), Color.White);
                    btnPlay.setPosition(new Vector2(330+ (int)vector.Y, 300+ (int)vector.X));
                    btnPlay.Draw(spriteBatch);
                    break;
                case GameState.Pause:
                    GraphicsDevice.Clear(Color.White);
                    spriteBatch.Begin();
                    Vector2 vector1 = getXYtoDrawMenu();
                    backToGameButton.setPosition(new Vector2(330 + (int)vector1.Y, 300 + (int)vector1.X));

                    backToGameButton.Draw(spriteBatch);
                    exitButton.setPosition(new Vector2(330 + (int)vector1.Y, 350 + (int)vector1.X));

                    exitButton.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    
                    spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                null, null, null, null,
                camera.Transform);
                    //pauseButton.setPosition(new Vector2(0, 0));
                    //pauseButton.Draw(spriteBatch);
                    this.spriteRender.Draw(
                this.spriteSheet.Sprite(TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002),
                    new Vector2(350, 530));
                    map.Draw(spriteBatch);
                    marcoPlayer.Draw(spriteBatch);
                    player.Draw(spriteBatch, new Vector2(200, 200));
                    //playerTxtPacker.DrawMoja(this.spriteRender, this.spriteSheet);
                    playerTxtPacker.Draw(spriteBatch, new Vector2(100, 100));
                    break;

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
