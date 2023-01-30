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

        // the behavior for the movement of the bullet
        public abstract void Move(GameTime gameTime);

        // returns whether the bullet collided with the player
        public bool Collided()
        {
            return base.DestinationRectangle.Intersects(Player.Instance.DestinationRectangle);
        }

        public bool Offscreen()
        {
            return base.DestinationRectangle.Left > Game1.gd.Viewport.Bounds.Right
                || base.DestinationRectangle.Right < Game1.gd.Viewport.Bounds.Left
                || base.DestinationRectangle.Top > Game1.gd.Viewport.Bounds.Bottom
                || base.DestinationRectangle.Bottom < Game1.gd.Viewport.Bounds.Top;
        }
    }
}
