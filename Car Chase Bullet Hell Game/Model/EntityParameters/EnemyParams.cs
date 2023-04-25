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
        public int? id { get; set; } // not currently used for anything
        public float? start { get; set; } // when the enemy should appear, from the start of the game
        public float? duration { get; set; } // how long the enemy should stay on screen
        public float? interval { get; set; } // how often to spawn an enemy
        public int? intervals { get; set; } // how many intervals
        public bool healthBar { get; set; }

        public bool underlyingEnemy { get; set; }
        public EnemyParams enemy { get; set; } // for spiral spawner
        public IList<int> dimensions { get; set; } // the DestinationRectangle
        public IList<MovementParams> movementItems { get; set; }
        public IList<ShotParams> shotItems { get; set; }

        public static EnemyParams DeepCopy(EnemyParams old)
        {
            EnemyParams obj = new EnemyParams();

            obj.enemy = old.enemy;
            if(obj.enemy!=null)
            {
                obj.underlyingEnemy = true;
            }

            obj.asset = old.asset;
            obj.health = old.health;
            obj.id = old.id;
            obj.start = old.start;
            obj.duration = old.duration;
            obj.interval = old.interval;
            obj.intervals = old.intervals;
            obj.healthBar = old.healthBar;
            
            if (old.dimensions != null)
            {
                obj.dimensions = new List<int>(old.dimensions);
            }

            if (old.movementItems != null)
            {
                obj.movementItems = new List<MovementParams>();
                foreach (MovementParams movementParam in old.movementItems)
                {
                    obj.movementItems.Add(MovementParams.DeepCopy(movementParam));
                }
            }

            if (old.shotItems != null)
            {
                obj.shotItems = new List<ShotParams>();
                foreach (ShotParams shotParam in old.shotItems)
                {
                    obj.shotItems.Add(ShotParams.DeepCopy(shotParam));
                }
            }

            return obj;
        }
    }
}
