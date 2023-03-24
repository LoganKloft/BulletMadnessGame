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
using Car_Chase_Bullet_Hell_Game.Model.Entities;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class DrawController
    {
        public static Background background;
        public static Sprite playerSprite;
        public static List<Sprite> sprites = new List<Sprite>();
        public static List<Sprite> lives = new List<Sprite>();
        public static float death = 1f;

        public static void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
        }

        public static void AddLives(Sprite sprite)
        {
            lives.Add(sprite);
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // draw background first so everything else appears on top
            background.Draw(spriteBatch, gameTime);

            for(int i=0; i<lives.Count; ++i)
            {
                lives[i].Draw(spriteBatch, gameTime);
            }
            

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

        public static void RemoveLife()
        {
            if (lives.Count>0)
            {
                lives.RemoveAt(lives.Count - 1);
                // center main player
                Player.Instance.DestinationRectangle.X = 1250/2 - 157 / 2 / 2;
                Player.Instance.DestinationRectangle.Y = 800 - 250 / 2 / 2;

                // make the player smaller
                Player.Instance.NotifyOfDestinationRectangleChange();
            }
            else
            {
                
            }
        }

        public static void UpdateLifeLocation()
        {
            for(int i=0; i<lives.Count; ++i)
            {
                lives[i].DestinationRectangle.X = (int)(1250 /1.25) - 157 / 2 / 2 + i*60;
                lives[i].DestinationRectangle.Y = 20;
            }
        }
    }
}
