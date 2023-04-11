using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.EntityParameters
{
    internal class WaveParams
    {
        public int id { get; set; }

        public IList<EnemyParams> enemies { get; set; }
    }
}
