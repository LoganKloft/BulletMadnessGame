using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionBulletBulletsCommand : Command
    {
        public override event DestroyCommandEventHandler DestroyEvent;

        private Shot _shot;

        public CollisionBulletBulletsCommand(Shot shot)
        {
            this._shot = shot;
            _shot.DestroyEvent += InvokeDestroyEvent;
        }

        public void InvokeDestroyEvent(Entity entity)
        {
            DestroyEvent?.Invoke(this);
        }

        public override void execute()
        {
            foreach (Shot shot in ShotController.GetShots())
            {
                if (shot?.shotParams?.sticky != null)
                {
                    if (shot.shotParams.sticky == true) continue;
                }

                if (_shot.HitBoxRectangle.Intersects(shot.HitBoxRectangle))
                {
                    shot.MovementPattern = _shot.MovementPattern;
                }
            }
        }
    }
}
