using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class OffScreenMovementPattern : MovementPattern
    {
        private float speed = 5f;
        private Point location = new Point(1325, 1700);
        private double x_move = 0, y_move = 0, distance = 0, direction = 0;
        public override void Move(GameTime gameTime, Enemy enemy)
        {
            x_move = location.X - enemy.DestinationRectangle.X;
            y_move = location.Y - enemy.DestinationRectangle.Y;
            distance = Math.Sqrt(Math.Pow(x_move, 2) + Math.Pow(y_move, 2));
            direction = speed / distance;
            //Update destination rectangles
            if (x_move != 0)
            {
                enemy.DestinationRectangle.X += (int)(x_move * direction);
            }
            if (y_move != 0)
            {
                enemy.DestinationRectangle.Y += (int)(y_move * direction);
            }
        }
    }
}
