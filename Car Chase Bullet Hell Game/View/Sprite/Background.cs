using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.View.Sprite
{
    internal class Background
    {
        public float ScrollSpeed = 1000f;
        private Queue<Sprite> _backgrounds = new Queue<Sprite>();
        private Sprite _lastEnqueued;
        private float _pauseTime = 0f;
        private GraphicsDevice _graphicsDevice;

        public Background(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void AddBackground(Sprite sprite)
        {
            sprite.DestinationRectangle = _graphicsDevice.Viewport.Bounds;
            if (_backgrounds.Count > 0)
            {
                sprite.DestinationRectangle.Offset(0, _lastEnqueued.DestinationRectangle.Y - sprite.DestinationRectangle.Height);
            }
            _backgrounds.Enqueue(sprite);
            _lastEnqueued = sprite;
        }

        public void Scroll(float elapsed)
        {
            // only scroll if there are more than one background
            if (_backgrounds.Count > 1)
            {
                foreach (Sprite sprite in _backgrounds)
                {
                    sprite.DestinationRectangle.Offset(0f, ScrollSpeed * elapsed);
                }

                if (_backgrounds.Peek().DestinationRectangle.Top > _graphicsDevice.Viewport.Height)
                {
                    Sprite sprite = _backgrounds.Dequeue();
                    sprite.DestinationRectangle = _lastEnqueued.DestinationRectangle;
                    sprite.DestinationRectangle.Offset(0f, -sprite.DestinationRectangle.Height);
                    _lastEnqueued = sprite;
                    _backgrounds.Enqueue(sprite);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Sprite sprite in _backgrounds)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
