using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPatternFactories;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
{
    internal class Spawner
    {
        public static List<SpawnItem> inactiveSpawnItems = new List<SpawnItem>();
        public static List<SpawnItem> activeSpawnItems = new List<SpawnItem>();

        private LevelParams levelParams;

        public const int widthSize = 1250, heightSize = 800;

        public Spawner(LevelParams levelParams) { this.levelParams = levelParams; }

        public static List<Enemy> GetActiveEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();
            foreach (SpawnItem spawnItem in activeSpawnItems)
            {
                enemies.Add(spawnItem.enemy);
            }
            return enemies;
        }

        public static void AddInactiveSpawnItem(SpawnItem spawnItem)
        {
            inactiveSpawnItems.Add(spawnItem);
        }

        public static void RemoveInactiveSpawnItem(SpawnItem spawnItem)
        {
            inactiveSpawnItems.Remove(spawnItem);
        }

        public static void AddActiveSpawnItem(SpawnItem spawnItem)
        {
            activeSpawnItems.Add(spawnItem);
            spawnItem.DestroySpawnItemEvent += DestroySpawnItemEventHandler;
        }

        public static void RemoveActiveSpawnItem(SpawnItem spawnitem)
        {
            spawnitem.DestroySpawnItemEvent -= DestroySpawnItemEventHandler;
            activeSpawnItems.Remove(spawnitem);
        }

        public static void DestroySpawnItemEventHandler(SpawnItem spawnItem)
        {
            RemoveActiveSpawnItem(spawnItem);
        }

        public bool CheckGameOver()
        {
            if (inactiveSpawnItems.Count == 0 & activeSpawnItems.Count == 0)
            {
                return true;
            }

            return false;
        }

        public void Initialize()
        {
            foreach (WaveParams waveParams in levelParams.waves)
            {
                foreach (EnemyParams enemyParams in waveParams.enemies)
                {
                    //handles orbit enemy for final boss
                    if (enemyParams.enemy != null)
                    {
                        EnemyParams orbit = EnemyParams.DeepCopy(enemyParams.enemy);
                        orbit.start = enemyParams.enemy.start;
                        orbit.duration = enemyParams.enemy.duration;
                        SpawnItem spawn = new SpawnItem(orbit);
                        AddInactiveSpawnItem(spawn);
                    }

                    // handles the original enemyParams
                    SpawnItem spawnItem = new SpawnItem(enemyParams);
                    AddInactiveSpawnItem(spawnItem);



                    // handles extra enemies that can be spawned every interval for intervals
                    if (enemyParams.interval != null && enemyParams.intervals != null)
                    {
                        float start = enemyParams.start != null ? (float)enemyParams.start : 0;
                        float duration = enemyParams.duration != null ? (float)enemyParams.duration : 0;
                        int intervals = enemyParams.intervals != null ? (int)enemyParams.intervals : 0;
                        float interval = enemyParams.interval != null ? (float)enemyParams.interval : 0;
                        for (int i = 0; i < intervals; i++)
                        {
                            start += interval;
                            duration += interval;

                            // create deep copy and change start time
                            EnemyParams deepCopy = EnemyParams.DeepCopy(enemyParams);
                            deepCopy.start = start;
                            deepCopy.duration = duration;

                            // create spawnitem and add to spawner
                            spawnItem = new SpawnItem(deepCopy);
                            AddInactiveSpawnItem(spawnItem);
                        }
                    }
                }
            }

            //si = new SpawnItem("02", 0, 15);
            //MovementPattern.MovementPattern mp1 = SpiralMovementPatternFactory.Create(new Point(500, 80), 1, 120, 180);
            //si.AddMovementItem(mp1, 15);
            //si.AddShotItem(0, 3f, 0.1f, shootPlayerShotPatternFactory.CreateShots(asset: "01", shotCount: 1));
            //si.AddShotItem(5, 3f, 0.1f, shootPlayerShotPatternFactory.CreateShots(asset: "01", shotCount: 1));
            //si.AddShotItem(10, 3f, 0.1f, shootPlayerShotPatternFactory.CreateShots(asset: "01", shotCount: 1));
            //AddInactiveSpawnItem(si);

            //si = new SpawnItem("02", 0, 15);
            //MovementPattern.MovementPattern mp2 = SpiralMovementPatternFactory.Create(new Point(500, 80), 1, 120, 0);
            //si.AddMovementItem(mp2, 15);
            //si.AddShotItem(0, 3f, 0.1f, shootPlayerShotPatternFactory.CreateShots(asset: "01", shotCount: 1));
            //si.AddShotItem(5, 3f, 0.1f, shootPlayerShotPatternFactory.CreateShots(asset: "01", shotCount: 1));
            //si.AddShotItem(10, 3f, 0.1f, shootPlayerShotPatternFactory.CreateShots(asset: "01", shotCount: 1));
            //AddInactiveSpawnItem(si);
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < activeSpawnItems.Count; i++)
            {

                int count = activeSpawnItems.Count;
                SpawnItem activeItem = activeSpawnItems[i];
                if (activeItem.duration < 0.025 && activeSpawnItems[i].offscreenOccurence is false)
                {
                    activeItem.duration += 2;
                    activeItem.AddMovementItem(new MovementParams{ movementPattern="OffscreenMovementPattern", duration=2});
                }
                if(activeItem.enemyParams.underlyingEnemy == true)
                {
                   if(activeSpawnItems.Count>1)
                    ((SpiralSpawner)activeItem.enemy).setOrbit(ref activeSpawnItems[i - 1].enemy);
                }
                activeItem.Update(gameTime);
                if (activeSpawnItems.Count < count)
                {
                    i--;
                }
            }

            for (int i = 0; i < inactiveSpawnItems.Count; i++)
            {
                int count = inactiveSpawnItems.Count;
                SpawnItem inactiveItem = inactiveSpawnItems[i];
                inactiveItem.Update(gameTime);
                if (inactiveSpawnItems.Count < count)
                {
                    i--;
                }
            }
        }
    }
}
