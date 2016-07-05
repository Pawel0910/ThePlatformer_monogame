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
    class HealthBar
    {
        private Texture2D container, lifeBar;
        private Vector2 position;
        private int fullHealth;
        private int currentHealth;
        public HealthBar(ContentManager content)
        {
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
            double amountToChange = ((double)percentAmountToChangeHeal / fullHealth)*100;
            if (currentHealth > 0)
            {
                double percent = (double)currentHealth / fullHealth*100;
                currentHealth = (int)(percent-amountToChange);
                currentHealth = (int)(((double)currentHealth * fullHealth)/100);
            }
        }
        public void restartHealthBar()
        {
            currentHealth = fullHealth;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lifeBar, position, new Rectangle((int)position.X,(int)position.Y,currentHealth,lifeBar.Height),
                Color.LightGreen);
            spriteBatch.Draw(container, position, Color.White);
        }
    }
}
