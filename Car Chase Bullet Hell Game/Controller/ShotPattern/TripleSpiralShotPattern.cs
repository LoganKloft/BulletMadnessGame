using Car_Chase_Bullet_Hell_Game.Controller.Commands;
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
    internal class TripleSpiralShotPattern : ShotPattern
    {

        private StraightShotFactory straightShotFactory = new StraightShotFactory();
        string asset;
        ShotParams _shotParams;

        public TripleSpiralShotPattern(ShotParams shotParams) : base()
        {
            this._shotParams = shotParams;
            this.asset = shotParams.asset;
        }

        // shoots at player location
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

            // (2) create three shots, one shooting at the player, the others +120 and +240 degrees of the original shot
            (Shot shot1, Sprite sprite1) = ShotFactory.CreateShot(_shotParams);
            MovementPattern.MovementPattern movementPattern1 = straightShotFactory.CreateMovementPattern(new MovementParams() { direction = angle});
            shot1.MovementPattern = movementPattern1;
            DrawController.AddSprite(sprite1);

            (Shot shot2, Sprite sprite2) = ShotFactory.CreateShot(_shotParams);
            MovementPattern.MovementPattern movementPattern2 = straightShotFactory.CreateMovementPattern(new MovementParams() { direction = angle + ((2.0d / 3.0d) * Math.PI) });
            shot2.MovementPattern = movementPattern2;
            DrawController.AddSprite(sprite2);

            (Shot shot3, Sprite sprite3) = ShotFactory.CreateShot(_shotParams);
            MovementPattern.MovementPattern movementPattern3 = straightShotFactory.CreateMovementPattern(new MovementParams() { direction = angle + ((4.0d / 3.0d) * Math.PI) });
            shot3.MovementPattern = movementPattern3;
            DrawController.AddSprite(sprite3);

            if (_shotParams.point == null)
            {
                shot1.DestinationRectangle.X = entity.Center.X - (shot1.DestinationRectangle.Width / 2);
                shot1.DestinationRectangle.Y = entity.Center.Y - (shot1.DestinationRectangle.Height / 2);

                shot2.DestinationRectangle.X = entity.Center.X - (shot1.DestinationRectangle.Width / 2);
                shot2.DestinationRectangle.Y = entity.Center.Y - (shot1.DestinationRectangle.Height / 2);

                shot3.DestinationRectangle.X = entity.Center.X - (shot3.DestinationRectangle.Width / 2);
                shot3.DestinationRectangle.Y = entity.Center.Y - (shot3.DestinationRectangle.Height / 2);


            }
            else
            {
                shot1.DestinationRectangle.X = this.point.X;
                shot1.DestinationRectangle.Y = this.point.Y;
            }

            ShotController.AddShot(shot1);
            ShotController.AddShot(shot2);
            ShotController.AddShot(shot3);

            // create proper command
            if (entity is Enemy)
            {
                // then shots should collide with player
                CollisionBulletCommand command = new CollisionBulletCommand(shot1, Player.Instance);
                CollisionDetector.AddCommand(command);

                command = new CollisionBulletCommand(shot2, Player.Instance);
                CollisionDetector.AddCommand(command);

                command = new CollisionBulletCommand(shot3, Player.Instance);
                CollisionDetector.AddCommand(command);
            }
        }
    }
}
