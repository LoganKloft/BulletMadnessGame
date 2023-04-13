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
    internal class CircleMovementPattern : MovementPattern
    {
        private int _radius = 200;
        private Point _pivotPoint = new Point(0, 0); // the point to rotate around
        private float _speed = 2f;
        private double _angle = Math.PI / 2d;
        MovementParams _movementParams;

        public CircleMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
            _pivotPoint = _movementParams.point != null ? new Point(_movementParams.point[0], _movementParams.point[1])
                : _pivotPoint;

            _radius = _movementParams.radius != null ? (int)_movementParams.radius : _radius;

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

        public override void Move(GameTime gameTime, Entity entity)
        {
            _angle += gameTime.ElapsedGameTime.TotalSeconds * _speed;

            double x_component = Math.Cos(_angle) * Radius;
            double y_component = Math.Sin(_angle) * Radius;

            entity.DestinationRectangle.X = PivotPoint.X + (int)x_component;
            entity.DestinationRectangle.Y = PivotPoint.Y + (int)y_component;

            entity.NotifyOfDestinationRectangleChange();
        }
    }
}
