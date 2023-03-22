using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.Model.MovementPattern
{
    internal abstract class MovementPattern
    {
        public abstract void Move(GameTime gameTime, Entity entity);

        public static MovementPattern Parse(string pattern)
        {
            if (pattern == "LeftMovementPattern")
            {
                return new LeftMovementPattern();
            }
            else if (pattern == "RightMovementPattern")
            {
                return new RightMovementPattern();
            }
            else if (pattern == "TriangleMovementPattern")
            {
                return new TriangleMovementPattern();
            }
            else if (pattern.Contains("CircleMovementPattern"))
            {
                return new CircleMovementPattern(new Point(625, 80), 80);
            }

            return null;
        }
    }
}
