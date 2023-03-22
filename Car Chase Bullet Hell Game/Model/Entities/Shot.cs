﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal abstract class Shot : Entity
    {
        public float Damage;
        private bool _offscreen = false; // prevent multiple offscreen event calls
        private bool _collided = false; // prevent multiple collided event calls

        // the behavior for the movement of the bullet
        public abstract void Move(GameTime gameTime);

        public delegate void BulletOffscreenEventHandler(object sender);
        public event BulletOffscreenEventHandler BulletOffscreenEvent;

        public delegate void BulletEndEventHandler(object sender);
        public event BulletEndEventHandler BulletEndEvent; // invoked whenever a Bullet has collided, went offscreen, etc.

        public void Offscreen()
        {
            if (_offscreen) return;
            //return base.DestinationRectangle.Left > Game1.gd.Viewport.Bounds.Right
            //    || base.DestinationRectangle.Right < Game1.gd.Viewport.Bounds.Left
            //    || base.DestinationRectangle.Top > Game1.gd.Viewport.Bounds.Bottom
            //    || base.DestinationRectangle.Bottom < Game1.gd.Viewport.Bounds.Top;
            if (DestinationRectangle.Left > Game1.gd.Viewport.Bounds.Right
                || DestinationRectangle.Right < Game1.gd.Viewport.Bounds.Left
                || DestinationRectangle.Top > Game1.gd.Viewport.Bounds.Bottom
                || DestinationRectangle.Bottom < Game1.gd.Viewport.Bounds.Top)
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
