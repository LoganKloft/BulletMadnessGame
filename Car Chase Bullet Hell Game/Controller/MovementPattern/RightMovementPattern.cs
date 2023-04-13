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
    internal class RightMovementPattern : MovementPattern
    {
        private int curRun = 0;
        private bool moveRight = true;
        MovementParams _movementParams;

        public RightMovementPattern(MovementParams movementParams)
        {
            _movementParams = movementParams;
        }

        public override void Move(GameTime gameTime, Entity entity)
        {
            if (curRun == 0)
            {
                ++curRun;
                entity.DestinationRectangle.Y = 100;
                entity.DestinationRectangle.X = 0;
            }

            if (moveRight)
                entity.DestinationRectangle.X += 5;
            else
                entity.DestinationRectangle.X -= 5;

            if (entity.DestinationRectangle.X > 550 || entity.DestinationRectangle.X < 0)
                moveRight = !moveRight;

            entity.NotifyOfDestinationRectangleChange();
        }
    }
}