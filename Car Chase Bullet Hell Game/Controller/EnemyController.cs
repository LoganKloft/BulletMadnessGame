using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class EnemyController
    {
        public static List<Enemy> enemies = new List<Enemy>();

        public static void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
            enemy.DestroyEvent += DestroyEnemyEventHandler;
        }

        public static void RemoveEnemy(Enemy enemy)
        {
            enemy.DestroyEvent -= DestroyEnemyEventHandler;
            enemies.Remove(enemy);
        }

        public static void DestroyEnemyEventHandler(Entity entity)
        {
            if (entity is Enemy)
            {
                Enemy enemy = (Enemy)entity;
                RemoveEnemy(enemy);
            }
        }

        public static void Update(GameTime gameTime)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                int count = enemies.Count;
                Enemy enemy = enemies[i];
                enemy.Update(gameTime);
                if (enemies.Count < count)
                {
                    i--;
                }
            }
        }
    }
}
