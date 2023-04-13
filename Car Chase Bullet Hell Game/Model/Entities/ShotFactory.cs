using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Car_Chase_Bullet_Hell_Game.Controller;
using Car_Chase_Bullet_Hell_Game.Model.EntityParameters;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class ShotFactory
    {
        public static (Shot, Sprite) CreateShot(ShotParams shotParams)
        {
            Sprite sprite = new Sprite();
            sprite.LoadContent(Game1.content, shotParams.asset);

            Shot shot = new Shot(.5);
            shot.DestinationRectangle = sprite.DestinationRectangle;

            shot.DestinationRectangleChanged += sprite.DestinationRectangleChangedHandler;
            shot.RotationChanged += sprite.RotationChangedHandler;
            shot.OriginChanged += sprite.OriginChangedHandler;
            shot.DestroyEvent += sprite.DestroyEventHandler;

            return (shot, sprite);
        }
    }
}
