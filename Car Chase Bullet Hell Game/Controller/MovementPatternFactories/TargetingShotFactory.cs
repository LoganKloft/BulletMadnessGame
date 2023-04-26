﻿using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories
{
    internal class TargetingShotFactory : MovementFactory
    {
        public override MovementPattern.MovementPattern CreateMovementPattern(MovementParams movementParams)
        {
            // need to be able to search an entity based on their id
            //Entity entity = GetEntity(movementParams.entity);
            //return new TargetingShot(entity);
            return new TargetingShot(movementParams);
        }
    }
}
