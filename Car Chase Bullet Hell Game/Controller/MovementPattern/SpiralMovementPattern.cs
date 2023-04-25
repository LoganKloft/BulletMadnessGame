using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class SpiralMovementPattern : MovementPattern
    {
        private int _radius = 200;
        private int _endRadius = 200;
        private Point _pivotPoint = new Point(0, 0); // the point to rotate around
        private Entity _enemy;
        private float _speed = 2f;
        private double _angle = Math.PI / 2d;
        MovementParams _movementParams;

        public SpiralMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
            _pivotPoint = _movementParams.point != null ? new Point(_movementParams.point[0], _movementParams.point[1])
                : _pivotPoint;
            _radius = _movementParams.radius != null ? (int)_movementParams.radius : _radius;

            _endRadius = _movementParams.endRadius != null ? (int) _movementParams.endRadius : _endRadius;
            _angle = _movementParams.startDegree != null ? (double)(Math.PI / 180) * (int)_movementParams.startDegree : 0;
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

        public override void Move(GameTime gameTime, List<Entity> entity)
        {
            _angle += gameTime.ElapsedGameTime.TotalSeconds * _speed;
            _enemy = entity[1];

            double x_component = Math.Cos(_angle) * Radius;
            double y_component = Math.Sin(_angle) * Radius;
            PivotPoint = new Point(_enemy.Center.X, _enemy.Center.Y);
            entity[0].DestinationRectangle.X = PivotPoint.X + (int)x_component;
            entity[0].DestinationRectangle.Y = PivotPoint.Y + (int)y_component;

            entity[0].NotifyOfDestinationRectangleChange();

            //if (_radius < _endRadius)
            //{
            //    // could base increase on speed, but _radius is an integer so not ideal
            //    _radius++;
            //}
        }
    }
}