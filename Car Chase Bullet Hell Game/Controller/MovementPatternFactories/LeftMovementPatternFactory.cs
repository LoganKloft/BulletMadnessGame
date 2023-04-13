using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories
{
    internal class LeftMovementPatternFactory : MovementFactory
    {
        public override MovementPattern.MovementPattern CreateMovementPattern(MovementParams movementParams)
        {
            return new LeftMovementPattern(movementParams);
        }
    }
}
