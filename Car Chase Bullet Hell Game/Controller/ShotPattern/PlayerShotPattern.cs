using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Car_Chase_Bullet_Hell_Game.Model.Entities.Entity;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class PlayerShotPattern : ShotPattern
    {
        private float shotSpeed = .25f;
        private float shotTimer = 0f;
        private StraightShotFactory straight = new StraightShotFactory();

        public delegate void LostLifeEventHandler();
        
        public override void CreateShots(Entity entity, GameTime gameTime)
        {
            Point current = entity.Center;
            shotTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shotTimer > shotSpeed)
            {
                shotTimer = shotSpeed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (shotTimer >= shotSpeed)
                {
                    Shot shot = new Shot(.5);
                    MovementPattern.MovementPattern movementPattern = straight.CreateMovementPattern(new MovementParams { direction = (-Math.PI / 2), speed = 10 });
                    shot.MovementPattern = movementPattern;
                    Sprite shotSprite = new Sprite();
                    shotSprite.LoadContent(Game1.content, "01");
                    shot.DestinationRectangle = shotSprite.DestinationRectangle;
                    shot.DestinationRectangleChanged += shotSprite.DestinationRectangleChangedHandler;
                    shot.RotationChanged += shotSprite.RotationChangedHandler;
                    shot.OriginChanged += shotSprite.OriginChangedHandler;
                    shot.DestroyEvent += shotSprite.DestroyEventHandler;
                    DrawController.AddSprite(shotSprite);

                    shot.DestinationRectangle.X = entity.Center.X - shot.DestinationRectangle.Width / 2;
                    shot.DestinationRectangle.Y = entity.Center.Y - shot.DestinationRectangle.Height / 2;
                    shot.NotifyOfDestinationRectangleChange();
                    shot.DestroyEvent += ShotController.DestroyEventHandler;
                    ShotController.AddShot(shot);
                    Command command = new CollisionBulletEnemyCommand(shot);
                    CollisionDetector.AddCommand(command);


                    shotTimer = 0f;
                }
            }
        }
    }
}
