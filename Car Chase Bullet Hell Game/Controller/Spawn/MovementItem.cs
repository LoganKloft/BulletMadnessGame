using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
{
    internal class MovementItem
    {
        private OffScreenMovementFactory offScreenMovementFactory = new OffScreenMovementFactory();
        private RightMovementPatternFactory rightMovementPatternFactory = new RightMovementPatternFactory();
        private LeftMovementPatternFactory leftFactory = new LeftMovementPatternFactory();
        private CircleMovementPatternFactory circleFactory = new CircleMovementPatternFactory();
        private TriangleMovementPatternFactory triangleFactory = new TriangleMovementPatternFactory();

        public float duration;
        public MovementPattern.MovementPattern movementPattern;
        public SpawnItem spawnItem;
        private bool active = false;
        private MovementParams movementParams;

        public MovementItem(SpawnItem spawnItem, MovementParams movementParams)
        {
            this.movementParams = movementParams;
            this.duration = movementParams.duration;
            this.spawnItem = spawnItem;

            switch (movementParams.movementPattern)
            {
                case "LeftMovementPattern":
                    movementPattern = leftFactory.CreateMovementPattern(movementParams);
                    break;
                case "RightMovementPattern":
                    movementPattern = rightMovementPatternFactory.CreateMovementPattern(movementParams);
                    break;
                case "CircleMovementPattern":
                    movementPattern = circleFactory.CreateMovementPattern(movementParams);
                    break;
                case "TriangleMovementPattern":
                    movementPattern = triangleFactory.CreateMovementPattern(movementParams);
                    break;
                case "OffscreenMovementPattern":
                    movementPattern = offScreenMovementFactory.CreateMovementPattern(movementParams);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (active == false)
            {
                spawnItem.enemy.MovementPattern = movementPattern;
                active = true;
            }

            if (duration <= 0f)
            {
                spawnItem.enemy.MovementPattern = null;
                spawnItem.movementItems.Remove(this);
            }
        }
    }
}
