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
        private long upgradeTime = 3000, elapsed, elapsedToMinus;
        private float scale;
        private float afterDivide;
        public static bool spawnUpgradeBar = false;
        public UpgradeBar(ContentManager content, float scale = 1)
        {
            this.scale = scale;
            LoadContent(content);
            fullHealth = upgradeBar.Width;
            currentHealth = fullHealth;
            afterDivide = fullHealth / upgradeTime;
        }
        private void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>("healthContainer");
            upgradeBar = content.Load<Texture2D>("healthBar");
        }
        public void Update(GameTime gameTime)
        {
            //elapsed += gameTime.ElapsedGameTime.Milliseconds;
            //if (elapsed > upgradeTime)
            //{
            //    elapsed = 0;
            //    currentHealth = fullHealth;
            //    UpgradeBar.spawnUpgradeBar = false;
            //}
            //else
            //{
                if (currentHealth > 0)
                {
                    currentHealth -= 2;

            }else
            {
                currentHealth = fullHealth;
                UpgradeBar.spawnUpgradeBar = false;
                //    //downgrade strzelania playera

            }
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
