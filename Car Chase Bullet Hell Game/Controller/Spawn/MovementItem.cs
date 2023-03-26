using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
{
    internal class MovementItem
    {
        public float duration;
        public MovementPattern.MovementPattern movementPattern;
        public SpawnItem spawnItem;
        private bool active = false;

        public MovementItem(float duration, MovementPattern.MovementPattern movementPattern, SpawnItem spawnItem)
        {
            this.duration = duration;
            this.spawnItem = spawnItem;
            this.movementPattern = movementPattern;
        }

        public void Update(GameTime gameTime)
        {
            duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (active == false)
            {
                spawnItem.enemy.MovementPattern = movementPattern;
                active = true;
            }

            if (duration <= 0f)
            {
                spawnItem.enemy.MovementPattern = null;
                spawnItem.movementItems.Remove(this);
            }
        }
    }
}
