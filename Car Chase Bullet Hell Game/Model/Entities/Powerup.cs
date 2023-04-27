using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class Powerup : Entity
    {
        public override event DestroyEventHandler DestroyEvent;

        private String pType;

        public Powerup(String powerUpType)
        {
            pType = powerUpType;
        }

        public void InvokeDestroyEvent()
        {
            DestroyEvent?.Invoke(this);
        }

        public Tuple<string, int> powerUpDeterminer()
        {
            if (pType == "ExtraDamage")
            {
                return Tuple.Create("ExtraDamage", 1);
            }

            else
            {
                return Tuple.Create("ExtraHealth", 1);
            }
        }
    }
}
