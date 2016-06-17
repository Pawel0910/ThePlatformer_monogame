using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        }
        GameState CurrentGameState = GameState.MainMenu;
        int screenWidth = 800, screenHight = 600;

        cButton btnPlay;
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

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHight;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            IsMouseVisible = true;

            btnPlay = new cButton(Content.Load<Texture2D>("button"),graphics.GraphicsDevice);
            btnPlay.setPosition(new Vector2(350, 300));
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
            marcoPlayer.Update(gameTime);
            switch(CurrentGameState)
            {
                case GameState.MainMenu:
                    if (btnPlay.isClicked == true) CurrentGameState = GameState.Playing;
                    btnPlay.Update(mouse);
                   // camera.Update(new Vector2(screenWidth/2, screenHight/2), map.Width, map.Height);
                    break;
                case GameState.Playing:
                    foreach (CollisionTile tile in map.CollisionTiles)
                    {
                        marcoPlayer.Collision(tile.Rectangle, map.Width, map.Height);
                      //  camera.Update(marcoPlayer.Position, map.Width, map.Height);

                    }

                    player.Update(gameTime);
                    playerTxtPacker.Update(gameTime);

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
            spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Deferred,
            //    BlendState.AlphaBlend,
            //    null,null,null,null,
            //    camera.Transform);
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    map.Draw(spriteBatch);
                    spriteBatch.Draw(Content.Load<Texture2D>("mainMenu"), new Rectangle(0, 0, screenWidth, screenHight), Color.White);
                    btnPlay.Draw(spriteBatch);
                    break;
                case GameState.Playing:
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
            
            // spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
