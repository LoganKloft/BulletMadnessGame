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
        protected abstract MovementPattern CreateMovementPattern([Optional] Point point, [Optional] int radius);

        public MovementPattern createMovement(Point point = default(Point), int radius=-1)
        {
            if (point != default(Point) && radius != -1)
            {
                return CreateMovementPattern(point, radius);
            }
            else if ((point == default(Point) && radius !=-1) || (point != default(Point) && radius==-1))
            {
                return null;
            }
            else
            {
                return CreateMovementPattern();
            }
        }
    }
}
