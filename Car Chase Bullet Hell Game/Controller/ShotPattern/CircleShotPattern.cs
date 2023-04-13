using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class CircleShotPattern : ShotPattern
    {
        int _shotCount = 0;
        private StraightShotFactory factory = new StraightShotFactory();
        string asset;
        ShotParams _shotParams;

        public CircleShotPattern(ShotParams shotParams) : base()
        {
            _shotParams = shotParams;
            _shotCount = shotParams.shotCount != null ? (int)shotParams.shotCount : _shotCount;
            this.asset = shotParams.asset;
            if (shotParams.point != null)
            {
                this.point = new Point(shotParams.point[0], shotParams.point[1]);
            }
        }

        public override void CreateShots(Entity entity, GameTime gameTime)
        {
            if (_shotCount == 0)
            {
                return;
            }

            double shotOffset = Math.PI * 2d / _shotCount;
            for (int i = 0; i < _shotCount; i++)
            {
                (Shot shot, Sprite sprite) = ShotFactory.CreateShot(_shotParams);
                MovementPattern.MovementPattern movementPattern = factory.CreateMovementPattern(new MovementParams { direction = shotOffset * i, speed = 5 });
                shot.MovementPattern = movementPattern;
                DrawController.AddSprite(sprite);

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

                shot.NotifyOfDestinationRectangleChange();
                ShotController.AddShot(shot);

                // create proper command
                if (entity is Enemy)
                {
                    // then shots should collide with player
                    CollisionBulletCommand command = new CollisionBulletCommand(shot, Player.Instance);
                    CollisionDetector.AddCommand(command);
                }

                if (entity is Player)
                {
                    // then shots should collide with enemies - player won't use CircleShotPattern (probably)
                }
            }
        }
    }
}
