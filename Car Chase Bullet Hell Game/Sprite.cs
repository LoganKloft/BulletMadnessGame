using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class Sprite
    {
        // the size and location of the sprite
        public Rectangle DestinationRectangle;

        // the part of the image used for the sprite
        public Rectangle SourceRectangle;
        public Color Color = Color.White;
        public float Rotation = 0f;
        public Vector2 Origin = Vector2.Zero;
        public SpriteEffects Effects = SpriteEffects.None;
        public float LayerDepth = 0f;

        private Texture2D _texture;

        // call this function to load an asset image for the sprite
        public void LoadContent(ContentManager content, string asset)
        {
            _texture = content.Load<Texture2D>(asset);
            SourceRectangle = _texture.Bounds;
            DestinationRectangle = new Rectangle(_texture.Bounds.X, _texture.Bounds.Y, _texture.Bounds.Width, _texture.Bounds.Height);
        }

        // call this function to display the sprite
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, DestinationRectangle, SourceRectangle, Color, Rotation, Origin, Effects, LayerDepth);
        }
    }
}
