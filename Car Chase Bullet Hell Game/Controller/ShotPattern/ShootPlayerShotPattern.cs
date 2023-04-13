using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class ShootPlayerShotPattern : ShotPattern
    {
        string _asset;
        int _shotCount;
        ShotParams _shotParams;
        private StraightShotFactory factory = new StraightShotFactory();

        public ShootPlayerShotPattern(ShotParams shotParams) : base()
        {
            _shotParams = shotParams;
        }
        public override void CreateShots(Entity entity, GameTime gameTime)
        {
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
            MovementPattern.MovementPattern movementPattern = factory.CreateMovementPattern(new MovementParams { direction = angle });
            shot.MovementPattern = movementPattern;
            DrawController.AddSprite(sprite);

            shot.DestinationRectangle.X = entity.Center.X - (shot.DestinationRectangle.Width / 2);
            shot.DestinationRectangle.Y = entity.Center.Y - (shot.DestinationRectangle.Height / 2);
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
