using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class Enemy : Entity, IDamageable
    {
        public MovementPattern MovementPattern;

        public override event DestroyEventHandler DestroyEvent;

        //public Queue<ShotPattern> ShotPatterns = new Queue<ShotPattern>();
        float health;

        public Enemy(float health = 1f)
        {
            this.health = health;
        }

        public float Health
        {
            get { return health; }
            set
            {
                health -= value;
                if (health <= 0f)
                {
                    DestroyEvent?.Invoke(this);
                }
            }
        }

        public void TakeDamage(Entity entity)
        {
            if (entity is Shot)
            {
                Shot shot = (Shot)entity;
                Health = Health - shot.Damage;
            }
        }

        public void Update(GameTime gameTime)
        {
            //ShotPattern shotPattern;
            //while (ShotPatterns.TryPeek(out shotPattern) && shotPattern.Finished())
            //{
            //    ShotPatterns.Dequeue();
            //    System.Diagnostics.Debug.WriteLine("ShotPattern Dequeued");
            //}

            if (MovementPattern is not null)
            {
                MovementPattern.Move(gameTime, this);
            }
            //foreach (ShotPattern pattern in ShotPatterns)
            //{
            //    pattern.Update(gameTime);
            //}
        }
    }
}
