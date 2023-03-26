﻿using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class RightMovementPatternFactory : MovementFactory
    {
        protected override MovementPattern CreateMovementPattern([Optional] Point point, [Optional] int radius, [Optional] Entity entity, [Optional] double direction, [Optional] double speed)
        {
            return new RightMovementPattern();
        }
    }
}