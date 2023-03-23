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
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal abstract class Entity
    {
        // if changing this, call the "NotifyOfDestinationRectangleChange" function
        public Rectangle DestinationRectangle;
        private float _rotation = 0f;
        public Vector2 _origin = Vector2.Zero;

        private float _rotationHitBox = 0f;
        public Vector2 _originHitBox = Vector2.Zero;
        private double percent = 1.0;

        public Rectangle HitBoxRectangle;

        /*
         * Entity has two constructors, one where the destination rectangle is the same as the hitbox (for enemies) and
         * one with a custom hitbox (player)
         */

        public Entity()
        {
            DestinationRectangle = new Rectangle();
            HitBoxRectangle = DestinationRectangle;
        }

        public Entity(double percentHitBoxSize)
        {
            DestinationRectangle = new Rectangle();
            HitBoxRectangle = new Rectangle();
            percent = percentHitBoxSize;
        }

        // alternatively can send 'this' instead of specific attributes
        // but not sending 'this' means sending one less complex object
        public delegate void DestinationRectangleChangedHandler(Rectangle destinationRectangle);
        public event DestinationRectangleChangedHandler DestinationRectangleChanged;

        public delegate void RotationChangedHandler(float rotation);
        public event RotationChangedHandler RotationChanged;

        public delegate void OriginChangedHandler(Vector2 origin);
        public event OriginChangedHandler OriginChanged;

        public delegate void DestroyEventHandler(Entity entity);
        public abstract event DestroyEventHandler DestroyEvent;

        public void NotifyOfDestinationRectangleChange()
        {
            // recalculate hitbox when DestinationRectangle changes - hitbox is centered
            //Point center = DestinationRectangle.Center;
            //HitBoxRectangle.X = center.X - (DestinationRectangle.Width / 2);
            //HitBoxRectangle.Y = center.Y - (DestinationRectangle.Height / 2);
            HitBoxRectangle = DestinationRectangle;

            DestinationRectangleChanged?.Invoke(DestinationRectangle);

            // change the location of hitbox while also maintaining the percentage size
            HitBoxRectangle.X = Convert.ToInt32(DestinationRectangle.X * percent);
            HitBoxRectangle.Y = Convert.ToInt32(DestinationRectangle.Y * percent);
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

        public float RotationHitBox
        {
            get { return _rotationHitBox; }
            set
            {
                _rotationHitBox = value;
            }
        }

        public Vector2 OriginHitBox
        {
            get { return _originHitBox; }
            set
            {
                _originHitBox = value;
            }
        }

        public Point CenterHitBox
        {
            get
            {
                return new Point(DestinationRectangle.X + DestinationRectangle.Width / 2, DestinationRectangle.Y + DestinationRectangle.Height / 2);
            }
        }

    }
}
