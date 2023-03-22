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
using Car_Chase_Bullet_Hell_Game.Model.MovementPattern;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class DrawController
    {
        public static Background background;
        public static Sprite playerSprite;
        public static List<Sprite> sprites = new List<Sprite>();

        public static void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // draw background first so everything else appears on top
            background.Draw(spriteBatch, gameTime);

            foreach (Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch, gameTime);
            }

            // draw player last so it appears on top of everything else
            //Player.Instance.Draw(_spriteBatch, gameTime);
            playerSprite.Draw(spriteBatch, gameTime);
        }

        public static void Remove(Sprite sprite)
        {
            sprites.Remove(sprite);
        }
    }
}
