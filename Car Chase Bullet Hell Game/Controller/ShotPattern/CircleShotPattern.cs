using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.View.Sprite;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class CircleShotPattern : ShotPattern
    {
        int _shotCount = 0;
        private float time = 0f;
        Point point;
        string asset;

        public CircleShotPattern(string asset, Point point, int shotCount) : base()
        {
            _shotCount = shotCount;
            this.asset = asset;
            this.point = point;
        }

        public override void CreateShots(Entity entity, GameTime gameTime)
        {
            if (_shotCount == 0)
            {
                return;
            }

            double shotOffset = Math.PI * 2d / _shotCount;
            for (int i = 0; i < _shotCount; i++)
            {
                Shot shot = new Shot(.5);
                MovementPattern.MovementPattern movementPattern = new StraightShot(shotOffset * i);
                shot.MovementPattern = movementPattern;
                Sprite shotSprite = new Sprite();
                shotSprite.LoadContent(Game1.content, asset);
                shot.DestinationRectangle = shotSprite.DestinationRectangle;
                shot.DestinationRectangleChanged += shotSprite.DestinationRectangleChangedHandler;
                shot.RotationChanged += shotSprite.RotationChangedHandler;
                shot.OriginChanged += shotSprite.OriginChangedHandler;
                shot.DestroyEvent += shotSprite.DestroyEventHandler;
                DrawController.AddSprite(shotSprite);

                shot.DestinationRectangle.X = point.X - (shot.DestinationRectangle.Width / 2);
                shot.DestinationRectangle.Y = point.Y - (shot.DestinationRectangle.Height / 2);
                shot.NotifyOfDestinationRectangleChange();
                ShotController.AddShot(shot);

                // create proper command
                if (entity is Enemy)
                {
                    // then shots should collide with player
                    CollisionBulletCommand command = new CollisionBulletCommand(shot, Player.Instance);
                    CollisionDetector.AddCommand(command);
                }

                if (entity is Player)
                {
                    // then shots should collide with enemies - player won't use CircleShotPattern (probably)
                }
            }
        }
    }
}
