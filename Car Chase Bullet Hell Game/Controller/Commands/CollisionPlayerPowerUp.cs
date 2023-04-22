using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;

namespace Car_Chase_Bullet_Hell_Game.Controller.Commands
{
    internal class CollisionPlayerPowerUpCommand : Command
    {
        private Entity power;
        private Entity player;

        public CollisionPlayerPowerUpCommand(Entity x, Entity y)
        { 
            if (x is Powerup)
            {
                power = x;
                player = y;
            }

            else
            {
                power = y;
                player = x;
            }

            power.DestroyEvent += DestroyEventHandler;
        }

        public override event DestroyCommandEventHandler DestroyEvent;

        public override void execute()
        {
            if (power.HitBoxRectangle.Intersects(player.HitBoxRectangle))
            {
                ((Player)player).ApplyPowerUp((Powerup)power);
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