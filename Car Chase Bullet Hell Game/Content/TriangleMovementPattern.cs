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
        private int instance = 1, lap = 0;
        private double x_move = 0, y_move = 0, distance = 0, direction = 0;
        private bool pause = false;
        
        public override void Move(GameTime gameTime, Enemy enemy)
        {
            if(lap==0)
            {
                enemy.DestinationRectangle.X = 400;
                enemy.DestinationRectangle.Y = 1000;
                lap++;
            }
            if(pointIndex == 3)
            {
                pointIndex = 0;
            }
            if(time%100==0)
            {
                pause = false;
                instance = 1;
                x_move = points[pointIndex].X - enemy.DestinationRectangle.X;
                y_move = points[pointIndex].Y - enemy.DestinationRectangle.Y;
                distance = Math.Sqrt(Math.Pow(x_move, 2) + Math.Pow(y_move, 2));
                direction = speed / distance;
                if (x_move != 0)
                {
                    enemy.DestinationRectangle.X += (int)(x_move * direction);
                }
                if (y_move != 0)
                {
                    enemy.DestinationRectangle.Y += (int)(y_move * direction);
                }
            }
            if(enemy.DestinationRectangle.X == points[pointIndex].X && enemy.DestinationRectangle.Y == points[pointIndex].Y)
            {
                pause = true;
                pointIndex++;
            }
            if(pause==true)
            {
                time++;
            }
         }
    }
}
