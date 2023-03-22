using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Model.MovementPattern;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal abstract class Entity
    {
        // if changing this, call the "NotifyOfDestinationRectangleChange" function
        public Rectangle DestinationRectangle;
        private float _rotation = 0f;
        public Vector2 _origin = Vector2.Zero;

        public Entity()
        {
            DestinationRectangle = new Rectangle();
        }

        // alternatively can send 'this' instead of specific attributes
        // but not sending 'this' means sending one less complex object
        public delegate void DestinationRectangleChangedHandler(Rectangle destinationRectangle);
        public event DestinationRectangleChangedHandler DestinationRectangleChanged;

        public delegate void RotationChangedHandler(float rotation);
        public event RotationChangedHandler RotationChanged;

        public delegate void OriginChangedHandler(Vector2 origin);
        public event OriginChangedHandler OriginChanged;

        public void NotifyOfDestinationRectangleChange()
        {
            DestinationRectangleChanged?.Invoke(DestinationRectangle);
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                RotationChanged?.Invoke(_rotation);
            }
        }

        public Vector2 Origin
        {
            get { return _origin; }
            set
            {
                _origin = value;
                OriginChanged?.Invoke(_origin);
            }
        }

        public Point Center
        {
            get
            {
                return new Point(DestinationRectangle.X + DestinationRectangle.Width / 2, DestinationRectangle.Y + DestinationRectangle.Height / 2);
            }
        }
    }
}
