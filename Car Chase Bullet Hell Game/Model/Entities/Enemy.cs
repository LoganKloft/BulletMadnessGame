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
    internal class Enemy : Entity
    {
        public MovementPattern MovementPattern;

        public override event DestroyEventHandler DestroyEvent;

        //public Queue<ShotPattern> ShotPatterns = new Queue<ShotPattern>();

        public void Update(GameTime gameTime)
        {
            //ShotPattern shotPattern;
            //while (ShotPatterns.TryPeek(out shotPattern) && shotPattern.Finished())
            //{
            //    ShotPatterns.Dequeue();
            //    System.Diagnostics.Debug.WriteLine("ShotPattern Dequeued");
            //}

            if (MovementPattern is not null)
            {
                MovementPattern.Move(gameTime, this);
            }
            //foreach (ShotPattern pattern in ShotPatterns)
            //{
            //    pattern.Update(gameTime);
            //}
        }
    }
}
