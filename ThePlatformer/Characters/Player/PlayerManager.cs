using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Characters.Player
{
    public class PlayerManager
    {
        private Camera camera;
        private MarcoPlayer marcoPlayer;
        private MapManager mapManager = MapManager.getInstance();
     
        public void Initialize()
        {
            marcoPlayer = new MarcoPlayer();

        }
        public void LoadContent(ContentManager Content,Viewport viewport)
        {
            camera = new Camera(viewport);
            marcoPlayer.mapHeight = mapManager.getMapHeight();
            marcoPlayer.mapWidth = mapManager.getMapWidth();
            marcoPlayer.Load(Content);

        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            marcoPlayer.Update(gameTime, graphics);
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
                       camera.Transform);
            marcoPlayer.Draw(spriteBatch);

        }
    }
}
