﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Characters.Player
{
    class PlayerManager
    {

        MarcoPlayer marcoPlayer;
        MapManager mapManager = MapManager.getInstance();
        public void LoadContent(ContentManager Content)
        {
            marcoPlayer = new MarcoPlayer(mapManager.getMapWidth(), mapManager.getMapHeight());
            marcoPlayer.Load(Content);
        }

        public void Update(GameTime gameTime, GraphicsDevice graphics)
        {
            marcoPlayer.Update(gameTime, graphics);
        }

        public void collisionWithMap()
        {
            foreach (CollisionTile tile in mapManager.getMap().CollisionTiles)
            {
                marcoPlayer.Collision(tile.Rectangle, mapManager.getMapWidth(), mapManager.getMapHeight());
            }
        }
        public MarcoPlayer getPlayer()
        {
            return marcoPlayer;
        }
        public void restart()
        {
            marcoPlayer = null;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            marcoPlayer.Draw(spriteBatch);
        }
    }
}
