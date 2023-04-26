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
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using static Car_Chase_Bullet_Hell_Game.Controller.ShotPattern.PlayerShotPattern;
using static Car_Chase_Bullet_Hell_Game.Controller.ShotPattern.PlayerPowerUpShotPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    // singleton pattern
    internal sealed class Player : Entity
    {
        private static Player _instance = null;
        private static PlayerMovementPatternFactory movementFactory = new PlayerMovementPatternFactory();
        //private static MovementPattern movement = movementFactory.createMovement();
        private static MovementPattern movement = movementFactory.CreateMovementPattern(null);
        private PlayerShotPattern shots = new PlayerShotPattern(new ShotParams { asset = "01"}, 1f);
        private static readonly object _lock = new object();
        private float health = 3f;
        public double invincibilityTime = 2;
        bool _pauseHasBeenUp = true;
        
        private Powerup powerup = null; 

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

        public bool PauseHasBeenUp
        {
            get { return _pauseHasBeenUp; }
            set { _pauseHasBeenUp = value; }
        }

        public bool IsSlow
        {
            get
            {
                if (movement is PlayerMovementPattern)
                {
                    return ((PlayerMovementPattern)movement).IsSlow;
                }

                return false;
            }
        }

        public bool IsCheatMode { get; set; }

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
            if(!IsInvincible && !IsCheatMode)
            {
                if (entity is Shot)
                {
                    Shot shot = (Shot)entity;
                    Health = Health - shot.Damage;
                    shots = new PlayerShotPattern(new ShotParams { asset = "01"}, 1f);
                }

                if (entity is Enemy)
                {
                    Health = Health - 1;
                }
            }
        }

        public void TakeDamage(float damage)
        {
            if (!IsInvincible && !IsCheatMode)
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
            if (IsInvincible)
            {
                invincibilityTime -= gameTime.ElapsedGameTime.TotalSeconds;
                if (invincibilityTime <= 0)
                {
                    IsInvincible = false;
                    invincibilityTime = 2;
                }
            }

            // enable invulnerability
            if (Keyboard.GetState().IsKeyDown(Keys.P) && PauseHasBeenUp)
            {
                IsCheatMode = !IsCheatMode;
                PauseHasBeenUp = false;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.P))
            {
                PauseHasBeenUp = true;
            }

            shots.CreateShots(_instance, gameTime);

            movement.Move(gameTime, _instance);
            
        }

        public void ApplyPowerUp(Powerup p)
        {
            powerup = p;

            Tuple<string, int> powerUpTuple = new(null, 0);

            if (powerup != null)
            {

                powerUpTuple = powerup.powerUpDeterminer();
            }

            if (powerUpTuple.Item1 == "ExtraHealth")
            {
                IsInvincible = true;
                invincibilityTime = 3;
                health += (float)powerUpTuple.Item2;
                // rawController.AddLives();
            }

            else
            {
                shots = new PlayerShotPattern(new ShotParams { asset = "cycleBullet"}, 2f); // FIX THIS TO APPLY EXTRA DAMAGE TO SHOTS
            }

            // System.Console.Write("Hi");
        }

        public bool checkIntersectPowerUp(Powerup p)
        {
            if (p.HitBoxRectangle.Intersects(this.HitBoxRectangle))
            {
                powerup = p;
                return true;
            }

            return false;
        }
    }
}