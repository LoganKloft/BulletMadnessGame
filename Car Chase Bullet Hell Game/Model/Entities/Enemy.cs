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

        public delegate void HealthChangedHandler(float health);
        public event HealthChangedHandler HealthChangedEvent;

        //public Queue<ShotPattern> ShotPatterns = new Queue<ShotPattern>();
        float health;

        public Enemy(float health = 5f)
        {
            this.health = health;
        }

        public float Health
        {
            get { return health; }
            set
            {
                health = value;
                InvokeHealthChangedEvent();
                if (health <= 0f)
                {
                    InvokeDestroyEvent();
                }
            }
        }

        public void InvokeHealthChangedEvent()
        {
            HealthChangedEvent?.Invoke(health);
        }

        public void TakeDamage(Entity entity)
        {
            if (entity is Shot)
            {
                Shot shot = (Shot)entity;
                Health = Health - shot.Damage;
            }
        }

        public void InvokeDestroyEvent()
        {
            DestroyEvent?.Invoke(this);
        }

        public virtual void Update(GameTime gameTime)
        {
            //ShotPattern shotPattern;
            //while (ShotPatterns.TryPeek(out shotPattern) && shotPattern.Finished())
            //{
            //    ShotPatterns.Dequeue();
            //    System.Diagnostics.Debug.WriteLine("ShotPattern Dequeued");
            //}

            if (MovementPattern is not null)
            {
                List<Entity> entity = new List<Entity>();
                entity.Add(this);
                MovementPattern.Move(gameTime, entity);
            }
            //foreach (ShotPattern pattern in ShotPatterns)
            //{
            //    pattern.Update(gameTime);
            //}
        }
    }
}
