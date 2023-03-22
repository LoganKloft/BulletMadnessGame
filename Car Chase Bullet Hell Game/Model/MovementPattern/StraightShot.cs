using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.Entities;

namespace Car_Chase_Bullet_Hell_Game.Model.MovementPattern
{
    internal class StraightShot : Shot
    {
        private double _direction = 0d;
        private double _xDirection = 1d;
        private double _yDirection = 0d;
        private double _speed = 5f;

        public double Direction
        {
            set
            {
                _direction = value;
                _xDirection = Math.Cos(_direction);
                _yDirection = Math.Sin(_direction);
            }
        }

        public override void Move(GameTime gameTime)
        {
            Origin = new Vector2(DestinationRectangle.Width / 2, DestinationRectangle.Height / 2);
            Rotation = (float)_direction;
            DestinationRectangle.X += (int)(_xDirection * _speed);
            DestinationRectangle.Y += (int)(_yDirection * _speed);
            NotifyOfDestinationRectangleChange();
            Offscreen();
        }
    }
}
