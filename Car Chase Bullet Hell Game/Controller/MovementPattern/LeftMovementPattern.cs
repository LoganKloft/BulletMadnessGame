using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class LeftMovementPattern : MovementPattern
    {
        private int curRun = 0;
        private bool moveLeft = true;
        MovementParams _movementParams;

        public LeftMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
        }

        public override void Move(GameTime gameTime, List<Entity> entity)
        {
            if (curRun == 0)
            {
                ++curRun;
                entity[0].DestinationRectangle.Y = 100;
                entity[0].DestinationRectangle.X = 1120;
            }

            if (moveLeft)
                entity[0].DestinationRectangle.X -= 5;
            else
                entity[0].DestinationRectangle.X += 5;

            if (entity[0].DestinationRectangle.X < 580 || entity[0].DestinationRectangle.X > 1130)
                moveLeft = !moveLeft;

            entity[0].NotifyOfDestinationRectangleChange();
        }
    }
}
