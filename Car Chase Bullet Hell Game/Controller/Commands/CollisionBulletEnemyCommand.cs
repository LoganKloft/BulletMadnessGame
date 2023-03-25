using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionBulletEnemyCommand : Command
    {
        private Shot _shot;
        public CollisionBulletEnemyCommand(Shot shot)
        {
            shot.DestroyEvent += DestroyEventHandler;

            _shot = shot;
        }

        public override event DestroyCommandEventHandler DestroyEvent;

        public override void execute()
        {
            foreach (Enemy enemy in Spawner.GetActiveEnemies())
            {
                if (Player.Instance.DestinationRectangle.Intersects(enemy.DestinationRectangle))
                {
                    enemy.TakeDamage(_shot);
                    _shot.InvokeDestroyEvent();
                }
            }
        }

        public void DestroyEventHandler(Entity entity)
        {
            DestroyEvent?.Invoke(this);
        }
    }
}
