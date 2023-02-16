using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Content
{
    internal class TriangleMovementPattern : MovementPattern
    {
        private float speed = 5f;
        private float time = 0;
        private Point[] points = { new Point(400, 1000), new Point(1800, 1000), new Point(1000, 500) };
        private int pointIndex = 1;
        private int lap = 0;
        private double x_move = 0, y_move = 0, distance = 0, direction = 0;
        private bool pause = false;
        
        public override void Move(GameTime gameTime, Enemy enemy)
        {
            // Set enemy at the correct position at the start of the movvement pattern
            if(lap==0)
            {
                enemy.DestinationRectangle.X = 400;
                enemy.DestinationRectangle.Y = 1000;
                lap++;
            }
            //Update the index when it goes out of range
            if(pointIndex == 3)
            {
                pointIndex = 0;
            }
            //Account for pauses at each corner of the triangle
            if(time%100==0)
            {
                pause = false;
                //Calculate necessary movement directions
                x_move = points[pointIndex].X - enemy.DestinationRectangle.X;
                y_move = points[pointIndex].Y - enemy.DestinationRectangle.Y;
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
            //Update the index of the point we are trying to get to once we have reached the previous point.
            if(enemy.DestinationRectangle.X == points[pointIndex].X && enemy.DestinationRectangle.Y == points[pointIndex].Y)
            {
                pause = true;
                pointIndex++;
            }
            //Handle pausing at each corner of the triagle
            if(pause==true)
            {
                time++;
            }
         }
    }
}
