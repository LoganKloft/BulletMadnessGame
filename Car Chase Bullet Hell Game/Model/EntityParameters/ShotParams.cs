using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.EntityParameters
{
    internal class ShotParams
    {
        public string shotPattern { get; set; }
        public string asset { get; set; }
        public IList<int> point { get; set; }
        public int shotCount { get; set; }
        public float start { get; set; }
        public float duration { get; set; }
        public float shootSpeed { get; set; }
    }
}
