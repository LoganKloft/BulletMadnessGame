using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal abstract class ShotPatternFactory
    {
        protected abstract ShotPattern CreateShotPattern(string asset, Point point, int shotCount);

        public ShotPattern CreateShots(string asset, Point point, int shotCount, Entity entity)
        {
            if(entity is Player)
            {
                return CreateShotPattern(asset, point, shotCount);
            }
            ShotPattern shotPattern = CreateShotPattern(asset, point, shotCount);
            shotPattern.CreateShots(entity: entity);
            return shotPattern;
        }
    }
}
