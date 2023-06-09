﻿using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class StraightMovementPattern : MovementPattern
    {
        float speed = 1000f;
        Queue<Point> waypoints = new Queue<Point>();
        Queue<Point> discarded = new Queue<Point>();
        public bool repeat = false;
        MovementParams _movementParams;

        public StraightMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
            speed = movementParams.speed != null ? (float)movementParams.speed : speed;
        }

        public void AddPoint(Point p)
        {
            waypoints.Enqueue(p);
        }

        public override void Move(GameTime gameTime, List<Entity> entity)
        {
            if (waypoints.Count > 0)
            {
                Point target = waypoints.Peek();
                Point current = entity[0].Center;

                // move the enemy closer to the target
                // 1 - create unit vector
                Vector2 v = new Vector2(target.X - current.X, target.Y - current.Y);
                float magnitude = (float)Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));
                v.X = v.X / magnitude;
                v.Y = v.Y / magnitude;

                Point difference = target - current;

                int xDistance = (int)Math.Min((float)Math.Abs(gameTime.ElapsedGameTime.TotalSeconds * speed * v.X), Math.Abs(difference.X));
                int yDistance = (int)Math.Min((float)Math.Abs(gameTime.ElapsedGameTime.TotalSeconds * speed * v.Y), Math.Abs(difference.Y));

                xDistance = v.X < 0 ? -xDistance : xDistance;
                yDistance = v.Y < 0 ? -yDistance : yDistance;

                Point offset = new Point(xDistance, yDistance);
                entity[0].DestinationRectangle.Offset(offset);
                entity[0].NotifyOfDestinationRectangleChange();

                // check if we've reached the target
                if (entity[0].Center == target)
                {
                    // we've reached the taget, time to move towards the next waypoint
                    discarded.Enqueue(waypoints.Dequeue());
                }
            }
            else if (repeat)
            {
                while (discarded.Count > 0)
                {
                    waypoints.Enqueue(discarded.Dequeue());
                }
            }

        }
    }
}
