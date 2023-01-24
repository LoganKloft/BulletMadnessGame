using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    // singleton pattern
    internal sealed class Player : Sprite
    {
        private static Player _instance;
        private static readonly object _lock = new object();

        Player() { }
        public static Player Instance
        {
            get
            {
                lock( _lock )
                {
                    if(_instance == null)
                    {
                        _instance = new Player();
                    }
                    return _instance;
                }
            }
        }
    }
}
