﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.View.Sprite;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class CircleShotPattern : ShotPattern
    {
        List<Shot> shots = new List<Shot>();
        int _shotCount = 0;
        private float time = 0f;

        public CircleShotPattern(int shotCount) : base()
        {
            _shotCount = shotCount;
        }

        // potential to be called multiple times if bullet is both offscreen and collides with enemy at the same time
        // or if bullet collides with enemy and lifetime runs outs, etc - some design improvements to make
        private void BulletOffscreenHandler(Shot shot)
        {
            _shotCount--;
        }

        public void CreateShots(ContentManager content, string asset, Point point)
        {
            if (_shotCount == 0)
            {
                return;
            }

            double shotOffset = Math.PI * 2d / _shotCount;
            for (int i = 0; i < _shotCount; i++)
            {
                Shot shot = new Shot();
                MovementPattern.MovementPattern movementPattern = new StraightShot(shotOffset * i);
                shot.MovementPattern = movementPattern;
                Sprite shotSprite = new Sprite();
                shotSprite.LoadContent(content, asset);
                shot.DestinationRectangle = shotSprite.DestinationRectangle;
                shot.DestinationRectangleChanged += shotSprite.DestinationRectangleChangedHandler;
                shot.RotationChanged += shotSprite.RotationChangedHandler;
                shot.OriginChanged += shotSprite.OriginChangedHandler;
                DrawController.AddSprite(shotSprite);

                shot.DestinationRectangle.X = point.X;
                shot.DestinationRectangle.Y = point.Y;
                shot.NotifyOfDestinationRectangleChange();
                shots.Add(shot);
                shot.BulletOffscreenEvent += BulletOffscreenHandler;
            }
        }

        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Shot shot in shots)
            {
                shot.Update(gameTime);
            }
        }

        public override bool Finished()
        {
            if (_shotCount <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
