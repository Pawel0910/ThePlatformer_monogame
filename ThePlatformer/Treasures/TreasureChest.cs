using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ThePlatformer.Treasures
{
    public class TreasureChest : BaseTreasureAbstract
    {
        public TreasureChest(Vector2 position)
            : base(position)
        {
        }

        public override void upgrade(MarcoPlayer player)
        {
            if (!isExist)
            {
                player.giveMeHP();
                isExist = true;//po to by player nie byl niesmiertelny :((
            }
        }
    }
}
