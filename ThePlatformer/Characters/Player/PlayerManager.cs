using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.View.Background;

namespace ThePlatformer.Characters.Player
{
    public class PlayerManager
    {
        private Camera camera;
        private MarcoPlayer marcoPlayer;
        private MapManager mapManager = MapManager.getInstance();
        private BackgroundManager background;

        public void Initialize()
        {
            marcoPlayer = new MarcoPlayer(new Vector2(16, 38));
            background = new BackgroundManager(marcoPlayer);

        }
        public void LoadContent(ContentManager Content, Viewport viewport, GraphicsDevice graphicsDevice)
        {
            camera = new Camera(viewport);
            marcoPlayer.mapHeight = mapManager.getMapHeight();
            marcoPlayer.mapWidth = mapManager.getMapWidth();
            marcoPlayer.Load(Content, graphicsDevice);
            background.LoadContent(Content,
            graphicsDevice.Viewport.Width,
                graphicsDevice.Viewport.Height);

        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            marcoPlayer.Update(gameTime, graphics);
            background.Update(gameTime, marcoPlayer._position);
            collisionWithMap();
        }

        public void collisionWithMap()
        {
            foreach (CollisionTile tile in mapManager.getMap().CollisionTiles)
            {
                marcoPlayer.Collision(tile.Rectangle, mapManager.getMapWidth(), mapManager.getMapHeight());
                camera.Update(marcoPlayer.Position, mapManager.getMapWidth(), mapManager.getMapHeight());
            }
        }
        public MarcoPlayer getPlayer()
        {
            return marcoPlayer;
        }
        public void restart()
        {
            marcoPlayer = null;
            camera = null;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred,
                       BlendState.AlphaBlend,
                       null, null, null, null,
                       camera.get_transformation());
            background.Draw(spriteBatch);

            marcoPlayer.Draw(spriteBatch);

        }
    }
}
