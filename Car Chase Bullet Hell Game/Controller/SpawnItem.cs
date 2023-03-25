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

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class SpawnItem
    {
        public bool spawned = false;
        public string asset;
        public float start;
        public float duration;
        public Rectangle DestinationRectangle;
        public Rectangle Hitbox;
        public List<MovementItem> movementItems = new List<MovementItem>();
        public List<ShotItem> shotItems = new List<ShotItem>();
        public Sprite sprite;
        public Enemy enemy;
        public bool offscreenOccurence = false;

        public delegate void DestroySpawnItemEventHandler(SpawnItem spawnItem);
        public event DestroySpawnItemEventHandler DestroySpawnItemEvent;

        public SpawnItem(string asset, float start, float duration)
        {
            this.asset = asset;
            this.start = start;
            this.duration = duration;
        }

        // instantiate all objects for the enemy
        // add enemy to Spawner to be updated
        // add sprite to DrawController to be drawn
        public void Spawn()
        {
            (Enemy enemy, Sprite sprite) = EnemyFactory.CreateEnemy(asset);

            this.enemy = enemy;
            this.sprite = sprite;

            if (!DestinationRectangle.IsEmpty)
            {
                enemy.DestinationRectangle = DestinationRectangle;
                sprite.DestinationRectangle = DestinationRectangle;
            }

            this.enemy.DestroyEvent += DestroyEnemyEventHandler;

            DrawController.AddSprite(sprite);
            Spawner.RemoveInactiveSpawnItem(this);
            Spawner.AddActiveSpawnItem(this);

            spawned = true;
        }

        public void DestroyEnemyEventHandler(Entity entity)
        {
            InvokeDestroySpawnItemEvent();
        }

        public void InvokeDestroySpawnItemEvent()
        {
            DestroySpawnItemEvent?.Invoke(this);
        }

        public void Update(GameTime gameTime)
        {
            if (spawned)
            {
                duration -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (duration <= 0f)
                {
                    enemy.InvokeDestroyEvent();
                }
                else
                {
                    if (movementItems.Count > 0)
                    {
                        if (movementItems[0].duration<0.025 && movementItems[0].movementPattern is not OffScreenMovementPattern)
                        {
                            movementItems.RemoveAt(0);
                        }
                        if (movementItems[0].movementPattern is OffScreenMovementPattern)
                        {
                            this.offscreenOccurence = true;
                        }
                        movementItems[0].Update(gameTime);
                    }

                    enemy.Update(gameTime);

                    for (int i = 0; i < shotItems.Count; i++)
                    {
                        int count = shotItems.Count;
                        ShotItem shotItem = shotItems[i];
                        shotItem.Update(gameTime);
                        if (shotItems.Count < count)
                        {
                            i--;
                        }
                    }
                }
            }
            else if (start <= 0)
            {
                Spawn();
            }
            else
            {
                start -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void AddMovementItem(string type, float duration)
        {
            movementItems.Add(new MovementItem(duration, MovementPattern.MovementPattern.Parse(type), this));
        }

        public void AddShotItem(float start, float duration, float shootSpeed, string type, string asset,int shotCount)
        {
            shotItems.Add(new ShotItem(this, start, duration, shootSpeed, type, asset, shotCount));
        }
    }
}
