using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    internal abstract class Shot : Sprite
    {
        public float Damage;

        // the behavior for the movement of the bullet
        public abstract void Move();

        // returns whether the bullet collided with the player
        public bool Collided()
        {
            return base.DestinationRectangle.Intersects(Player.Instance.DestinationRectangle);
        }
    }
}
