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
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
{
    internal class ShotItem
    {
        public float start = 0;
        public float duration = 0;
        public float shootSpeed = 0;
        public float timer = 0;
        public float pause = 0;
        public float completePause = 0;
        public bool hasPause = false;
        public bool isPaused = false;
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
        private SingleSpiralShotFactory singleSpiralShotFactory = new SingleSpiralShotFactory();
        private TripleSpiralShotPatternFactory tripleSpiralShotPatternFactory = new TripleSpiralShotPatternFactory();

        public ShotItem(SpawnItem spawnItem, ShotParams shotParams)
        {
            this.shotParams = shotParams;
            this.start = shotParams.start != null ? (float)shotParams.start : start;
            this.duration = shotParams.duration != null ? (float)shotParams.duration : duration;
            this.shootSpeed = timer = shotParams.shootSpeed != null ? (float)shotParams.shootSpeed : timer;
            if(shotParams.pause!=null)
            {
                this.hasPause = true;
                this.pause = this.completePause = (float)shotParams.pause;
            }
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
                case "SingleSpiralShotPattern":
                    this.shotPattern = singleSpiralShotFactory.CreateShotPattern(shotParams);
                    break;
                case "TripleSpiralShotPattern":
                    this.shotPattern = tripleSpiralShotPatternFactory.CreateShotPattern(shotParams);
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
                        if(hasPause)
                        {
                            pause -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                        }
                        //shotPattern.point = spawnItem.enemy.Center;
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
                        if (this.isPaused)
                        {
                            this.pause = this.completePause;
                            this.isPaused = false;
                        }
                        timer = shootSpeed;
                        if (hasPause && !isPaused)
                        {
                            pause -= (float)gameTime.ElapsedGameTime.TotalSeconds*5;
                            if(pause <= 0f && !this.isPaused)
                            {
                                timer = 2;
                                this.isPaused = true;
                            }
                        }
                        shotPattern.CreateShots(spawnItem.enemy);
                    }
                }
                
            }
        }
    }
}
