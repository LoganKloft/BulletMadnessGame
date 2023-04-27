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
using Microsoft.Xna.Framework.Audio;
using System.Reflection.Metadata;
using static Car_Chase_Bullet_Hell_Game.Controller.Game1;

namespace Car_Chase_Bullet_Hell_Game.Controller.ShotPattern
{
    internal class PlayerShotPattern : ShotPattern
    {
        private float shotSpeed = .25f;
        private float shotTimer = 0f;
        private StraightShotFactory straight = new StraightShotFactory();
        ShotParams _shotParams;
        float dmg = 0f;
        public delegate void LostLifeEventHandler();
        List<SoundEffect> soundEffects;

        public PlayerShotPattern(ShotParams shotParams, float damage)
        {
            dmg = damage;
            _shotParams = shotParams;
            soundEffects = new List<SoundEffect>();
            soundEffects.Add(Game1.content.Load<SoundEffect>("bulletNoise"));
        }

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
                    (Shot shot, Sprite sprite) = ShotFactory.CreateShot(_shotParams);
                    MovementPattern.MovementPattern movementPattern = straight.CreateMovementPattern(new MovementParams { direction = (-Math.PI / 2), speed = 10 });
                    shot.MovementPattern = movementPattern;
                    shot.Damage = dmg;
                    DrawController.AddSprite(sprite);

                    shot.DestinationRectangle.X = entity.Center.X - shot.DestinationRectangle.Width / 2;
                    shot.DestinationRectangle.Y = entity.Center.Y - shot.DestinationRectangle.Height / 2;
                    shot.NotifyOfDestinationRectangleChange();
                    ShotController.AddShot(shot);
                    if (getState() is PlayingState)
                    {
                        soundEffects[0].Play();
                    }
                    Command command = new CollisionBulletEnemyCommand(shot);
                    CollisionDetector.AddCommand(command);


                    shotTimer = 0f;
                }
            }
        }
    }
}
