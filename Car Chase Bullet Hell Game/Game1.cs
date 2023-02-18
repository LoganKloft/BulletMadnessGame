using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Car_Chase_Bullet_Hell_Game
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Background _background;
        private Enemy _bossEnemy;
        private Enemy _midBossEnemy;
        private float time = 0f;
        private float time2 = 0f;
        private int occurence = 1, shotIndex = 0, numBullets=8;
        private List<Type> shotTypes = new List<Type>();

        public static GraphicsDevice gd;

        public const int widthSize = 1250, heightSize = 800;
        public const int playerWidth = 70;
        public const int playerHeight = 125;
        private int _direction = 1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            System.Diagnostics.Debug.WriteLine("Starting Game");

            shotTypes.Add(typeof(HalfCircleShotPattern));
            shotTypes.Add(typeof(CircleShotPattern));
        }

        protected override void Initialize()
        {
            // display settings
            gd = GraphicsDevice;

            // increase size of the game window
            _graphics.PreferredBackBufferWidth = widthSize;
            _graphics.PreferredBackBufferHeight = heightSize;
            _graphics.ApplyChanges();

            _background = new Background(GraphicsDevice);

            // instantiate boss enemy classes
            _bossEnemy = new Enemy();
            CircleMovementPattern _movementPattern = new CircleMovementPattern(new Point(widthSize / 2, heightSize / 10), heightSize / 10);
            List<Rectangle> _bossAnimationRectangles = new List<Rectangle>();

            _midBossEnemy = new Enemy();
            TriangleMovementPattern circle = new TriangleMovementPattern();
            _midBossEnemy.MovementPattern = circle;

            // prepare boss enemy instantiations
            _bossEnemy.MovementPattern = _movementPattern;

            // create boss animation frames - not using a loop since r2 and r3 don't follow adding 128 to the x
            Rectangle r1 = new Rectangle(0, 0, 128, 128); // first animation frame
            Rectangle r2 = new Rectangle(132, 0, 128, 128); // second animation frame
            Rectangle r3 = new Rectangle(264, 0, 128, 128); // third animation frame
            Rectangle r4 = new Rectangle(384, 0, 128, 128); // fourth animation frame
            _bossAnimationRectangles.Add(r1);
            _bossAnimationRectangles.Add(r2);
            _bossAnimationRectangles.Add(r3);
            _bossAnimationRectangles.Add(r4);
            _bossEnemy.Animations = _bossAnimationRectangles;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Player.Instance.LoadContent(Content, "Cars", new Rectangle(0, 0, playerWidth, playerHeight));

            // add the same image two times for the background
            for (int i = 0; i < 2; i++)
            {
                Sprite sprite = new Sprite();
                sprite.LoadContent(Content, "Road");
                _background.AddBackground(sprite);
            }

            _bossEnemy.LoadContent(Content, "Boss", _bossEnemy.Animations[0]);

            _midBossEnemy.LoadContent(Content, "tank");

            // center main player
            Player.Instance.DestinationRectangle.X = widthSize / 2 - ((157 / 2) / 2);
            Player.Instance.DestinationRectangle.Y = heightSize / 2 - ((250 / 2) / 2);

            // make the player smaller
            Player.Instance.DestinationRectangle.Width = Player.Instance.DestinationRectangle.Width / 2;
            Player.Instance.DestinationRectangle.Height = Player.Instance.DestinationRectangle.Height / 2;

            // make boss bigger
            _bossEnemy.DestinationRectangle.Width = heightSize / 4;
            _bossEnemy.DestinationRectangle.Height = heightSize / 4;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            time2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > .4f)
            {
                CircleShotPattern csp = new CircleShotPattern(16);
                csp.CreateShots(Content, "01", _bossEnemy.Center);
                _bossEnemy.ShotPatterns.Enqueue(csp);
                time = 0f;
            }

            CircleMovementPattern cmp = _bossEnemy.MovementPattern as CircleMovementPattern;
            if(cmp != null)
            {
                Point pivotPoint = cmp.PivotPoint;

                if (pivotPoint.X < widthSize / 4)
                {
                    _direction = 1;
                    pivotPoint.X = widthSize / 4;
                }
                
                if (pivotPoint.X > widthSize - widthSize / 4)
                {
                    _direction = -1;
                    pivotPoint.X = widthSize - widthSize / 4;
                }

                pivotPoint.X += 2 * _direction;
                cmp.PivotPoint = pivotPoint;
            }

            Player.Instance.Update(gameTime);

            // Mid-Boss Shot
            if (time2 > 2f)
            {
                if((occurence-1)%3==0 && occurence!=1)
                {
                    if (shotIndex == 0)
                    {
                        shotIndex = 1;
                        numBullets = 16;
                    }
                    else
                    {
                        shotIndex = 0;
                        numBullets = 8;
                    }
                }
                var constructors = shotTypes[shotIndex].GetConstructors();
                object pattern = constructors[0].Invoke(new object[] { numBullets });
                if(pattern.GetType() == typeof(HalfCircleShotPattern))
                {
                    HalfCircleShotPattern half = (HalfCircleShotPattern)pattern;
                    half.CreateShots(Content, "bullet1", _midBossEnemy.Center);
                    _midBossEnemy.ShotPatterns.Enqueue(half);
                }
                else if(pattern.GetType() == typeof(CircleShotPattern))
                {
                    CircleShotPattern circle = (CircleShotPattern)pattern;
                    circle.CreateShots(Content, "bullet2", _midBossEnemy.Center);
                    _midBossEnemy.ShotPatterns.Enqueue(circle);
                }
                this.time2 = 0f;
                this.occurence++;
            }

            _background.Scroll((float)gameTime.ElapsedGameTime.TotalSeconds);
            _bossEnemy.Update(gameTime);
            _midBossEnemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _background.Draw(_spriteBatch, gameTime);
            _bossEnemy.Draw(_spriteBatch, gameTime);
            _midBossEnemy.Draw(_spriteBatch, gameTime);
            Player.Instance.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}