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

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class EnemyFactory
    {
        public static (Enemy, Sprite) CreateEnemy(string asset)
        {
            Sprite sprite = new Sprite();
            sprite.LoadContent(Game1.content, asset);

            Enemy enemy = new Enemy();
            enemy.DestinationRectangle = sprite.DestinationRectangle;

            enemy.DestinationRectangleChanged += sprite.DestinationRectangleChangedHandler;
            enemy.RotationChanged += sprite.RotationChangedHandler;
            enemy.OriginChanged += sprite.OriginChangedHandler;

            return (enemy, sprite);
        }
    }
}
