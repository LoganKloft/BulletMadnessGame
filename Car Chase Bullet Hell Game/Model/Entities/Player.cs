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

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    // singleton pattern
    internal sealed class Player : Entity
    {
        private static float slow = 250.0f, normal = 500.0f, fast = 750.0f, godMode = 1000.0f;
        private static Player _instance;
        private static float speed = normal;
        private static readonly object _lock = new object();
        private static Rectangle screenSize = Game1.gd.Viewport.Bounds;
        private float rightSideMax = screenSize.Width - Game1.playerWidth / 2;
        private float leftSideMax = 0.0f + Game1.playerWidth / 2;
        private float topSideMax = 0.0f + Game1.playerHeight / 2;
        private float bottomSideMax = screenSize.Height - Game1.playerHeight / 2;

        Player() { }
        public static Player Instance
        {
            get
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
        }

        public void Update(GameTime gameTime)
        {
            Point current = _instance.Center;
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