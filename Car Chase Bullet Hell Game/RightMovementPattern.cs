using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class RightMovementPattern : MovementPattern
    {
        //float speed = 100f;
        Queue<Point> waypoints = new Queue<Point>();
        Queue<Point> discarded = new Queue<Point>();
        public bool repeat = false;
        private int curRun = 0;

        private bool moveRight = true;

        public void AddPoint(Point p)
        {
            waypoints.Enqueue(p);
        }

        public override void Move(GameTime gameTime, Enemy enemy)
        {
            if (curRun == 0)
            {
                ++curRun;
                enemy.DestinationRectangle.Y = 100;
                enemy.DestinationRectangle.X = 0;
            }

            if (moveRight)
                enemy.DestinationRectangle.X += 5;
            else
                enemy.DestinationRectangle.X -= 5;

            if (enemy.DestinationRectangle.X > 550 || enemy.DestinationRectangle.X < 0)
                moveRight = !moveRight;
        }
    }
}