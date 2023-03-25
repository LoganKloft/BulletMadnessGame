using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class LeftMovementPatternFactory : MovementFactory
    {
        protected override MovementPattern CreateMovementPattern([Optional] Point point, [Optional] int radius)
        {
            return new LeftMovementPattern();
        }
    }
}
