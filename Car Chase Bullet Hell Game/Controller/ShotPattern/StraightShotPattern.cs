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
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class StraightShotPattern : ShotPattern
    {
        private int shotCount = 0;
        string asset;
        ShotParams _shotParams;
        private StraightShotFactory factory = new StraightShotFactory();
        //Point point;
        //double dir; - Never used and hard to make work with current parser. If needed, we need to update parameters passed into parser.

        //public StraightShotPattern(string asset, Point point, double dir, int shotCount) : base()
        public StraightShotPattern(ShotParams shotParams) : base()
        {
            _shotParams = shotParams;
            shotCount = shotParams.shotCount != null ? (int)shotParams.shotCount : shotCount;
            if (shotParams.point != null)
            {
                this.point = new Point(shotParams.point[0], shotParams.point[1]);
            }
        }

        // potential to be called multiple times if bullet is both offscreen and collides with enemy at the same time
        // or if bullet collides with enemy and lifetime runs outs, etc - some design improvements to make
        //private void BulletOffscreenHandler(object sender)
        //{
        //    shotCount--;
        //}

        public override void CreateShots(Entity entity, GameTime gameTime)
        {
            (Shot shot, Sprite sprite) = ShotFactory.CreateShot(_shotParams);
            MovementPattern.MovementPattern movementPattern = factory.CreateMovementPattern(new MovementParams() {direction = 1.5});
            shot.MovementPattern = movementPattern;
            DrawController.AddSprite(sprite);
            //shot.LoadContent(content, asset);
            //shot.Direction = dir;
            if (_shotParams.point == null)
            {
                shot.DestinationRectangle.X = entity.Center.X - (shot.DestinationRectangle.Width / 2);
                shot.DestinationRectangle.Y = entity.Center.Y - (shot.DestinationRectangle.Height / 2);
            }
            else
            {
                shot.DestinationRectangle.X = this.point.X;
                shot.DestinationRectangle.Y = this.point.Y;
            }

            //shot.DestinationRectangle.Width = 50;
            //shot.DestinationRectangle.Height = 50;

            //shots.Add(shot);
            //shot.BulletOffscreenEvent += BulletOffscreenHandler;
            ShotController.AddShot(shot);
            // create proper command
            if (entity is Enemy)
            {
                // then shots should collide with player
                CollisionBulletCommand command = new CollisionBulletCommand(shot, Player.Instance);
                CollisionDetector.AddCommand(command);
            }
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