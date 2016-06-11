using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Player player;
        MarcoPlayer marcoPlayer;
        SpriteSheet spriteSheet;
        SpriteRender spriteRender;
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
            marcoPlayer.Update(gameTime);
            foreach (CollisionTile tile in map.CollisionTiles)
            {
                marcoPlayer.Collision(tile.Rectangle, map.Width, map.Height);
            }
            player.Update(gameTime);
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
            this.spriteRender.Draw(
            this.spriteSheet.Sprite(
                TexturePackerMonoGameDefinitions.CapGuyDemo.Capguy_turn_0002
            ),
            new Vector2(350, 530)
        );
            map.Draw(spriteBatch);
            marcoPlayer.Draw(spriteBatch);
            player.Draw(spriteBatch, new Vector2(200, 200));
            // spriteBatch.Draw(background, new Rectangle(0, 0, 800, 480), Color.White);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
