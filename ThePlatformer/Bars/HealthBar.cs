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
    public class HealthBar
    {
        private Texture2D container, lifeBar;
        private Vector2 position;
        public int fullHealth;
        public int currentHealth;
        private Color healthBarColor = Color.Green;

        public HealthBar(ContentManager content)
        {
            LoadContent(content);
            fullHealth = lifeBar.Width;
            currentHealth = fullHealth;
        }
        public HealthBar(ContentManager content, Vector2 position)
        {
            this.position = position;
            LoadContent(content);
            fullHealth = lifeBar.Width;
            currentHealth = fullHealth;
        }
        private void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>("healthContainer");
            lifeBar = content.Load<Texture2D>("healthBar");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">Pozycja na ekranie zycia</param>
        /// <param name="amountToChangeHeal"> to jest ta ilosc która jest jakby krokiem do zmiany życia na pasku, wyrażona w%</param>
        public void Update(Vector2 position)
        {
            this.position = position;

        }
        public void updateHealthStatus(int percentAmountToChangeHeal)
        {
            double amountToChange = ((double)percentAmountToChangeHeal / fullHealth) * 100;
            if (currentHealth > 0)
            {
                double percent = (double)currentHealth / fullHealth * 100;
                currentHealth = (int)(percent - amountToChange);
                currentHealth = (int)(((double)currentHealth * fullHealth) / 100);
            }
            colorSwitcher();
        }
        private void colorSwitcher()
        {
            int lifeInPercent = (int)((double)currentHealth / fullHealth * 100);
            if (lifeInPercent > 80)
                healthBarColor = Color.Green;
            else if (lifeInPercent > 50)
                healthBarColor = Color.Yellow;
            else if (lifeInPercent > 30)
                healthBarColor = Color.Orange;
            else if (lifeInPercent >= 20)
                healthBarColor = Color.Red;
        }
        public void restartHealthBar()
        {
            currentHealth = fullHealth;
            healthBarColor = Color.Green;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(lifeBar, position, new Rectangle((int)position.X, (int)position.Y, currentHealth, lifeBar.Height),
                healthBarColor);
            spriteBatch.Draw(container, position, Color.White);
        }
        public void Draw(SpriteBatch spriteBatch, float scale)
        {
            spriteBatch.Draw(lifeBar, position, new Rectangle((int)position.X, (int)position.Y, currentHealth, lifeBar.Height),
               healthBarColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(container, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
