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

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class ShotItem
    {
        public float start;
        public float duration;
        public float shootSpeed;
        public float timer;
        public string type;
        public string asset;
        int shotCount;
        public SpawnItem spawnItem;
        private ShotPattern.ShotPattern shotPattern;
        private bool active = false;

        public ShotItem(SpawnItem spawnItem, float start, float duration, float shootSpeed, ShotPattern.ShotPattern shotPattern)
        {
            this.start = start;
            this.duration = duration;
            this.shootSpeed = this.timer = shootSpeed;
            //this.type = type;
            //this.asset = asset;
            this.spawnItem = spawnItem;
            //this.shotCount = shotCount;
            this.shotPattern = shotPattern;
        }

        public void Update(GameTime gameTime)
        {
            duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (active == false)
            {
                start -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (start <= 0f)
                {
                    //spawnItem.enemy.ShotPatterns.Enqueue(ShotPattern.ShotPattern.Parse(type, asset, spawnItem.enemy));
                    shotPattern.point = spawnItem.enemy.Center;
                    shotPattern.CreateShots(spawnItem.enemy);
                    //ShotPattern.ShotPattern.Parse(type, asset, spawnItem.enemy.Center, shotCount, spawnItem.enemy).CreateShots(spawnItem.enemy);
                    active = true;
                }
            }

            if (active)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (duration <= 0f)
                {
                    spawnItem.shotItems.Remove(this);
                }

                if (timer <= 0f)
                {
                    timer = shootSpeed;
                    //spawnItem.enemy.ShotPatterns.Enqueue(ShotPattern.ShotPattern.Parse(type, asset, spawnItem.enemy));
                    // ShotPattern.ShotPattern.Parse(type, asset, spawnItem.enemy.Center, shotCount, spawnItem.enemy).CreateShots(spawnItem.enemy);
                    shotPattern.point = spawnItem.enemy.Center;
                    shotPattern.CreateShots(spawnItem.enemy);
                }
            }
        }
    }
}
