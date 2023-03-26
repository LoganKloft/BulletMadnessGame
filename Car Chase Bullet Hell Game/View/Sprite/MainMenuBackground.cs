using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.View.Sprite
{
    internal class MainMenuBackground
    {
        private Queue<Sprite> _backgrounds = new Queue<Sprite>();
        private Sprite _lastEnqueued;
        private float _pauseTime = 0f;
        private GraphicsDevice _graphicsDevice;

        public MainMenuBackground(GraphicsDevice graphicsDevice)
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

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Sprite sprite in _backgrounds)
            {
                sprite.Draw(spriteBatch, gameTime);
            }
        }
    }
}
