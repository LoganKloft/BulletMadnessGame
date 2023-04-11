﻿using System;
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
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
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
        private ShotParams shotParams;

        private CircleShotPatternFactory circleShotPatternFactory = new CircleShotPatternFactory();
        private HalfCircleShotPatternFactory halfCircleShotPatternFactory = new HalfCircleShotPatternFactory();
        private StraightShotPatternFactory straightShotPatternFactory = new StraightShotPatternFactory();
        private ShootPlayerShotPatternFactory shootPlayerShotPatternFactory = new ShootPlayerShotPatternFactory();

        public ShotItem(SpawnItem spawnItem, ShotParams shotParams)
        {
            this.shotParams = shotParams;
            this.start = shotParams.start;
            this.duration = shotParams.duration;
            this.shootSpeed = timer = shotParams.shootSpeed;
            //this.type = type;
            //this.asset = asset;
            this.spawnItem = spawnItem;
            //this.shotCount = shotCount;

            switch (shotParams.shotPattern)
            {
                case "CircleShotPattern":
                    this.shotPattern = circleShotPatternFactory.CreateShotPattern(shotParams);
                    break;
                case "HalfCircleShotPattern":
                    this.shotPattern = halfCircleShotPatternFactory.CreateShotPattern(shotParams);
                    break;
                case "ShootPlayerShotPattern":
                    this.shotPattern = shootPlayerShotPatternFactory.CreateShotPattern(shotParams);
                    break;
                case "StraightShotPattern":
                    this.shotPattern = straightShotPatternFactory.CreateShotPattern(shotParams);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (active == false)
            {
                start -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (Player.Instance.IsInvincible == false)
                {
                    if (start <= 0f)
                    {
                        shotPattern.point = spawnItem.enemy.Center;
                        shotPattern.CreateShots(spawnItem.enemy);
                        active = true;
                    }
                }
            }

            if (active)
            {
                duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (Player.Instance.IsInvincible==false)
                {
                    timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (duration <= 0f)
                    {
                        spawnItem.shotItems.Remove(this);
                    }

                    if (timer <= 0f)
                    {
                        timer = shootSpeed;
                        shotPattern.point = spawnItem.enemy.Center;
                        shotPattern.CreateShots(spawnItem.enemy);
                    }
                }
                
            }
        }
    }
}
