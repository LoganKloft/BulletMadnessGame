using System;
using System.Collections.Generic;
using System.Reflection;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Car_Chase_Bullet_Hell_Game.Controller.Spawn;
using System.Text.Json;
using System.IO;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    public class Game1 : Game
    {
        public enum GameState
        {
            StartMenu,
            Playing,
            Pause,
            Exit
        }
        private Button _startButton;
        private Button _startMenu;
        private Vector2 _startMenuPosition;
        private Vector2 _startButtonPosition;
        private MouseState _mouseState;
        private bool _mouseLeftPressed;

        private GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;

        public static GraphicsDevice gd;

        public const int widthSize = 1250, heightSize = 800;
        public const int playerWidth = 70;
        public const int playerHeight = 125;

        private MainMenuBackground _mainMenuBackground;
        private Spawner spawner;
        private Sprite _playerSprite;
        private Background _background;
        private LifeItem _lives;

        public static ContentManager content;

        public static GameState gameState;

        public static Song song;
        List<SoundEffect> soundEffects;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            gameState = GameState.StartMenu;
            soundEffects = new List<SoundEffect>();

            System.Diagnostics.Debug.WriteLine("Starting Game");
        }

        protected override void Initialize()
        {
            // display settings
            gd = GraphicsDevice;
            content = Content;

            _startButtonPosition = new Vector2(170, 400);
            _mouseState = MouseButtons.GetState();
            _mouseLeftPressed = false;

            // set size of the game window
            _graphics.PreferredBackBufferWidth = widthSize;
            _graphics.PreferredBackBufferHeight = heightSize;
            _graphics.ApplyChanges();

            _background = new Background(gd);
            _mainMenuBackground = new MainMenuBackground(gd);

            _playerSprite = new Sprite();
            //DrawController.AddSprite(_playerSprite);
            Player.Instance.DestinationRectangleChanged += _playerSprite.DestinationRectangleChangedHandler;
            Player.Instance.RotationChanged += _playerSprite.RotationChangedHandler;
            Player.Instance.OriginChanged += _playerSprite.OriginChangedHandler;
            _lives = new LifeItem(Content, "heart");

            // test code for using json to control spawning
            string cwd = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = cwd + "../../../Levels/level1.json";
            string jsonString = File.ReadAllText(fileName);
            LevelParams levelParams = JsonSerializer.Deserialize<LevelParams>(jsonString);

            spawner = new Spawner(levelParams);
            spawner.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _startButton = new Button(Content.Load<Texture2D>("StartButton"), Content.Load<Texture2D>("StartButton"), new Point(916, 289), _startButtonPosition, "Start Button", 33, true, 1.0f);

            gameState = GameState.StartMenu;

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Player.Instance.LoadContent(Content, "Cars", new Rectangle(0, 0, playerWidth, playerHeight));
            _playerSprite.LoadContent(Content, "Cars", new Rectangle(0, 0, playerWidth, playerHeight));

            // center main player
            Player.Instance.DestinationRectangle.X = widthSize / 2 - 157 / 2 / 2;
            Player.Instance.DestinationRectangle.Y = heightSize / 2 - 250 / 2 / 2;

            // make the player smaller
            Player.Instance.DestinationRectangle.Width = playerWidth / 2;
            Player.Instance.DestinationRectangle.Height = playerHeight / 2;
            Player.Instance.NotifyOfDestinationRectangleChange();

            song = Content.Load<Song>("song");
            
            //MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            soundEffects.Add(Content.Load<SoundEffect>("bulletNoise"));

            //soundEffects[0].Play();
            //var instance = soundEffects[0].CreateInstance();
            //instance.IsLooped = true;
            //instance.Play();

            // add the same image two times for the background
            for (int i = 0; i < 2; i++)
            {
                Sprite sprite = new Sprite();
                sprite.LoadContent(Content, "Road");
                _background.AddBackground(sprite);
            }

            Sprite temp = new Sprite();
            temp.LoadContent(Content, "MainMenuBackground");
            _mainMenuBackground.AddBackground(temp);

            Sprite gameLost = new Sprite();
            gameLost.LoadContent(Content, "GameOver");

            Sprite gameWon = new Sprite();
            gameWon.LoadContent(Content, "win");

            Sprite slowMode = new Sprite();
            slowMode.LoadContent(Content, "Turtle");
            slowMode.DestinationRectangle = new Rectangle(widthSize - 64, heightSize - 64, 64, 64);

            Sprite invulnMode = new Sprite();
            invulnMode.LoadContent(Content, "Shield");
            invulnMode.DestinationRectangle = new Rectangle(widthSize - 64, heightSize - 128, 64, 64);

            DrawController.gameLost = gameLost;
            DrawController.gameWon = gameWon;
            DrawController.background = _background;
            DrawController.playerSprite = _playerSprite;
            DrawController.slowMode = slowMode;
            DrawController.invulnMode = invulnMode;
        }

        //void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        //{
        //    MediaPlayer.Play(song);
        //}

        public void HandleInput(GameTime gameTime)
        {
            _mouseState = MouseButtons.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (MouseButtons.HasNotBeenPressed(true))
                {
                    _mouseLeftPressed = true;
                }
            }
        }

        private bool CheckIfButtonWasClicked()
        {
            if (_mouseState.X >= _startButton.Position.X && _mouseState.X <= (_startButton.Position.X + _startButton.Dimensions.X))
            {
                if (_mouseState.Y >= _startButton.Position.Y && _mouseState.Y <= (_startButton.Position.Y + _startButton.Dimensions.Y) && _startButton.Visible)
                {
                    return true;
                }
            }
            return false;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            /*if (Keyboard.GetState().IsKeyDown(Keys.P))
            {
                if (gameState != GameState.Pause && gameState != GameState.StartMenu)
                {
                    gameState = GameState.Pause;
                }
                else if (gameState != GameState.Pause && gameState != GameState.StartMenu)
                {
                    gameState = GameState.Playing;
                }
            }*/
            HandleInput(gameTime);
            _startButton.UpdateButton();

            if (gameState == GameState.StartMenu)
            {
                if (_mouseLeftPressed)
                {
                    _mouseLeftPressed = false;

                    if (CheckIfButtonWasClicked())
                    {
                        _startButton.Clicked();
                        MediaPlayer.Play(song);
                    }
                }
            }

            else if (gameState == GameState.Playing)
            {
                spawner.Update(gameTime);
                Player.Instance.Update(gameTime);
                ShotController.Update(gameTime);
                CollisionDetector.DetectCollisions();
                _background.Scroll((float)gameTime.ElapsedGameTime.TotalSeconds);

                if (spawner.CheckGameOver() == true)
                {
                    MediaPlayer.Stop();
                    return;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Rectangle sourceRectangle = new Rectangle(0, 0, _startButton.CellWidth, _startButton.CellHeight);
            Rectangle destinationRectangle = new Rectangle((int)_startButton.Position.X, (int)_startButton.Position.Y, _startButton.CellWidth, _startButton.CellHeight);

            _spriteBatch.Begin();

            if (gameState == GameState.StartMenu)
            {
                _mainMenuBackground.Draw(_spriteBatch, gameTime);
                _spriteBatch.Draw(_startButton.Texture, destinationRectangle, sourceRectangle, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            }
            else if (gameState == GameState.Playing)
            {
                DrawController.Draw(_spriteBatch, gameTime, spawner);
            }

            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}