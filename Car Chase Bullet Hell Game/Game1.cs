using System.Collections.Generic;
using System.Drawing;
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
        private Enemy _gruntA;
        private Enemy _gruntB;
        private List<Enemy> gruntAList = new();
        private int gruntAsDesired = 2; // can change how many grunt A spawn
        private float time = 0f;
        private float time2 = 0f;

        public static GraphicsDevice gd;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            System.Diagnostics.Debug.WriteLine("Starting Game");
        }

        protected override void Initialize()
        {
            //
            gd = GraphicsDevice;

            // increase size of the game window
            _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            _graphics.ApplyChanges();

            _background = new Background(GraphicsDevice);

            // instantiate boss enemy classes
            _bossEnemy = new Enemy();
            CircleMovementPattern _movementPattern = new CircleMovementPattern();
            List<Microsoft.Xna.Framework.Rectangle> _bossAnimationRectangles = new List<Microsoft.Xna.Framework.Rectangle>();

            // first enemy
            _gruntA = new Enemy();
            RightMovementPattern _rightMovementPattern = new RightMovementPattern();
            _gruntA.MovementPattern = _rightMovementPattern;

            // first enemy
            _gruntB = new Enemy();
            LeftMovementPattern _leftMovementPattern = new LeftMovementPattern();
            _gruntB.MovementPattern = _leftMovementPattern;

            int enemyCount = 0;

            /* foreach (Enemy grunt in gruntAList)
            {
                enemyCount++;

                if (enemyCount % 2 == 0)
                {
                    RightMovementPattern _straightMovementPattern = new RightMovementPattern();
                    grunt.MovementPattern = _straightMovementPattern;
                }

                else
                {
                    LeftMovementPattern _straightMovementPattern = new LeftMovementPattern();
                    grunt.MovementPattern = _straightMovementPattern;
                }

            } */

            // prepare boss enemy instantiations
            _bossEnemy.MovementPattern = _movementPattern;

            // create boss animation frames - not using a loop since r2 and r3 don't follow adding 128 to the x
            Microsoft.Xna.Framework.Rectangle r1 = new Microsoft.Xna.Framework.Rectangle(0, 0, 128, 128); // first animation frame
            Microsoft.Xna.Framework.Rectangle r2 = new Microsoft.Xna.Framework.Rectangle(132, 0, 128, 128); // second animation frame
            Microsoft.Xna.Framework.Rectangle r3 = new Microsoft.Xna.Framework.Rectangle(264, 0, 128, 128); // third animation frame
            Microsoft.Xna.Framework.Rectangle r4 = new Microsoft.Xna.Framework.Rectangle(384, 0, 128, 128); // fourth animation frame
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

            Player.Instance.LoadContent(Content, "Cars", new Microsoft.Xna.Framework.Rectangle(0, 0, 157 / 2, 250 / 2));

            // add the same image two times for the background
            for (int i = 0; i < 2; i++)
            {
                Sprite sprite = new Sprite();
                sprite.LoadContent(Content, "Road");
                _background.AddBackground(sprite);
            }

            _bossEnemy.LoadContent(Content, "Boss", _bossEnemy.Animations[0]);

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

            for (int x = 0; x < gruntAsDesired; ++x)
            {
                Enemy newGruntA = new Enemy();
                newGruntA.LoadContent(Content, "Motorcycle");

                newGruntA.DestinationRectangle.Width = 125;
                newGruntA.DestinationRectangle.Height = 125;

                gruntAList.Add(newGruntA);
            }



            // make boss bigger
            _bossEnemy.DestinationRectangle.Width = 512;
            _bossEnemy.DestinationRectangle.Height = 512;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (time > 1f)
            {
                CircleShotPattern csp = new CircleShotPattern(16);
                csp.CreateShots(Content, "01", _bossEnemy.Center);
                _bossEnemy.ShotPatterns.Enqueue(csp);
                time = 0f;
            }


            time2 += (float)gameTime.ElapsedGameTime.TotalSeconds;
            // update gametime with spawn intervals


            _background.Scroll((float)gameTime.ElapsedGameTime.TotalSeconds);
            _gruntA.Update(gameTime);

            _gruntB.Update(gameTime);

            time2 += (float)gameTime.ElapsedGameTime.TotalSeconds;

            /* if (time2 > 2f)
            {
                StraightShot s = new StraightShot();
                s.CreateShots(Content, "CycleBullet", _gruntA.Center);
                _bossEnemy.ShotPatterns.Enqueue(s);

                StraightShot ss = new StraightShot();
                ss.CreateShots(Content, "CycleBullet", _gruntB.Center);
                _bossEnemy.ShotPatterns.Enqueue(ss);
            } */


            _bossEnemy.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            _spriteBatch.Begin();
            _background.Draw(_spriteBatch, gameTime);
            _bossEnemy.Draw(_spriteBatch, gameTime);
            Player.Instance.Draw(_spriteBatch, gameTime);
            _gruntA.Draw(_spriteBatch, gameTime);
            _gruntB.Draw(_spriteBatch, gameTime);
            /* foreach (Enemy grunt in gruntAList)
            {
                grunt.Draw(_spriteBatch, gameTime);
            } */

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}