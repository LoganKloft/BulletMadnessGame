using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class SpiralSpawner : Enemy
    {
        public Enemy orbitEnemy;

        public SpiralSpawner(EnemyParams enemyParams): base(enemyParams)
        {
            this.enemyParams = enemyParams;
            this.Health = enemyParams.health != null ? (float)enemyParams.health : this.Health;
        }

        public void setOrbit(ref Enemy enemy)
        {
            this.orbitEnemy = enemy;
            this.orbitEnemy.DestroyEvent += DestroyHandler;
        }

        public void DestroyHandler(Entity entity)
        {
            this.InvokeDestroyEvent();
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
