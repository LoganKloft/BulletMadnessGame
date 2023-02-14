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
    internal abstract class Shot : Sprite
    {
        public float Damage;
        private bool _offscreen = false; // prevent multiple offscreen event calls
        private bool _collided = false; // prevent multiple collided event calls

        // the behavior for the movement of the bullet
        public abstract void Move(GameTime gameTime);

        public delegate void BulletCollideEventHandler(object sender);
        public event BulletCollideEventHandler BulletCollideEvent;

        public delegate void BulletOffscreenEventHandler(object sender);
        public event BulletCollideEventHandler BulletOffscreenEvent;

        public delegate void BulletEndEventHandler(object sender);
        public event BulletEndEventHandler BulletEndEvent; // invoked whenever a Bullet has collided, went offscreen, etc.

        // returns whether the bullet collided with the player
        public void Collided()
        {
            if (_collided) return;
            // return base.DestinationRectangle.Intersects(Player.Instance.DestinationRectangle);
            if (base.DestinationRectangle.Intersects(Player.Instance.DestinationRectangle))
            {
                BulletCollideEvent?.Invoke(this);
                InvokeBulletEnd();
                _collided = true;
            }
        }

        public void Offscreen()
        {
            if (_offscreen) return;
            //return base.DestinationRectangle.Left > Game1.gd.Viewport.Bounds.Right
            //    || base.DestinationRectangle.Right < Game1.gd.Viewport.Bounds.Left
            //    || base.DestinationRectangle.Top > Game1.gd.Viewport.Bounds.Bottom
            //    || base.DestinationRectangle.Bottom < Game1.gd.Viewport.Bounds.Top;
            if (base.DestinationRectangle.Left > Game1.gd.Viewport.Bounds.Right
                || base.DestinationRectangle.Right < Game1.gd.Viewport.Bounds.Left
                || base.DestinationRectangle.Top > Game1.gd.Viewport.Bounds.Bottom
                || base.DestinationRectangle.Bottom < Game1.gd.Viewport.Bounds.Top)
            {
                BulletOffscreenEvent?.Invoke(this);
                InvokeBulletEnd();
                _offscreen = true;
            }
        }

        public void InvokeBulletEnd()
        {
            BulletEndEvent?.Invoke(this);
        }
    }
}
