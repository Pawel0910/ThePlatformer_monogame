using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Treasures
{
    public class TreasureTime : BaseTreasureAbstract
    {
        public TreasureTime(Vector2 position) :
            base(position)
        {
            title = "+ 10sec";
        }
        public override void upgrade(MarcoPlayer player)
        {
            if (!isExist)
            {
                Game1.EndTime += 10000;
            }
            base.upgrade(player);
        }
    }
}
