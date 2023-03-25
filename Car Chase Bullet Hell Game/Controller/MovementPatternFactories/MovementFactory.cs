﻿using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal abstract class MovementFactory
    {
        protected abstract MovementPattern CreateMovementPattern([Optional] Point point, [Optional] int radius, [Optional] Entity entity, [Optional] double direction, [Optional] double speed);

        public MovementPattern createMovement(Point point = default(Point), int radius=-1, Entity entity = null, double direction = double.NaN, double speed = double.NaN)
        {
            if (point != default(Point) && radius != -1)
            {
                return CreateMovementPattern(point: point, radius: radius);
            }
            else if ((point == default(Point) && radius !=-1) || (point != default(Point) && radius==-1))
            {
                return null;
            }
            else if(entity!=null)
            {
                return CreateMovementPattern(entity: entity);
            }
            else if(direction!=double.NaN)
            {
                return CreateMovementPattern(direction: direction, speed: speed);
            }
            else
            {
                return CreateMovementPattern();
            }
        }
    }
}