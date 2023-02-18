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
        private Enemy _gruntA;
        private Enemy _gruntB;

        private float time = 0f;
        private float time2 = 0f, gameUpdate=0f, gameDraw=0f, time3 = 0f;
        private int occurence = 1, shotIndex = 0, numBullets=8;
        private List<Type> shotTypes = new List<Type>();
        private OffScreenMovementPattern offScreen = new OffScreenMovementPattern();

        public static GraphicsDevice gd;

        public const int widthSize = 1250, heightSize = 800;
        public const int playerWidth = 70;
        public const int playerHeight = 125;

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
            CircleMovementPattern _movementPattern = new CircleMovementPattern();
            List<Rectangle> _bossAnimationRectangles = new List<Rectangle>();

            _gruntA = new Enemy();
            RightMovementPattern _rightMovementPattern = new RightMovementPattern();
            _gruntA.MovementPattern = _rightMovementPattern;

            // first enemy
            _gruntB = new Enemy();
            LeftMovementPattern _leftMovementPattern = new LeftMovementPattern();
            _gruntB.MovementPattern = _leftMovementPattern;


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

            // load grunt A
            _gruntA.LoadContent(Content, "Motorcycle");

            // make grunt A smaller
            _gruntA.DestinationRectangle.Width = 125;
            _gruntA.DestinationRectangle.Height = 125;

            // load grunt A
            _gruntB.LoadContent(Content, "Motorcycle");

            // make grunt A smaller
            _gruntB.DestinationRectangle.Width = 125;
            _gruntB.DestinationRectangle.Height = 125;


            _bossEnemy.LoadContent(Content, "Boss", _bossEnemy.Animations[0]);

            _midBossEnemy.LoadContent(Content, "tank");

            // center main player
            Player.Instance.DestinationRectangle.X = widthSize / 2 - ((157 / 2) / 2);
            Player.Instance.DestinationRectangle.Y = heightSize / 2 - ((250 / 2) / 2);

            // make boss bigger
            _bossEnemy.DestinationRectangle.Width = 512;
            _bossEnemy.DestinationRectangle.Height = 512;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameUpdate = (float)gameTime.TotalGameTime.TotalSeconds;

            if((int)gameUpdate == 90)
            {
                CircleMovementPattern circ = new CircleMovementPattern();
                _bossEnemy.MovementPattern = circ;
            }
            //Final-Boss shots and unpdate only after 10 seconds of game play and again at 20 seconds to end
            if ((gameUpdate>40 && gameUpdate<=75) || gameUpdate>=90)
            {
                time += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time > 1f)
                {
                    CircleShotPattern csp = new CircleShotPattern(16);
                    csp.CreateShots(Content, "01", _bossEnemy.Center);
                    _bossEnemy.ShotPatterns.Enqueue(csp);
                    time = 0f;
                }
                _bossEnemy.Update(gameTime);
            }

            Player.Instance.Update(gameTime);
            // Mid-Boss Shot and update only after 5 seconds of game play
            if (gameUpdate > 5 && gameUpdate < 40)
            {
                time2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (time2 > 2f)
                {
                    if ((occurence - 1) % 3 == 0 && occurence != 1)
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
                    //Determine what type of bullet pattern we should be firing.
                    var constructors = shotTypes[shotIndex].GetConstructors();
                    object pattern = constructors[0].Invoke(new object[] { numBullets });
                    if (pattern.GetType() == typeof(HalfCircleShotPattern))
                    {
                        HalfCircleShotPattern half = (HalfCircleShotPattern)pattern;
                        half.CreateShots(Content, "bullet1", _midBossEnemy.Center);
                        _midBossEnemy.ShotPatterns.Enqueue(half);
                    }
                    else if (pattern.GetType() == typeof(CircleShotPattern))
                    {
                        CircleShotPattern circle = (CircleShotPattern)pattern;
                        circle.CreateShots(Content, "bullet2", _midBossEnemy.Center);
                        _midBossEnemy.ShotPatterns.Enqueue(circle);
                    }
                    this.time2 = 0f;
                    this.occurence++;
                }

                _midBossEnemy.Update(gameTime);
            }

            //Move mid boss enemy of screen at the correct time
            if (gameUpdate >= 40 && gameUpdate < 50)
            {
                _midBossEnemy.MovementPattern = offScreen;
                _midBossEnemy.Update(gameTime);
            }

            //Move final boss enemy off the screen when time is between 2 numbers.
            if (gameUpdate >= 75 && gameUpdate < 80)
            {
                _bossEnemy.MovementPattern = offScreen;
                _bossEnemy.Update(gameTime);
            }

            if (gameUpdate >= 0 && gameUpdate <= 35)
            {
                time3 += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (time3 > 2f)
                {
                    HalfCircleShotPattern half = new(20);
                    half.CreateShots(Content, "bullet03", _gruntA.Center);
                    half.CreateShots(Content, "bullet03", _gruntB.Center);

                    _gruntA.ShotPatterns.Enqueue(half);
                    _gruntB.ShotPatterns.Enqueue(half);

                    this.time3 = 0f;
                }

                _gruntA.Update(gameTime);
                _gruntB.Update(gameTime);
            }

            _background.Scroll((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            gameDraw = (float)gameTime.TotalGameTime.TotalSeconds;

            _spriteBatch.Begin();
            _background.Draw(_spriteBatch, gameTime);

            _gruntA.Draw(_spriteBatch, gameTime);
            _gruntB.Draw(_spriteBatch, gameTime);


            if(gameDraw>40)
            {
                _bossEnemy.Draw(_spriteBatch, gameTime);
                _bossEnemy.Draw(_spriteBatch, gameTime);
            }

            if(gameDraw > 5 && gameDraw<=50)
            {
                _midBossEnemy.Draw(_spriteBatch, gameTime);
            }
            Player.Instance.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}