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
    internal class Enemy : Sprite
    {
        public MovementPattern MovementPattern;
        public Queue<ShotPattern> ShotPatterns = new Queue<ShotPattern>();

        public void Update(GameTime gameTime)
        {
            ShotPattern shotPattern;
            while (ShotPatterns.TryPeek(out shotPattern) && shotPattern.Finished())
            {
                ShotPatterns.Dequeue();
            }

            MovementPattern.Move(gameTime, this);
            foreach (ShotPattern pattern in ShotPatterns)
            {
                pattern.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
            foreach (ShotPattern pattern in ShotPatterns)
            {
                pattern.Draw(spriteBatch, gameTime);
            }
        }
    }
}
