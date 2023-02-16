using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class HalfCircleShotPattern : ShotPattern
    {

        private List<Shot> shots = new List<Shot>();
        private int shotCount = 0;

        internal HalfCircleShotPattern(int shotCount) : base()
        {
            this.shotCount = shotCount;
        }

        // potential to be called multiple times if bullet is both offscreen and collides with enemy at the same time
        // or if bullet collides with enemy and lifetime runs outs, etc - some design improvements to make
        private void BulletEndEventHandler(object sender)
        {
            shotCount--;
        }

        internal void CreateShots(ContentManager content, string asset, Point point)
        {
            if (shotCount == 0)
            {
                return;
            }

            double offset = (Math.PI * 1d) / shotCount;
            for (int i = 0; i < shotCount; i++)
            {
                StraightShot shot = new StraightShot();
                shot.LoadContent(content, asset);
                shot.Direction = -offset * i;
                shot.DestinationRectangle.X = point.X - (shot.DestinationRectangle.Width / 2);
                shot.DestinationRectangle.Y = point.Y - (shot.DestinationRectangle.Height / 2);
                shots.Add(shot);
                shot.BulletEndEvent += BulletEndEventHandler;
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Shot shot in shots)
            {
                shot.Move(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Shot shot in shots)
            {
                shot.Draw(spriteBatch, gameTime);
            }
        }

        public override bool Finished()
        {
            return shotCount <= 0;
        }
    }
}
