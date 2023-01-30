using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class StraightShot : Shot
    {
        private double _direction = 0d;
        private double _xDirection = 1d;
        private double _yDirection = 0d;
        private double _speed = 10f;

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
            base.DestinationRectangle.X += (int) (_xDirection * _speed);
            base.DestinationRectangle.Y += (int)(_yDirection * _speed);
        }
    }
}
