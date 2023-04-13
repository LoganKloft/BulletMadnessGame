using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Controller;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.View.Sprite
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

        // Contains the boundaries in the source image for the different animations of the Sprite
        public List<Rectangle> Animations;
        private int animationIndex = 0;

        protected Texture2D _texture;

        public delegate void SpriteDestroyEventHandler(Sprite sprite);
        public event SpriteDestroyEventHandler DestroyEvent;

        // call this function to load an asset image for the sprite
        public void LoadContent(ContentManager content, string asset)
        {
            _texture = content.Load<Texture2D>(asset);
            SourceRectangle = _texture.Bounds;
            DestinationRectangle = new Rectangle(_texture.Bounds.X, _texture.Bounds.Y, _texture.Bounds.Width, _texture.Bounds.Height);
        }

        public void LoadContent(ContentManager content, string asset, Rectangle sourceRectangle)
        {
            _texture = content.Load<Texture2D>(asset);
            SourceRectangle = sourceRectangle;
            DestinationRectangle = new Rectangle(0, 0, sourceRectangle.Width, sourceRectangle.Height);
        }

        // call this function to display the sprite
        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, DestinationRectangle, SourceRectangle, Color, Rotation, Origin, Effects, LayerDepth);

            // change to next animation
            if (Animations != null)
            {
                animationIndex++;
                if (animationIndex == Animations.Count)
                {
                    animationIndex = 0;
                }

                SourceRectangle = Animations[animationIndex];
            }
        }

        public Point Center
        {
            get
            {
                return new Point(DestinationRectangle.X + DestinationRectangle.Width / 2, DestinationRectangle.Y + DestinationRectangle.Height / 2);
            }
        }

        public void DestinationRectangleChangedHandler(Rectangle destinationRectangle)
        {
            DestinationRectangle = destinationRectangle;
        }

        public void RotationChangedHandler(float rotation)
        {
            Rotation = rotation;
        }

        public void OriginChangedHandler(Vector2 origin)
        {
            Origin = origin;
        }

        public void DestroyEventHandler(Entity entity)
        {
            DestroyEvent?.Invoke(this);
        }
    }
}
