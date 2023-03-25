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

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    // singleton pattern
    internal sealed class Player : Entity
    {
        private static float slow = 250.0f, normal = 500.0f, fast = 750.0f, godMode = 1000.0f;
        private static Player _instance = null;
        private static float speed = normal;
        private static readonly object _lock = new object();
        private static Rectangle screenSize = Game1.gd.Viewport.Bounds;
        private float rightSideMax = screenSize.Width - Game1.playerWidth / 2;
        private float leftSideMax = 0.0f + Game1.playerWidth / 2;
        private float topSideMax = 0.0f + Game1.playerHeight / 2;
        private float bottomSideMax = screenSize.Height - Game1.playerHeight / 2;
        private float health = 3f;
        private float shotSpeed = .25f;
        private float shotTimer = 0f;
        private bool invincibility = false;

        public override event DestroyEventHandler DestroyEvent;

        public delegate void LostLifeEventHandler();
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
            Point current = _instance.Center;
            shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shotTimer > shotSpeed)
            {
                shotTimer = shotSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (shotTimer >= shotSpeed)
                {
                    Shot shot = new Shot(.5);
                    MovementPattern movementPattern = new StraightShot(-Math.PI / 2, 10);
                    shot.MovementPattern = movementPattern;
                    Sprite shotSprite = new Sprite();
                    shotSprite.LoadContent(Game1.content, "01");
                    shot.DestinationRectangle = shotSprite.DestinationRectangle;
                    shot.DestinationRectangleChanged += shotSprite.DestinationRectangleChangedHandler;
                    shot.RotationChanged += shotSprite.RotationChangedHandler;
                    shot.OriginChanged += shotSprite.OriginChangedHandler;
                    shot.DestroyEvent += shotSprite.DestroyEventHandler;
                    DrawController.AddSprite(shotSprite);

                    shot.DestinationRectangle.X = Center.X - shot.DestinationRectangle.Width / 2;
                    shot.DestinationRectangle.Y = Center.Y - shot.DestinationRectangle.Height / 2;
                    shot.NotifyOfDestinationRectangleChange();
                    shot.DestroyEvent += ShotController.DestroyEventHandler;
                    ShotController.AddShot(shot);
                    Command command = new CollisionBulletEnemyCommand(shot);
                    CollisionDetector.AddCommand(command);


                    shotTimer = 0f;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    speed = slow;

                    Vector2 v = new Vector2(temp * speed, 0);

                    if (current.X + temp * speed >= rightSideMax)
                        v.X = rightSideMax - current.X;

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {
                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(temp * speed, 0);

                    if (current.X + temp * speed >= rightSideMax)
                        v.X = rightSideMax - current.X;

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    speed = slow;

                    Vector2 v = new Vector2(temp * speed, 0);

                    if (current.X + temp * speed <= leftSideMax)
                        v.X = leftSideMax - current.X;

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {
                    float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(temp * speed, 0);

                    if (current.X + temp * speed <= leftSideMax)
                    {
                        v.X = leftSideMax - current.X;
                    }

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    speed = slow;

                    Vector2 v = new Vector2(0, temp * speed);

                    if (current.Y + temp * speed <= topSideMax)
                        v.Y = topSideMax - current.Y;

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(current.X + " " + current.Y);
                    float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(0, temp * speed);
                    if (current.Y + temp * speed <= topSideMax)
                        v.Y = topSideMax - current.Y;
                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    speed = slow;

                    Vector2 v = new Vector2(0, temp * speed);

                    if (current.Y + temp * speed > bottomSideMax)
                        v.Y = bottomSideMax - current.Y;

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(current.X + " " + current.Y);

                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(0, temp * speed);

                    if (current.Y + temp * speed > bottomSideMax)
                        v.Y = bottomSideMax - current.Y;

                    _instance.DestinationRectangle.Offset(v);
                    _instance.NotifyOfDestinationRectangleChange();
                }
            }

            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            if (capabilities.IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                System.Diagnostics.Debug.WriteLine(state.ThumbSticks.Left.X + " " + state.ThumbSticks.Left.Y);
                if (capabilities.HasLeftXThumbStick)
                {
                    if (state.ThumbSticks.Left.X > 0)
                    {
                        float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(temp * speed, 0);

                        if (current.X + temp * speed >= rightSideMax)
                            v.X = rightSideMax - current.X;

                        _instance.DestinationRectangle.Offset(v);
                        _instance.NotifyOfDestinationRectangleChange();
                    }
                    if (state.ThumbSticks.Left.X < 0)
                    {
                        float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(temp * speed, 0);

                        if (current.X + temp * speed <= leftSideMax)
                            v.X = leftSideMax - current.X;

                        _instance.DestinationRectangle.Offset(v);
                        _instance.NotifyOfDestinationRectangleChange();
                    }
                }
                if (capabilities.HasLeftYThumbStick)
                {
                    if (state.ThumbSticks.Left.Y > 0)
                    {
                        float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(0, temp * speed);

                        if (current.Y + temp * speed <= topSideMax)
                            v.Y = topSideMax - current.Y;

                        _instance.DestinationRectangle.Offset(v);
                        _instance.NotifyOfDestinationRectangleChange();
                    }
                    if (state.ThumbSticks.Left.Y < 0)
                    {
                        float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(0, temp * speed);

                        if (current.Y + temp * speed > bottomSideMax)
                            v.Y = bottomSideMax - current.Y;

                        _instance.DestinationRectangle.Offset(v);
                        _instance.NotifyOfDestinationRectangleChange();
                    }
                }
            }
        }

        //public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    base.Draw(spriteBatch, gameTime);
        //}
    }
}