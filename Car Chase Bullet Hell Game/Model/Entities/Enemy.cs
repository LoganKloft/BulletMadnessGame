using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Model.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Model.MovementPattern;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class Enemy : Sprite
    {
        public MovementPattern.MovementPattern MovementPattern;
        public Queue<ShotPattern.ShotPattern> ShotPatterns = new Queue<ShotPattern.ShotPattern>();

        public void Update(GameTime gameTime)
        {
            ShotPattern.ShotPattern shotPattern;
            while (ShotPatterns.TryPeek(out shotPattern) && shotPattern.Finished())
            {
                ShotPatterns.Dequeue();
                System.Diagnostics.Debug.WriteLine("ShotPattern Dequeued");
            }

            if (MovementPattern is not null)
            {
                MovementPattern.Move(gameTime, this);
            }
            foreach (ShotPattern.ShotPattern pattern in ShotPatterns)
            {
                pattern.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            foreach (ShotPattern.ShotPattern pattern in ShotPatterns)
            {
                pattern.Draw(spriteBatch, gameTime);
            }
        }
    }
}
