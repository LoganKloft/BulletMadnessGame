using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class SpiralMovementPattern : MovementPattern
    {
        private int _radius = 100;
        private int _endRadius = 400;
        private Point _pivotPoint = new Point(0, 0); // the point to rotate around
        private float _speed = 2f;
        private double _angle = Math.PI / 2d;

        public SpiralMovementPattern(Point pivotPoint, int radius, int endRadius, int startDegree)
        {
            _pivotPoint = pivotPoint;
            _radius = radius;
            _endRadius = endRadius;
            _angle = (Math.PI / 180) * startDegree;
        }

        public Point PivotPoint
        {
            get { return _pivotPoint; }
            set { _pivotPoint = value; }
        }
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public int endRadius
        {
            get { return _endRadius; }
            set { _endRadius = value; }
        }

        public override void Move(GameTime gameTime, Entity entity)
        {
            _angle += gameTime.ElapsedGameTime.TotalSeconds * _speed;

            double x_component = Math.Cos(_angle) * Radius;
            double y_component = Math.Sin(_angle) * Radius;

            entity.DestinationRectangle.X = PivotPoint.X + (int)x_component;
            entity.DestinationRectangle.Y = PivotPoint.Y + (int)y_component;

            entity.NotifyOfDestinationRectangleChange();

            if (_radius < _endRadius)
            {
                // could base increase on speed, but _radius is an integer so not ideal
                _radius++;
            }
        }
    }
}