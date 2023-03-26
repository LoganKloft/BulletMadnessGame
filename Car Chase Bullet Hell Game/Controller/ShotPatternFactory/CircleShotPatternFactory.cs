using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class CircleShotPatternFactory: ShotPatternFactory
    {
        protected override ShotPattern CreateShotPattern(string asset, Point point, int shotCount)
        {
            return new CircleShotPattern(asset, point, shotCount);
        }
    }
}
