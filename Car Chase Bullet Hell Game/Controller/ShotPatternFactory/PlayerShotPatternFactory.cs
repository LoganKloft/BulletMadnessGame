using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class PlayerShotPatternFactory : ShotPatternFactory
    {
        public override ShotPattern CreateShotPattern(ShotParams shotParams)
        {
            Point point = new Point(0, 0);
            if (shotParams.point != null)
            {
                point.X = shotParams.point[0];
                point.Y = shotParams.point[1];
            }
            return new PlayerShotPattern();
        }
    }
}
