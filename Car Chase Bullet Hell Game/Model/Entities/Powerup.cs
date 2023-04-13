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


    }
}
