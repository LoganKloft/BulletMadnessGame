using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal abstract class ShotPattern
    {
        //public abstract void Update(GameTime gameTime);
        //public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        //public abstract bool Finished();

        public abstract void CreateShots(Entity entity, GameTime gameTime = null);

        public static ShotPattern Parse(string type, string asset, Point point, int shotCount, Entity entity)
        {
            if (type == "CircleShotPattern")
            {
                CircleShotPattern csp = new CircleShotPattern(asset, point, shotCount);
                csp.CreateShots(entity, null);
                return csp;
            }
            else if (type == "HalfCircleShotPattern")
            {
                HalfCircleShotPattern hcsp = new HalfCircleShotPattern(asset, point, shotCount);
                hcsp.CreateShots(entity, null);
                return hcsp;
            }
            else if (type == "StraightShotPattern")
            {
                StraightShotPattern shp = new StraightShotPattern(asset, point, shotCount);
                shp.CreateShots(entity, null);
                return shp;
            }

            return null;
        }
    }
}
