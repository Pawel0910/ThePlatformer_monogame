using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePlatformer.Enemies;

namespace ThePlatformer
{
    public class NormalEnemy : EnemyBase
    {
        private int distanceToPlayer = 100;
        private float startTime = 0, endTime = 0;

        public override void Update(GameTime gameTime)
        {
            if (position.Y > MarcoPlayer.mapHeight - rectangle.Height)
            {
                position.X = MarcoPlayer.rectangle.X + 300;
                position.Y = -20;
            }
            //    startTime += gameTime.ElapsedGameTime.Milliseconds;
            //    if (startTime > 3500)
            //    {
            //        if (canTeleport)
            //        {
            //            position += new Vector2(100, 0);
            //            position.Y = 0;
            //        //canTeleport = false - bedzie woczas ustawiany w updatcie kolizji
            //    }
            //    startTime = 0;
            //}
            //running away before player
            if (rectangle.X - MarcoPlayer.rectangle.X < distanceToPlayer)
            {
                position += new Vector2(4, 0);
            }
            //zmiana pozycji
            position += velocity;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            //grawitacaj
            if (velocity.Y < 10)
            {
                velocity.Y += 0.4f;
            }
            //base.Update(gameTime) ;
        }
    }
}
