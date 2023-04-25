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
        public int? shotCount { get; set; }
        public float? start { get; set; }
        public float? duration { get; set; }
        public float? shootSpeed { get; set; }
        public bool? sticky { get; set; } 
        public double? hitboxPercent { get; set; } // ideally a double in range [0 - 1]

        public static ShotParams DeepCopy(ShotParams old)
        {
            ShotParams obj = new ShotParams();
            obj.shotPattern = old.shotPattern;
            obj.asset = old.asset;
            if (old.point != null)
            {
                obj.point = new List<int>(old.point);
            }
            obj.shotCount = old.shotCount;
            obj.start = old.start;
            obj.duration = old.duration;
            obj.shootSpeed = old.shootSpeed;
            obj.hitboxPercent = old.hitboxPercent;
            obj.sticky = old.sticky;

            return obj;
        }
    }
}
