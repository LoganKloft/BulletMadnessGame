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
    internal class OffScreenMovementPattern : MovementPattern
    {
        private float speed = 5f;
        private Point location = new Point(545, -1000);
        private double x_move = 0, y_move = 0, distance = 0, direction = 0;
        MovementParams _movementParams;

        public OffScreenMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
            speed = movementParams.speed != null ? (float)movementParams.speed : speed;
        }

        public override void Move(GameTime gameTime, List<Entity> entity)
        {

            x_move = location.X - entity[0].DestinationRectangle.X;
            y_move = location.Y - entity[0].DestinationRectangle.Y;
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
    }
}
