using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class TargetingShot : Shot
    {
        public float LifeTime = 10f; // lifetime in seconds
        public double _direction = 0d;
        private double _xDirection = 1d;
        private double _yDirection = 0d;
        private double _speed = 10f;
        private double _rotationSpeed = .05f;

        public TargetingShot()
        {
            base.Origin = new Vector2(base.DestinationRectangle.Width / 2, base.DestinationRectangle.Height / 2);
        }

        public delegate void BulletLifetimeExpiredEventHandler(object sender);
        public BulletLifetimeExpiredEventHandler BulletLifetimeExpiredEvent;

        public double Direction
        {
            set
            {
                _direction = value;
                _xDirection = Math.Cos(_direction);
                _yDirection = Math.Sin(_direction);
            }
            get
            {
                return _direction;
            }
        }

        public override void Move(GameTime gameTime)
        {
            Point target = Player.Instance.Center;
            Point current = base.Center;

            // v = unit vector from current to target
            Vector2 v = new Vector2(target.X - current.X, target.Y - current.Y);
            float magnitude = (float)Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
            v.X = Math.Abs(v.X / magnitude);
            v.Y = Math.Abs(v.Y / magnitude);

            // angle based on x-axis
            double angle = Math.Atan2(v.Y, v.X);

            // adjust angle based on position of bullet relative to player - angle is in the fourth quadrant
            if (current.X < target.X)
            {
                // to the left of the target
                if (current.Y < target.Y)
                {
                    // above the target and to the left
                    // target in 4th quadrant - do nothing
                }
                else
                {
                    // below the target and to the left
                    // target in 1st quadrant - add 3pi/2
                    angle += MathHelper.Pi + MathHelper.PiOver2;
                }
            }
            else
            {
                // to the right of the target
                if (current.Y < target.Y)
                {
                    // above the target and to the right
                    // target in 3rd quadrant - add pi/2
                    angle += MathHelper.PiOver2;
                }
                else
                {
                    // below the target and to the right
                    // target in 2nd quadrant - add pi
                    angle += MathHelper.Pi;
                }
            }

            double offset = Direction - angle < 0 ? 1 : -1;
            offset *= _rotationSpeed;
            Direction += offset;

            base.Rotation = (float)Direction;
            base.DestinationRectangle.X += (int)(_xDirection * _speed);
            base.DestinationRectangle.Y += (int)(_yDirection * _speed);

            LifeTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (LifeTime < 0)
            {
                BulletLifetimeExpiredEvent?.Invoke(this);
                base.InvokeBulletEnd();
            }
        }
    }
}
