using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.EntityParameters
{
    internal class MovementParams
    {
        public string movementPattern { get; set; }
        public IList<int> point { get; set; }
        public int radius { get; set; }
        public int endRadius { get; set; }
        public int startDegree { get; set; }
        public int entity { get; set; }
        public double direction { get; set; }
        public double speed { get; set; }
        public float duration { get; set; }
    }
}
