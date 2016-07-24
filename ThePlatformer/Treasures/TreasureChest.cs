using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Treasures
{
    public class TreasureChest : BaseTreasureAbstract
    {
        public override void upgrade(MarcoPlayer player)
        {
            if (!isExist)
            {
                player.giveMeHP();
            }
        }
    }
}
