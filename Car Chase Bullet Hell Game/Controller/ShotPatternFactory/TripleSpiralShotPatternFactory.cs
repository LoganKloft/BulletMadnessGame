using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class TripleSpiralShotPatternFactory : ShotPatternFactory
    {
        public override ShotPattern CreateShotPattern(ShotParams shotParams)
        {
            return new TripleSpiralShotPattern(shotParams);
        }
    }
}
