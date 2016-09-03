using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using ThePlatformer.Health;

namespace ThePlatformer.Treasures
{
    public class TreasureUpgrade : BaseTreasureAbstract
    {
        public TreasureUpgrade(Vector2 position) :
            base(position)
        {
        }
        public override void upgrade(MarcoPlayer player)
        {
            if (!isExist)
            {
                upgradeShooting = true;//TODO tutaj trzeba bedzie dodac szybsze strzelanie do playera
                UpgradeBar.spawnUpgradeBar = true;
                player.delayBetweenBulletShots = 100f;
            }
            base.upgrade(player);
        }
    }
}
