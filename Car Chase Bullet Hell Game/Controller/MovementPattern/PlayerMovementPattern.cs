using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.MovementPattern
{
    internal class PlayerMovementPattern : MovementPattern
    {
        private static float slow = 250.0f, normal = 500.0f, fast = 750.0f, godMode = 1000.0f;
        private static float speed = normal;
        private float rightSideMax = screenSize.Width - Game1.playerWidth / 2;
        private float leftSideMax = 0.0f + Game1.playerWidth / 2;
        private float topSideMax = 0.0f + Game1.playerHeight / 2;
        private float bottomSideMax = screenSize.Height - Game1.playerHeight / 2;
        private static Rectangle screenSize = Game1.gd.Viewport.Bounds;

        public override void Move(GameTime gameTime, Entity entity)
        {
            Point current = entity.Center;

            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    speed = slow;

                    Vector2 v = new Vector2(temp * speed, 0);

                    if (current.X + temp * speed >= rightSideMax)
                        v.X = rightSideMax - current.X;

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {
                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(temp * speed, 0);

                    if (current.X + temp * speed >= rightSideMax)
                        v.X = rightSideMax - current.X;

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();
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

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();

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

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();
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

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {
                    float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(0, temp * speed);
                    if (current.Y + temp * speed <= topSideMax)
                        v.Y = topSideMax - current.Y;
                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();
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

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();

                    speed = normal;
                }
                else
                {

                    float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    Vector2 v = new Vector2(0, temp * speed);

                    if (current.Y + temp * speed > bottomSideMax)
                        v.Y = bottomSideMax - current.Y;

                    entity.DestinationRectangle.Offset(v);
                    entity.NotifyOfDestinationRectangleChange();
                }
            }

            GamePadCapabilities capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            if (capabilities.IsConnected)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (capabilities.HasLeftXThumbStick)
                {
                    if (state.ThumbSticks.Left.X > 0)
                    {
                        float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(temp * speed, 0);

                        if (current.X + temp * speed >= rightSideMax)
                            v.X = rightSideMax - current.X;

                        entity.DestinationRectangle.Offset(v);
                        entity.NotifyOfDestinationRectangleChange();
                    }
                    if (state.ThumbSticks.Left.X < 0)
                    {
                        float temp = -1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(temp * speed, 0);

                        if (current.X + temp * speed <= leftSideMax)
                            v.X = leftSideMax - current.X;

                        entity.DestinationRectangle.Offset(v);
                        entity.NotifyOfDestinationRectangleChange();
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

                        entity.DestinationRectangle.Offset(v);
                        entity.NotifyOfDestinationRectangleChange();
                    }
                    if (state.ThumbSticks.Left.Y < 0)
                    {
                        float temp = 1.0f * (float)gameTime.ElapsedGameTime.TotalSeconds;

                        Vector2 v = new Vector2(0, temp * speed);

                        if (current.Y + temp * speed > bottomSideMax)
                            v.Y = bottomSideMax - current.Y;

                        entity.DestinationRectangle.Offset(v);
                        entity.NotifyOfDestinationRectangleChange();
                    }
                }
            }
        }
    }
}
