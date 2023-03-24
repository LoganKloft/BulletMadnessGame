using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    interface IDamageable
    {
        public void TakeDamage(Entity entity);
    }
}
