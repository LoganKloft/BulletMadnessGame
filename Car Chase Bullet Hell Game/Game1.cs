using System.Collections.Generic;
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

            Player.Instance.LoadContent(Content, "Cars", new Rectangle(0, 0, 157 / 2, 250 / 2));

            // add the same image two times for the background
            for (int i = 0; i < 2; i++)
            {
                Sprite sprite = new Sprite();
                sprite.LoadContent(Content, "Road");
                _background.AddBackground(sprite);
            }

            _bossEnemy.LoadContent(Content, "Boss", _bossEnemy.Animations[0]);

            _midBossEnemy.LoadContent(Content, "tank");

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
            _bossEnemy.Draw(_spriteBatch, gameTime);
            _midBossEnemy.Draw(_spriteBatch, gameTime);
            Player.Instance.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}