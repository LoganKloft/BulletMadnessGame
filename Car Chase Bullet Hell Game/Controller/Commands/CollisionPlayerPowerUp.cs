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
        private Powerup power;
        private Entity player;

        public CollisionPlayerPowerUpCommand(Entity x, Entity y)
        { 
            if (x is Powerup)
            {
                power = (Powerup)x;
                player = y;
            }

            else
            {
                power = (Powerup)y;
                player = x;
            }

            power.DestroyEvent += DestroyEventHandler;
        }

        public override event DestroyCommandEventHandler DestroyEvent;

        public override void execute()
        {
            if (power.DestinationRectangle.Intersects(player.DestinationRectangle))
            {
                ((Player)player).ApplyPowerUp((Powerup)power);
                power.InvokeDestroyEvent();
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