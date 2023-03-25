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
        private Shot bullet;
        private Entity entity2;

        public override event DestroyCommandEventHandler DestroyEvent;

        public CollisionBulletCommand(Entity x, Entity y)
        {
            x.DestroyEvent += DestroyEventHandler;
            y.DestroyEvent += DestroyEventHandler;

            if (x is Shot)
            {
                bullet = (Shot)x;
                entity2 = y;
            }

            else if (y is Shot)
            {
                bullet = (Shot)y;
                entity2 = x;
            }
        }

        public override void execute()
        {
            if (bullet.HitBoxRectangle.Intersects(entity2.HitBoxRectangle))
            {
                if (entity2 is Enemy)
                {
                    ((Enemy)entity2).TakeDamage(bullet);
                    bullet.InvokeDestroyEvent();
                }

                else if (entity2 is Player)
                {
                    ((Player)entity2).TakeDamage(bullet);
                    bullet.InvokeDestroyEvent();
                }
                CollisionDetector.RemoveCommand(this);
            }
        }

        public void DestroyEventHandler(Entity entity)
        {
            DestroyEvent?.Invoke(this);
        }
    }
}
