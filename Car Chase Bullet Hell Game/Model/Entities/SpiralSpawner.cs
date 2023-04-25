using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class SpiralSpawner : Enemy
    {
        public Enemy orbitEnemy;

        public void setOrbit(ref Enemy enemy)
        {
            this.orbitEnemy = enemy;
        }
        public override void Update(GameTime gameTime)
        {
            if(MovementPattern!=null)
            {
                List<Entity> entity = new List<Entity>();
                entity.Add(this);
                entity.Add(orbitEnemy);
                MovementPattern.Move(gameTime, entity);
            }

        }
    }
}
