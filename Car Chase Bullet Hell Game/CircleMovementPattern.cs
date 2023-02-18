using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class CircleMovementPattern : MovementPattern
    {
        private int _radius = 200;
        private Point _pivotPoint = new Point(0, 0); // the point to rotate around
        private float _speed = 2f;
        private double _angle = Math.PI / 2d;

        public CircleMovementPattern(Point pivotPoint, int radius)
        {
            _pivotPoint = pivotPoint;
            _radius = radius;
        }

        public Point PivotPoint {
            get { return _pivotPoint; }
            set { _pivotPoint = value; }
        }
        public int Radius { 
            get { return _radius; }
            set { _radius = value; }
        }

        public override void Move(GameTime gameTime, Enemy enemy)
        {
            _angle += gameTime.ElapsedGameTime.TotalSeconds * _speed;

            double x_component = Math.Cos(_angle) * Radius;
            double y_component = Math.Sin(_angle) * Radius;

            enemy.DestinationRectangle.X = PivotPoint.X + (int)x_component;
            enemy.DestinationRectangle.Y = PivotPoint.Y + (int)y_component;
        }
    }
}
