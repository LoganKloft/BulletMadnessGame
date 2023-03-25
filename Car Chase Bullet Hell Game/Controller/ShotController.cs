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

        //public static void DestroyEventHandler(Entity entity)
        //{
        //    if (entity is Shot)
        //    {
        //        Shot shot = (Shot)entity;
        //        shots.Remove(shot);
        //    }
        //} - Issue with deleting shots from list before they are actuall removed. This is causing issues with collision detection as they 
        // appear to still be present in the background, which causes player to lose a life when no bullets are on the screen.

    }
}
