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
using Car_Chase_Bullet_Hell_Game.Controller;


namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class Shot : Entity
    {
        public float Damage = 1f;
        public float LifeTime = 0f;
        public bool hasLifeTime = false;
        private bool _offscreen = false; // prevent multiple offscreen event calls
        private bool _collided = false; // prevent multiple collided event calls

        public MovementPattern MovementPattern;

        public Shot() : base() { }
        public Shot(double percentHitBoxSize) : base(percentHitBoxSize) { }

        // the behavior for the movement of the bullet
        public void Update(GameTime gameTime)
        {
            if (MovementPattern is not null)
            {
                MovementPattern.Move(gameTime, this);
                Offscreen();
            }
        }

        public delegate void BulletOffscreenEventHandler(Shot shot);
        public event BulletOffscreenEventHandler BulletOffscreenEvent;

        public override event DestroyEventHandler DestroyEvent;

        public void Offscreen()
        {
            if (_offscreen) return;

            if (DestinationRectangle.Left > Game1.gd.Viewport.Bounds.Right
                || DestinationRectangle.Right < Game1.gd.Viewport.Bounds.Left
                || DestinationRectangle.Top > Game1.gd.Viewport.Bounds.Bottom
                || DestinationRectangle.Bottom < Game1.gd.Viewport.Bounds.Top)
            {
                //BulletOffscreenEvent?.Invoke(this);
                InvokeDestroyEvent();
                _offscreen = true;
            }
        }

        public void InvokeDestroyEvent()
        {
            DestroyEvent?.Invoke(this);
        }
    }
}
