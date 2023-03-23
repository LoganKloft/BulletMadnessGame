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
        public abstract void Update(GameTime gameTime);
        //public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract bool Finished();

        public static ShotPattern Parse(string type, string asset, Entity entity)
        {
            if (type == "CircleShotPattern")
            {
                CircleShotPattern csp = new CircleShotPattern(16);
                csp.CreateShots(Game1.content, asset, entity.Center);
                return csp;
            }
            else if (type == "HalfCircleShotPattern")
            {

            }
            else if (type == "StraightShotPattern")
            {

            }

            return null;
        }
    }
}
