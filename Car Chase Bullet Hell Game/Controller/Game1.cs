using System;
using System.Collections.Generic;
using System.Reflection;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static GraphicsDevice gd;

        public const int widthSize = 1250, heightSize = 800;
        public const int playerWidth = 70;
        public const int playerHeight = 125;

        private Spawner spawner;
        private Sprite _playerSprite;
        private Background _background;

        public static ContentManager content;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            System.Diagnostics.Debug.WriteLine("Starting Game");
        }

        protected override void Initialize()
        {
            // display settings
            gd = GraphicsDevice;
            content = Content;

            // increase size of the game window
            _graphics.PreferredBackBufferWidth = widthSize;
            _graphics.PreferredBackBufferHeight = heightSize;
            _graphics.ApplyChanges();

            _background = new Background(gd);

            _playerSprite = new Sprite();
            //DrawController.AddSprite(_playerSprite);
            Player.Instance.DestinationRectangleChanged += _playerSprite.DestinationRectangleChangedHandler;
            Player.Instance.RotationChanged += _playerSprite.RotationChangedHandler;
            Player.Instance.OriginChanged += _playerSprite.OriginChangedHandler;

            spawner = new Spawner(Content);
            spawner.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Player.Instance.LoadContent(Content, "Cars", new Rectangle(0, 0, playerWidth, playerHeight));
            _playerSprite.LoadContent(Content, "Cars", new Rectangle(0, 0, playerWidth, playerHeight));

            // center main player
            Player.Instance.DestinationRectangle.X = widthSize / 2 - 157 / 2 / 2;
            Player.Instance.DestinationRectangle.Y = heightSize / 2 - 250 / 2 / 2;

            // make the player smaller
            Player.Instance.DestinationRectangle.Width = Player.Instance.DestinationRectangle.Width / 2;
            Player.Instance.DestinationRectangle.Height = Player.Instance.DestinationRectangle.Height / 2;
            Player.Instance.NotifyOfDestinationRectangleChange();

            // add the same image two times for the background
            for (int i = 0; i < 2; i++)
            {
                Sprite sprite = new Sprite();
                sprite.LoadContent(Content, "Road");
                _background.AddBackground(sprite);
            }

            DrawController.background = _background;
            DrawController.playerSprite = _playerSprite;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            spawner.Update(gameTime);

            Player.Instance.Update(gameTime);
            _background.Scroll((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            DrawController.Draw(_spriteBatch, gameTime);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}