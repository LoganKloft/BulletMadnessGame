using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;


namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionBulletCommand : Command
    {
        private Entity bullet;
        private Entity entity2;

        CollisionBulletCommand(Entity x, Entity y)
        {
            if (x is Shot)
            {
                bullet = x;
                entity2 = y;
            }

            else
            {
                bullet = y;
                entity2 = x;
            }
        }

        public override void execute()
        {
            if (bullet.HitBoxRectangle.Intersects(entity2.HitBoxRectangle))
            {
                return;
                // DECREASE THE HEALTH BAR OF THE ENTITY THAT WAS HTI! 
            }
        }
    }
}
