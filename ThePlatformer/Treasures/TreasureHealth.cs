using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ThePlatformer.Treasures
{
    public class TreasureHealth : BaseTreasureAbstract
    {
        public TreasureHealth(Vector2 position)
            : base(position)
        {
            title = "+ HP";
        }

        public override void upgrade(MarcoPlayer player)
        {
            if (!isExist)
            {
                player.giveMeHP();
                // isExist = true;//po to by player nie byl niesmiertelny :((
            }
        }
    }
}
