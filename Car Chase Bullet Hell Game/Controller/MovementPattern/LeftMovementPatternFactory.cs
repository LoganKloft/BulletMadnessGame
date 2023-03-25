using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class LeftMovementPatternFactory : MovementFactory
    {
        protected override MovementPattern CreateMovementPattern([Optional] Point point, [Optional] int radius, [Optional] Entity entity)
        {
            return new LeftMovementPattern();
        }
    }
}
