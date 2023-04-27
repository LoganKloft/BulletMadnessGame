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
using Car_Chase_Bullet_Hell_Game.Controller.Spawn;
using Microsoft.Xna.Framework.Media;

namespace Car_Chase_Bullet_Hell_Game.Controller
{
    internal class DrawController
    {
        public static Background background;
        public static Sprite playerSprite;
        public static Sprite gameLost;
        public static Sprite gameWon;
        public static Dictionary<string, Sprite> effects = new Dictionary<string, Sprite>();
        public static List<Sprite> sprites = new List<Sprite>();
        public static List<Sprite> lives = new List<Sprite>();
        public static List<Sprite> shields = new List<Sprite>();
        public static List<Sprite> powerUps = new List<Sprite>();
        public static float death = 1f;
        private static bool gameOverLost = false;
        private static bool gameOverWin = false;

        public static void AddEffect(string effectName, string asset)
        {
            Sprite sprite = new Sprite();
            sprite.LoadContent(Game1.content, asset);
            sprite.DestinationRectangle.Width = 64;
            sprite.DestinationRectangle.Height = 64;
            effects.Add(effectName, sprite);
            UpdateEffectLocation();
        }

        public static void RemoveEffect(string effectName)
        {
            effects.Remove(effectName);
            UpdateEffectLocation();
        }

        public static bool HasEffect(string effectName)
        {
            return effects.ContainsKey(effectName);
        }

        public static void AddSprite(Sprite sprite)
        {
            sprites.Add(sprite);
            sprite.DestroyEvent += DestroyEventHandler;
        }

        public static void AddLives(Sprite sprite)
        {
            lives.Add(sprite);
            UpdateLifeLocation();
        }

        public static void AddShield()
        {
            Sprite sprite = new Sprite();
            sprite.LoadContent(Game1.content, "WoodenShield");
            sprite.DestinationRectangle.Width = 64;
            sprite.DestinationRectangle.Height = 64;
            shields.Add(sprite);
            UpdateShieldLocation();
        }

        public static void RemoveShield()
        {
            if (shields.Count > 0)
            {
                shields.RemoveAt(0);
                UpdateShieldLocation();
            } 
        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime, Spawner spawn)
        {
            if(!gameOverLost && !gameOverWin)
            {
                // draw background first so everything else appears on top
                background.Draw(spriteBatch, gameTime);

                for (int i = 0; i < lives.Count; ++i)
                {
                    lives[i].Draw(spriteBatch, gameTime);
                }

                for (int i = 0; i < shields.Count; ++i)
                {
                    shields[i].Draw(spriteBatch, gameTime);
                }

                foreach (Sprite sprite in sprites)
                {
                    sprite.Draw(spriteBatch, gameTime);
                }

                //if(Player.Instance.IsInvincible || Player.Instance.IsCheatMode)
                //{
                //    invulnMode.Draw(spriteBatch, gameTime);
                //}

                //if (Player.Instance.IsSlow)
                //{
                //    slowMode.Draw(spriteBatch, gameTime);
                //}

                foreach (KeyValuePair<string, Sprite> kvp in effects)
                {
                    kvp.Value.Draw(spriteBatch, gameTime);
                }

                if(spawn.CheckGameOver() == true)
                {
                    gameOverWin = true;
                }

                // draw player last so it appears on top of everything else
                //Player.Instance.Draw(_spriteBatch, gameTime);
                playerSprite?.Draw(spriteBatch, gameTime);
            }
            else if(gameOverLost)
            {
                MediaPlayer.Stop();
                GameOverLost(spriteBatch, gameTime);
                Game1.stopGame();
            }
            else
            {
                MediaPlayer.Stop();
                GameOverWon(spriteBatch, gameTime);
                Game1.stopGame();
            }
        }

        public static void DestroyEventHandler(Sprite sprite)
        {
            Remove(sprite);
        }

        public static void Remove(Sprite sprite)
        {
            sprite.DestroyEvent -= DestroyEventHandler;
            sprites.Remove(sprite);
        }

        public static void RemoveLife()
        {
            if (lives.Count>1)
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
                gameOverLost = true;
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

        public static void UpdateShieldLocation()
        {
            for (int i = 0; i < shields.Count; ++i)
            {
                shields[i].DestinationRectangle.X = (int)(1250 / 1.25) - 157 / 2 / 2 + i * 60;
                shields[i].DestinationRectangle.Y = 64;
            }
        }

        public static void UpdateEffectLocation()
        {
            int x = Game1.widthSize - 64;
            int y = Game1.heightSize - 64;

            foreach (KeyValuePair<string, Sprite> kvp in effects)
            {
                kvp.Value.DestinationRectangle.X = x;
                kvp.Value.DestinationRectangle.Y = y;

                y -= 64;
            }
        }

        public static void GameOverLost(SpriteBatch spriteBatch, GameTime gameTime)
        {
            gameLost.Draw(spriteBatch, gameTime);
        }
        public static void GameOverWon(SpriteBatch spriteBatch, GameTime gameTime)
        {
            gameWon.Draw(spriteBatch, gameTime);
        }

        public static void DestroyPlayerEventHandler(Entity entity)
        {
            playerSprite = null;
        }
    }
}
