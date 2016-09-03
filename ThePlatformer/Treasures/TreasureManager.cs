using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ThePlatformer.Characters.Player;
using ThePlatformer.Health;

namespace ThePlatformer.Treasures
{
    public class TreasureManager
    {
        private static List<BaseTreasureAbstract> treasureList;
        private static ContentManager Content;
        private PlayerManager playerManager;
        private MapManager map = MapManager.getInstance();
        private UpgradeBar upgradeBar;
        private GraphicsDevice graphics;
        public TreasureManager(PlayerManager playerManager)
        {
            treasureList = new List<BaseTreasureAbstract>();
            this.playerManager = playerManager;
        }
        public void Load(ContentManager Content, GraphicsDevice graphics)
        {
            this.graphics = graphics;
            TreasureManager.Content = Content;
            upgradeBar = new UpgradeBar(Content,playerManager.getPlayer(), 0.65f);

            //for (int i = 0; i < treasureList.Count; i++)
            //{
            //    treasureList[i].Load(Content, "arrow1");
            //}
        }
        public void Update(GameTime gameTime)
        {
            upgradeShooting(gameTime);
            deleteCollectedTreasures(gameTime);
            for (int i = 0; i < treasureList.Count; i++)
            {
                treasureList[i].Update(gameTime, playerManager.getPlayer());
                foreach (CollisionTile tile in map.getMap().CollisionTiles)
                {
                    treasureList[i].CollisionMap(tile.Rectangle, map.getMap().Width, map.getMap().Height);
                }
            }
        }
        public void upgradeShooting(GameTime gameTime)
        {
            if (UpgradeBar.spawnUpgradeBar)
            {
                upgradeBar.Update(gameTime);
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < treasureList.Count; i++)
            {
                treasureList[i].Draw(spriteBatch);
            }
            if (UpgradeBar.spawnUpgradeBar)
            {
                upgradeBar.Draw(spriteBatch, setRightCornerFontPosition(200, 70));
            }
        }
        private Vector2 setRightCornerFontPosition(int shiftX, int shiftY)
        {
            return new Vector2(playerManager.getPlayer()._position.X + graphics.Viewport.Width / 2 - shiftX,
                playerManager.getPlayer()._position.Y - graphics.Viewport.Height / 2 + shiftY);
        }
        public static void addTreasure(BaseTreasureAbstract treasure)
        {
            treasure.Load(Content, "arrow1");
            treasureList.Add(treasure);
        }
        private void deleteCollectedTreasures(GameTime gameTime)
        {
            for (int i = 0; i < treasureList.Count; i++)
            {
                if (!treasureList[i].isExist)
                {
                    treasureList.RemoveAt(i);
                }
            }
        }
    }
}
