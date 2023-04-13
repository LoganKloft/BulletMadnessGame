using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Car_Chase_Bullet_Hell_Game.Controller;

namespace Car_Chase_Bullet_Hell_Game.View.Sprite
{
    internal class BossHealthBar : Sprite
    {
        Sprite border = new Sprite();
        Sprite healthBar = new Sprite();

        private double originalHealth;
        private double currentHealth;
        private double percentShown = 1;
        public BossHealthBar(Enemy enemy)
        {
            enemy.HealthChangedEvent += HealthChangedEventHandler;
            enemy.DestroyEvent += DestroyEventHandler;
            originalHealth = enemy.Health;
            currentHealth = enemy.Health;
            border.LoadContent(Game1.content, "HealthBar");
            healthBar.LoadContent(Game1.content, "RedHealthBar");
            border.DestinationRectangle.Height = 75;
            healthBar.DestinationRectangle.Height = 75;
            border.DestinationRectangle.Width = 400;
            healthBar.DestinationRectangle.Width = 400;
            border.DestinationRectangle.X = Game1.gd.Viewport.Width / 2 - border.DestinationRectangle.Width / 2;
            healthBar.DestinationRectangle.X = Game1.gd.Viewport.Width / 2 - healthBar.DestinationRectangle.Width / 2;
            healthBar.Color = Color.Red;
        }

        public void HealthChangedEventHandler(float health)
        {
            if (health > originalHealth)
            {
                // show entire health bar
                healthBar.DestinationRectangle = border.DestinationRectangle;
            }
            else if (health <= 0 )
            {
                // empty 
                healthBar.DestinationRectangle.Width = 0;
            }
            else
            {
                // not empty
                currentHealth = health;
                percentShown = currentHealth / originalHealth;
                int newWidth = (int)(border.DestinationRectangle.Width * percentShown);
                int widthDiff = border.DestinationRectangle.Width - newWidth;
                healthBar.DestinationRectangle.Width = (int)(border.DestinationRectangle.Width * percentShown);
                healthBar.DestinationRectangle.X = border.DestinationRectangle.X + widthDiff / 2;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            healthBar.Draw(spriteBatch, gameTime);
            border.Draw(spriteBatch, gameTime);
        }
    }
}
