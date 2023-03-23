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
    internal class StraightShotPattern : ShotPattern
    {
        private int shotCount = 0;
        string asset;
        Point point;
        double dir;

        public StraightShotPattern(string asset, Point point, double dir, int shotCount) : base()
        {
            this.shotCount = shotCount;
            this.asset = asset;
            this.point = point;
            this.dir = dir;
        }

        // potential to be called multiple times if bullet is both offscreen and collides with enemy at the same time
        // or if bullet collides with enemy and lifetime runs outs, etc - some design improvements to make
        //private void BulletOffscreenHandler(object sender)
        //{
        //    shotCount--;
        //}

        public override void CreateShots(Entity entity)
        {
            Shot shot = new Shot();
            MovementPattern.MovementPattern movementPattern = new StraightShot(0);
            shot.MovementPattern = movementPattern;
            //shot.LoadContent(content, asset);
            //shot.Direction = dir;
            shot.DestinationRectangle.X = point.X - shot.DestinationRectangle.Width / 2;
            shot.DestinationRectangle.Y = point.Y - shot.DestinationRectangle.Height / 2;
            shot.NotifyOfDestinationRectangleChange();

            //shot.DestinationRectangle.Width = 50;
            //shot.DestinationRectangle.Height = 50;

            //shots.Add(shot);
            //shot.BulletOffscreenEvent += BulletOffscreenHandler;
            ShotController.AddShot(shot);
        }

        //public override void Update(GameTime gameTime)
        //{
        //    foreach (Shot shot in shots)
        //    {
        //        shot.Update(gameTime);
        //    }
        //}

        //public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        //{
        //    foreach (Shot shot in shots)
        //    {
        //        shot.Draw(spriteBatch, gameTime);
        //    }
        //}

        //public override bool Finished()
        //{
        //    return shotCount <= 0;
        //}
    }
}