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
        private int _radius = 500;
        private Point _pivotPoint = new Point(1750, 1000); // the point to rotate around
        float _speed = 1f;
        double _angle = Math.PI / 2d;

        public override void Move(GameTime gameTime, Enemy enemy)
        {
            _angle += gameTime.ElapsedGameTime.TotalSeconds * _speed;

            double x_component = Math.Cos(_angle) * _radius;
            double y_component = Math.Sin(_angle) * _radius;

            enemy.DestinationRectangle.X = _pivotPoint.X + (int)x_component;
            enemy.DestinationRectangle.Y = _pivotPoint.Y + (int)y_component;
        }
    }
}
