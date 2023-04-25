using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller.ShotPattern;
using Car_Chase_Bullet_Hell_Game.Controller.MovementPattern;
using Car_Chase_Bullet_Hell_Game.Controller;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class EnemyFactory
    {
        public static (Enemy, Sprite) CreateEnemy(EnemyParams enemyParams)
        {
            Sprite sprite = new Sprite();
            sprite.LoadContent(Game1.content, enemyParams.asset);

            Enemy enemy = new Enemy(enemyParams);
            enemy.DestinationRectangle = sprite.DestinationRectangle;

            enemy.DestinationRectangleChanged += sprite.DestinationRectangleChangedHandler;
            enemy.RotationChanged += sprite.RotationChangedHandler;
            enemy.OriginChanged += sprite.OriginChangedHandler;
            enemy.DestroyEvent += sprite.DestroyEventHandler;

            return (enemy, sprite);
        }
    }
}
