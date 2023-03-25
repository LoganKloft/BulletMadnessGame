using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class OffScreenMovementFactory : MovementFactory
    {
        protected override MovementPattern CreateMovementPattern([Optional] Point point, [Optional] int radius)
        {
            return new OffScreenMovementPattern();
        }
    }
}
