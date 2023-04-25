using Car_Chase_Bullet_Hell_Game.Controller;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class SpiralSpawnerFactory
    {
        public static (Enemy, Sprite) CreateEnemy(string asset)
        {
            Sprite sprite = new Sprite();
            sprite.LoadContent(Game1.content, asset);

            Enemy enemy = new SpiralSpawner();
            enemy.DestinationRectangle = sprite.DestinationRectangle;

            enemy.DestinationRectangleChanged += sprite.DestinationRectangleChangedHandler;
            enemy.RotationChanged += sprite.RotationChangedHandler;
            enemy.OriginChanged += sprite.OriginChangedHandler;
            enemy.DestroyEvent += sprite.DestroyEventHandler;

            return (enemy, sprite);
        }
    }
}
