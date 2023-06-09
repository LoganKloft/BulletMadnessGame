﻿using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class SingleSpiralShotPattern : ShotPattern
    {

        //private TargetingShotFactory targetFactory = new TargetingShotFactory();
        private StraightShotFactory straightShotFactory = new StraightShotFactory();
        string asset;
        ShotParams _shotParams;

        public SingleSpiralShotPattern(ShotParams shotParams) : base()
        {
            this._shotParams = shotParams;
            this.asset = shotParams.asset;
        }

        public override void CreateShots(Entity entity, GameTime gameTime = null)
        {
            // (1) calculate angle of shots to shoot at player
            Point target = Player.Instance.Center;
            Point current = entity.Center;

            // v = unit vector from current to target
            Vector2 v = new Vector2(target.X - current.X, target.Y - current.Y);
            float magnitude = (float)Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
            v.X = v.X / magnitude;
            v.Y = v.Y / magnitude;

            // angle based on x-axis
            double angle = Math.Atan2(v.Y, v.X);

            (Shot shot, Sprite sprite) = ShotFactory.CreateShot(_shotParams);
            MovementPattern.MovementPattern movementPattern = straightShotFactory.CreateMovementPattern(new MovementParams{ direction = angle });
            shot.MovementPattern = movementPattern;
            DrawController.AddSprite(sprite);
            //shot.LoadContent(content, asset);
            //shot.Direction = dir;
            if (_shotParams.point == null)
            {
                shot.DestinationRectangle.X = entity.Center.X - (shot.DestinationRectangle.Width / 2);
                shot.DestinationRectangle.Y = entity.Center.Y - (shot.DestinationRectangle.Height / 2);
            }
            else
            {
                shot.DestinationRectangle.X = this.point.X;
                shot.DestinationRectangle.Y = this.point.Y;
            }

            //shot.DestinationRectangle.Width = 50;
            //shot.DestinationRectangle.Height = 50;

            //shots.Add(shot);
            //shot.BulletOffscreenEvent += BulletOffscreenHandler;
            ShotController.AddShot(shot);
            // create proper command
            if (entity is Enemy)
            {
                // then shots should collide with player
                CollisionBulletCommand command = new CollisionBulletCommand(shot, Player.Instance);
                CollisionDetector.AddCommand(command);
            }
        }
    }
}
