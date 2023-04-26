using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Controller.Commands;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
{
    internal class SpawnItem
    {
        public Tuple<Powerup, Sprite> PowerUpTuple = new Tuple<Powerup, Sprite>(null, null);
        public bool spawned = false;
        public string asset;
        public float start = 0;
        public float duration = 00;
        public Rectangle DestinationRectangle;
        public Rectangle Hitbox;
        public List<MovementItem> movementItems = new List<MovementItem>();
        public List<ShotItem> shotItems = new List<ShotItem>();
        public Sprite sprite;
        public Enemy enemy;
        public bool offscreenOccurence = false;
        public int score = 0;

        public delegate void DestroySpawnItemEventHandler(SpawnItem spawnItem);
        public event DestroySpawnItemEventHandler DestroySpawnItemEvent;

        EnemyParams enemyParams;

        public SpawnItem(EnemyParams enemyParams)
        {
            this.enemyParams = enemyParams;
            asset = enemyParams.asset;
            start = enemyParams.start != null ? (float)enemyParams.start : start;
            duration = enemyParams.duration != null ? (float)enemyParams.duration : duration;
            score = enemyParams.score;

            if (enemyParams.dimensions != null)
            {
                DestinationRectangle = new Rectangle(0, 0, enemyParams.dimensions[0], enemyParams.dimensions[1]);
            }

            foreach (MovementParams movementParams in enemyParams.movementItems)
            {
                this.AddMovementItem(movementParams);
            }

            foreach (ShotParams shotParams in enemyParams.shotItems)
            {
                this.AddShotItem(shotParams);
            }
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
            Command command = new CollisionPlayerEnemyCommand(this.enemy, Player.Instance);
            CollisionDetector.AddCommand(command);

            DrawController.AddSprite(sprite);
            if (enemyParams.healthBar)
            {
                BossHealthBar healthBar = new BossHealthBar(enemy);
                DrawController.AddSprite(healthBar);
            }

            Spawner.RemoveInactiveSpawnItem(this);
            Spawner.AddActiveSpawnItem(this);

            spawned = true;
        }



        public void DestroyEnemyEventHandler(Entity entity)
        {
            if (enemy.Health <= 0)
            {
                Random rnd = new Random();
                int pUpDecider = rnd.Next(1, 4); // powerup is random, randomly can not spawn as well!

                Tuple<Powerup, Sprite> tup = new Tuple<Powerup, Sprite>(null, null);

                if (pUpDecider == 1)
                {
                    PowerUpFactory pFact = new PowerUpFactory();
                    tup = pFact.CreatePowerUp("pUpHeart", "ExtraHeart", this.enemy);
                    PowerUpTuple = tup;

                    DrawController.AddSprite(PowerUpTuple.Item2);

                    Command command = new CollisionPlayerPowerUpCommand(PowerUpTuple.Item1, Player.Instance);
                    CollisionDetector.AddCommand(command);
                }

                if (pUpDecider == 2)
                {
                    PowerUpFactory pFact = new PowerUpFactory();
                    tup = pFact.CreatePowerUp("pUpDamage", "ExtraDamage", this.enemy); 
                    PowerUpTuple = tup;

                    DrawController.AddSprite(PowerUpTuple.Item2);

                    Command command = new CollisionPlayerPowerUpCommand(PowerUpTuple.Item1, Player.Instance);
                    CollisionDetector.AddCommand(command);
                }

                Player.Instance.Score += this.score;
            }

         

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
                        if (movementItems[0].movementPattern is OffScreenMovementPattern)
                        {
                            offscreenOccurence = true;
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

        public void AddMovementItem(MovementParams movementParams)
        {
            movementItems.Add(new MovementItem(this, movementParams));
        }

        public void AddShotItem(ShotParams shotParams)
        {
            shotItems.Add(new ShotItem(this, shotParams));
        }
    }
}
