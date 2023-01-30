using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Car_Chase_Bullet_Hell_Game
{
    internal class CircleShotPattern : ShotPattern
    {
        List<Shot> shots = new List<Shot>();
        int _shotCount = 0;
        private float time = 0f;

        public CircleShotPattern(int shotCount) : base()
        {
            _shotCount = shotCount;
        }

        public void CreateShots(ContentManager content, string asset, Point point)
        {
            if (_shotCount == 0)
            {
                return;
            }

            double shotOffset = (Math.PI * 2d) / _shotCount;
            for(int i = 0; i < _shotCount; i++)
            {
                StraightShot shot = new StraightShot();
                shot.LoadContent(content, asset);
                shot.Direction = shotOffset * i;
                shot.DestinationRectangle.X = point.X;
                shot.DestinationRectangle.Y = point.Y;
                shots.Add(shot);
            }
        }

        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Shot shot in shots)
            {
                shot.Move(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Shot shot in shots)
            {
                shot.Draw(spriteBatch, gameTime);
            }
        }

        public override bool Finished()
        {
            return time > 10f;
        }
    }
}
