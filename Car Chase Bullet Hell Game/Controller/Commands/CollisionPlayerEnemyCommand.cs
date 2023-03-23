using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionPlayerEnemyCommand : Command
    {
        private Entity enemy;
        private Entity player;

        public CollisionPlayerEnemyCommand(Entity x, Entity y)
        {
            if (x is Enemy)
            {
                enemy = x;
                player = y;
            }

            else
            {
                enemy = y;
                player = x;
            }
        }

        public override void execute()
        {
            if (enemy.HitBoxRectangle.Intersects(player.HitBoxRectangle))
            {
                return;
                // DECREASE THE HEALTH BAR OF THE BOTH OF THE ENTITIES THAT WERE HTI! 
            }
        }
    }
}