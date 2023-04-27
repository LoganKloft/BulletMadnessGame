using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class TriangleMovementPattern : MovementPattern
    {
        private float speed = 5f;
        private float time = 0;
        private Point[] points = { new Point(100, 200), new Point(950, 200), new Point(525, 50) };
        private int pointIndex = 0;
        private int lap = 0;
        private double x_move = 0, y_move = 0, distance = 0, direction = 0;
        private bool pause = false;
        MovementParams _movementParams;

        public TriangleMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
            speed = movementParams.speed != null ? (float)movementParams.speed : speed;
        }

        public override void Move(GameTime gameTime, List<Entity> entity)
        {
            // Set enemy at the correct position at the start of the movement pattern
            if (lap == 0)
            {
                entity[0].DestinationRectangle.X = -10;
                entity[0].DestinationRectangle.Y = -10;
                entity[0].NotifyOfDestinationRectangleChange();
                lap++;
            }
            //Update the index when it goes out of range
            if (pointIndex == 3)
            {
                pointIndex = 0;
            }
            //Account for pauses at each corner of the triangle
            if (time % 100 == 0)
            {
                pause = false;
                //Calculate necessary movement directions
                x_move = points[pointIndex].X - entity[0].DestinationRectangle.X;
                y_move = points[pointIndex].Y - entity[0].DestinationRectangle.Y;
                distance = Math.Sqrt(Math.Pow(x_move, 2) + Math.Pow(y_move, 2));
                direction = speed / distance;
                //Update destination rectangles
                bool updated = false;
                if (x_move != 0)
                {
                    entity[0].DestinationRectangle.X += (int)(x_move * direction);
                    updated = true;
                }
                if (y_move != 0)
                {
                    entity[0].DestinationRectangle.Y += (int)(y_move * direction);
                    updated = true;
                }
                if (updated)
                {
                    entity[0].NotifyOfDestinationRectangleChange();
                }
            }
            //Update the index of the point we are trying to get to once we have reached the previous point.
            if (entity[0].DestinationRectangle.X >= points[pointIndex].X - 5 && entity[0].DestinationRectangle.X <= points[pointIndex].X + 5 && entity[0].DestinationRectangle.Y >= points[pointIndex].Y - 5 && entity[0].DestinationRectangle.Y <= points[pointIndex].Y + 5)
            {
                pause = true;
                pointIndex++;
            }
            //Handle pausing at each corner of the triagle
            if (pause == true)
            {
                time++;
            }
        }
    }
}
