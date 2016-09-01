using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Characters.Player;

namespace ThePlatformer.Treasures
{
    public class TreasureManager
    {
        private static List<BaseTreasureAbstract> treasureList;
        private static ContentManager Content;
        private PlayerManager playerManager;
        public TreasureManager(PlayerManager playerManager)
        {
            treasureList = new List<BaseTreasureAbstract>();
            this.playerManager = playerManager;
        }
        public void Load(ContentManager Content)
        {
            TreasureManager.Content = Content;
            //for (int i = 0; i < treasureList.Count; i++)
            //{
            //    treasureList[i].Load(Content, "arrow1");
            //}
        }
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < treasureList.Count; i++)
            {
                treasureList[i].Update(gameTime, playerManager.getPlayer());
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int i = 0; i < treasureList.Count; i++)
            {
                treasureList[i].Draw(spriteBatch);
            }
        }
        public static void addTreasure(BaseTreasureAbstract treasure)
        {
            treasure.Load(Content, "arrow1");
            treasureList.Add(treasure);
        }
    }
}
