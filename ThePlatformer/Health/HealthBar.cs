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
        private int currentHealth=80;
        public HealthBar(ContentManager content)
        {
            position = new Vector2(100, 100);
            LoadContent(content);
            fullHealth = lifeBar.Width;
        }
        private void LoadContent(ContentManager content)
        {
            container = content.Load<Texture2D>("healthContainer");
            lifeBar = content.Load<Texture2D>("healthBar"); 
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lifeBar, position, new Rectangle((int)position.X,(int)position.Y,currentHealth,lifeBar.Height),
                Color.White);
            spriteBatch.Draw(container, position, Color.White);
        }
    }
}
