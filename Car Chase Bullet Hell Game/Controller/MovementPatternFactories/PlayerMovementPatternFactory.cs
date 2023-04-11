using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories
{
    internal class PlayerMovementPatternFactory : MovementFactory
    {

        public override MovementPattern.MovementPattern CreateMovementPattern(MovementParams movementParams)
        {
            return new PlayerMovementPattern();
        }
    }
}
