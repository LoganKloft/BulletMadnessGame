using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.EntityParameters
{
    internal class EnemyParams
    {
        public string asset { get; set; }
        public int? health { get; set; }
        public int? id { get; set; }
        public float? start { get; set; }
        public float? duration { get; set; }
        public IList<int> dimensions { get; set; }
        public IList<MovementParams> movementItems { get; set; }
        public IList<ShotParams> shotItems { get; set; }
        public bool healthBar { get; set; }
        public int score { get; set; }
    }
}
