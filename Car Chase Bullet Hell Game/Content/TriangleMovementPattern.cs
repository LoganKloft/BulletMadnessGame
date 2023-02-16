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
        private float speed = 2f;
        private float time = 0;
        private Point[] points = { new Point(400, 1000), new Point(1800, 1000), new Point(1000, 500) };
        int pointIndex = 1;
        double dx = 0, dy = 0, distance = 0, scale = 0;
        
        public override void Move(GameTime gameTime, Enemy enemy)
        {
            if(time==0)
            {
                enemy.DestinationRectangle.X = 300;
                enemy.DestinationRectangle.Y = 1000;
                time++;
            }
            if(pointIndex == 3)
            {
                pointIndex = 0;
            }
            dx = points[pointIndex].X - enemy.DestinationRectangle.X;
            dy = points[pointIndex].Y - enemy.DestinationRectangle.Y;
            distance = Math.Sqrt(dx * dx + dy * dy);
            scale = 5f / distance;
            if(dx!=0)
            {
                enemy.DestinationRectangle.X += (int)(dx * scale);
            }
            if (dy!=0)
            {
                enemy.DestinationRectangle.Y += (int)(dy * scale);
            }
            if(enemy.DestinationRectangle.X == points[pointIndex].X && enemy.DestinationRectangle.Y == points[pointIndex].Y)
            {
                pointIndex++;
            }
        }
    }
}
