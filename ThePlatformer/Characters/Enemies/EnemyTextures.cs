using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Enemies;
using ThePlatformer.SpriteBase.Animation;

namespace ThePlatformer.Characters.Enemies
{
    public static class EnemyTextures
    {
        public static Texture2D idle;
        public static IAnimation shootingAnimation;
        public static void Load(ContentManager Content)
        {
            idle = Content.Load<Texture2D>("idle3");
            //shootingAnimation = new AnimationImpl(200, new ShootingEnemy(), "marco");
            // shootingAnimation.LoadConent(Content);
            //parachuter=Content.Load(Texture2D)()
        }
    }
}
