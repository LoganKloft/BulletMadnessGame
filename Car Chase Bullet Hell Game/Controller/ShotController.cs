using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class ShotController
    {
        static List<Shot> shots = new List<Shot>();

        public static void AddShot(Shot shot)
        {
            shots.Add(shot);
            shot.DestroyEvent += DestroyEventHandler;
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < shots.Count; i++)
            {
                int count = shots.Count;
                Shot shot = shots[i];
                shot.Update(gameTime);
                if (shots.Count < count) i--;
            }
        }

        public static void DestroyEventHandler(Entity entity)
        {
            if (entity is Shot)
            {
                Shot shot = (Shot)entity;
                Remove(shot);
            }
        }

        public static void Remove(Shot shot)
        {
            shot.DestroyEvent -= DestroyEventHandler;
            shots.Remove(shot);
        }
    }
}
