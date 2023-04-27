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
        
        public int Score = 0;

        private int hitCounter = 0;

        private Powerup powerup = null;
        private float powerupTime = 0f;
        private bool powerupActive = false;

        private bool invincibility = false;

        public override event DestroyEventHandler DestroyEvent;

        public event LostLifeEventHandler LostLife;

        public delegate void GainLifeEventHandler();
        public event GainLifeEventHandler GainLife;

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
                if (value < health)
                {
                    health = value;
                    LostLife?.Invoke();

                    if (health <= 0f)
                    {
                        InvokeDestroyEvent();
                    }
                }
                else if (value > health)
                {
                    health = value;
                    GainLife?.Invoke();
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

                    if (hitCounter == 0)
                    {
                        Health = Health - shot.Damage;
                        shots = new PlayerShotPattern(new ShotParams { asset = "01" }, 1f);
                    }

                    else
                    {
                        --hitCounter;
                        DrawController.RemoveShield();
                    }
                        
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

            if (!IsInvincible && DrawController.HasEffect("Invincible"))
            {
                DrawController.RemoveEffect("Invincible");
            }
            else if (IsInvincible && !DrawController.HasEffect("Invincible"))
            {
                DrawController.AddEffect("Invincible", "Shield");
            }

            if (!IsSlow && DrawController.HasEffect("Slow"))
            {
                DrawController.RemoveEffect("Slow");
            }
            else if (IsSlow && !DrawController.HasEffect("Slow"))
            {
                DrawController.AddEffect("Slow", "Turtle");
            }

            if (!IsCheatMode && DrawController.HasEffect("Cheat"))
            {
                DrawController.RemoveEffect("Cheat");
            }
            else if (IsCheatMode && !DrawController.HasEffect("Cheat"))
            {
                DrawController.AddEffect("Cheat", "Shield");
            }

            if (!powerupActive && DrawController.HasEffect("Damage"))
            {
                DrawController.RemoveEffect("Damage");
            }
            else if (powerupActive && !DrawController.HasEffect("Damage"))
            {
                DrawController.AddEffect("Damage", "pUpDamage");
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

            if (powerupActive && powerupTime <= 0f)
            {
                powerupTime = 0f;
                powerupActive = false;
                shots = new PlayerShotPattern(new ShotParams { asset = "01" }, 1f);
            }

            if (powerupTime > 0f)
            {
                powerupTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            shots.CreateShots(_instance, gameTime);
            List<Entity> entity = new List<Entity>();
            entity.Add(_instance);

            movement.Move(gameTime, entity);
            
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
                ++hitCounter;
                DrawController.AddShield();
                // health += (float)powerUpTuple.Item2;
                // rawController.AddLives();
            }

            else
            {
                shots = new PlayerShotPattern(new ShotParams { asset = "cycleBullet"}, 2f); // FIX THIS TO APPLY EXTRA DAMAGE TO SHOTS
                powerupActive = true;
                powerupTime += 5f;
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