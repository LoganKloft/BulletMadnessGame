﻿using System;
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
    internal class Spawner
    {
        public static List<SpawnItem> inactiveSpawnItems = new List<SpawnItem>();
        public static List<SpawnItem> activeSpawnItems = new List<SpawnItem>();

        private OffScreenMovementPattern offScreen = new OffScreenMovementPattern();

        public const int widthSize = 1250, heightSize = 800;

        public Spawner() { }

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
            SpawnItem si;

            si = new SpawnItem("Motorcycle", 0, 15);
            si.DestinationRectangle = new Rectangle(0, 0, 125, 125);
            si.AddMovementItem("RightMovementPattern", 15);
            si.AddShotItem(0f, 15f, 5f, "CircleShotPattern", "01", 16);
            AddInactiveSpawnItem(si);

            si = new SpawnItem("Motorcycle", 0, 15);
            si.DestinationRectangle = new Rectangle(0, 0, 125, 125);
            si.AddMovementItem("LeftMovementPattern", 15);
            si.AddShotItem(0f, 15f, 5f, "CircleShotPattern", "02", 16);
            AddInactiveSpawnItem(si);

            si = new SpawnItem("Police", 30, 15);
            si.DestinationRectangle = new Rectangle(0, 0, 125, 125);
            si.AddMovementItem("RightMovementPattern", 15);
            AddInactiveSpawnItem(si);

            si = new SpawnItem("Police", 30, 15);
            si.DestinationRectangle = new Rectangle(0, 0, 125, 125);
            si.AddMovementItem("LeftMovementPattern", 15);
            AddInactiveSpawnItem(si);

            si = new SpawnItem("Boss", 45, 15);
            si.AddMovementItem("CircleMovementPattern", 15);
            AddInactiveSpawnItem(si);

            si = new SpawnItem("Tank", 15, 15);
            si.AddMovementItem("TriangleMovementPattern", 15);
            si.AddShotItem(0, 2.5f, 1.5f, "CircleShotPattern", "bullet2", 16);
            si.AddShotItem(2.5f, 5f, 1.5f, "HalfCircleShotPattern", "bullet1", 8);
            si.AddShotItem(5, 7.5f, 1.5f, "CircleShotPattern", "bullet2", 16);
            si.AddShotItem(7.5f, 10f, 1.5f, "HalfCircleShotPattern", "bullet1", 8);
            si.AddShotItem(10, 12.5f, 1.5f, "CircleShotPattern", "bullet2", 16);
            si.AddShotItem(12.5f, 15f, 1.5f, "HalfCircleShotPattern", "bullet1", 8);
            AddInactiveSpawnItem(si);
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
                    activeItem.AddMovementItem("OffScreenMovementPattern", 2);
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