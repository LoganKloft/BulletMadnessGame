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
        public int? radius { get; set; }
        public int? endRadius { get; set; }
        public int? startDegree { get; set; }
        public int? entity { get; set; }
        public double? direction { get; set; }
        public float? speed { get; set; }
        public float? duration { get; set; }

        public static MovementParams DeepCopy(MovementParams old)
        {
            MovementParams obj = new MovementParams();
            obj.movementPattern = old.movementPattern;
            if (old.point != null)
            {
                obj.point = new List<int>(old.point);
            }
            obj.radius = old.radius;
            obj.endRadius = old.endRadius;
            obj.startDegree = old.startDegree;
            obj.entity = old.entity;
            obj.direction = old.direction;
            obj.speed = old.speed;
            obj.duration = old.duration;

            return obj;
        }
    }
}
