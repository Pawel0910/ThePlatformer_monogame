using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePlatformer.Health
{
    public class UpgradeBar
    {
        private Texture2D container, upgradeBar;
        public int fullHealth;
        public int currentHealth;
        private Color healthBarColor = Color.Blue;
        private long upgradeTime = 5000, delay, elapsed, smallerElapsed;
        private int onePercent;
        private float scale;
        private float afterDivide;
        private MarcoPlayer player;
        public static bool spawnUpgradeBar = false;
        
        public UpgradeBar(ContentManager content, MarcoPlayer player, float scale = 1)
        {
            this.player = player;
            this.scale = scale;
            LoadContent(content);
            fullHealth = upgradeBar.Width;
            currentHealth = fullHealth;
            afterDivide = fullHealth / upgradeTime;
            countDelayBeetwenDistract();
            countOnePercent();
        }
        private void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>("healthContainer");
            upgradeBar = content.Load<Texture2D>("healthBar");
            
        }
        private void countOnePercent()
        {
            onePercent = fullHealth / 100;
        }
        private void countDelayBeetwenDistract()
        {
            delay = upgradeTime / 100;
        }
        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;
            smallerElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsed > upgradeTime)
            {
                elapsed = 0;
                currentHealth = fullHealth;
                UpgradeBar.spawnUpgradeBar = false;
                player.delayBetweenBulletShots = 600f;
            }
            else
            {
                if (smallerElapsed > delay)
                {
                    smallerElapsed = 0;
                    currentHealth -= onePercent;
                }
            }
            
            //if (currentHealth > 0)
            //{
            //    currentHealth -= 1;

            //}else
            //{
            //    currentHealth = fullHealth;
            //    UpgradeBar.spawnUpgradeBar = false;
            //    player.delayBetweenBulletShots = 750f;

            //}
            // }
            updatePosition();
        }
        private void updatePosition()
        {
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (spawnUpgradeBar)
            {
            spriteBatch.Draw(upgradeBar, position, new Rectangle((int)position.X, (int)position.Y, currentHealth, upgradeBar.Height),
                healthBarColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(container, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            }
        }
    }
}
