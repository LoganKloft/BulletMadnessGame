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
            //x.DestroyEvent += DestroyEventHandler;
            //y.DestroyEvent += DestroyEventHandler;

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

            enemy.DestroyEvent += DestroyEventHandler;
        }

        public override event DestroyCommandEventHandler DestroyEvent;

        public override void execute()
        {
            if (enemy.HitBoxRectangle.Intersects(player.HitBoxRectangle))
            {
                ((Player)player).TakeDamage(enemy);
            }
        }

        public void DestroyEventHandler(Entity entity)
        {
            InvokeDestroyEvent();
        }

        public void InvokeDestroyEvent()
        {
            DestroyEvent?.Invoke(this);
        }
    }
}