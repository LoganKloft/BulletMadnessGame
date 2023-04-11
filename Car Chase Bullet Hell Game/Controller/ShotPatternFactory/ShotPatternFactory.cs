using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal abstract class ShotPatternFactory
    {
        public abstract ShotPattern CreateShotPattern(ShotParams shotParams);
    }
}
