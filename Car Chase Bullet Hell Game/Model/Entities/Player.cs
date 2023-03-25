using Car_Chase_Bullet_Hell_Game.Controller;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using static Car_Chase_Bullet_Hell_Game.Controller.ShotPattern.PlayerShotPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    // singleton pattern
    internal sealed class Player : Entity
    {
        private static Player _instance = null;
        private static PlayerMovementPattern movement = new PlayerMovementPattern();
        private static PlayerShotPattern shots = new PlayerShotPattern();
        private static readonly object _lock = new object();
        private float health = 3f;
        
        private bool invincibility = false;

        public override event DestroyEventHandler DestroyEvent;

        public event LostLifeEventHandler LostLife;

        Player() : base(.5) { }
        public static Player Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Player();
                        }
                        return _instance;
                    }
                }
                return _instance;
            }
        }

        public float Health
        {
            get { return health; }
            set
            {
                health = value;
                LostLife?.Invoke();
                if (health <= 0f)
                {
                    InvokeDestroyEvent();
                }
            }
        }

        public bool IsInvincible
        {
            get { return invincibility; }
            set { invincibility = value; }
        }

        public void TakeDamage(Entity entity)
        {
            if(invincibility==false)
            {
                if (entity is Shot)
                {
                    Shot shot = (Shot)entity;
                    Health = Health - shot.Damage;
                }

                if (entity is Enemy)
                {
                    Health = Health - 1;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (invincibility == false)
            {
                Health = Health - damage;
            }
        }

        public void InvokeDestroyEvent()
        {
            DestroyEvent?.Invoke(this);
        }

        public void Update(GameTime gameTime)
        {
            shots.CreateShots(_instance, gameTime);

            movement.Move(gameTime, _instance);
            
        }
    }
}