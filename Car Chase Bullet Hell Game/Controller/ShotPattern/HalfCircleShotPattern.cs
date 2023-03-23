using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class HalfCircleShotPattern : ShotPattern
    {

        private List<Shot> shots = new List<Shot>();
        private int shotCount = 0;

        public HalfCircleShotPattern(int shotCount) : base()
        {
            this.shotCount = shotCount;
        }

        // potential to be called multiple times if bullet is both offscreen and collides with enemy at the same time
        // or if bullet collides with enemy and lifetime runs outs, etc - some design improvements to make
        private void BulletOffscreenHandler(object sender)
        {
            shotCount--;
        }

        internal void CreateShots(ContentManager content, string asset, Point point)
        {
            if (shotCount == 0)
            {
                return;
            }

            double offset = Math.PI * 1d / shotCount;
            for (int i = 0; i < shotCount; i++)
            {
                Shot shot = new Shot();
                MovementPattern.MovementPattern movementPattern = new StraightShot(offset * i);
                shot.MovementPattern = movementPattern;
                //shot.LoadContent(content, asset);
                shot.DestinationRectangle.X = point.X - shot.DestinationRectangle.Width / 2;
                shot.DestinationRectangle.Y = point.Y - shot.DestinationRectangle.Height / 2;

                shot.DestinationRectangle.Width = 50;
                shot.DestinationRectangle.Height = 50;
                shot.NotifyOfDestinationRectangleChange();

                shots.Add(shot);
                shot.BulletOffscreenEvent += BulletOffscreenHandler;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Shot shot in shots)
            {
                shot.Update(gameTime);
            }
        }

        //public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    foreach (Shot shot in shots)
        //    {
        //        shot.Draw(spriteBatch, gameTime);
        //    }
        //}

        public override bool Finished()
        {
            return shotCount <= 0;
        }
    }
}
