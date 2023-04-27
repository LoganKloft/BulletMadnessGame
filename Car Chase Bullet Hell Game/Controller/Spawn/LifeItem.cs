using Car_Chase_Bullet_Hell_Game.Model.Entities;
using Car_Chase_Bullet_Hell_Game.View.Sprite;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game.Controller.Spawn
{
    internal class LifeItem
    {
        public ContentManager content;
        public Sprite sprite;
        public string asset;

        public LifeItem(ContentManager content, string asset)
        {
            Player.Instance.LostLife += LostLifeItem;
            Player.Instance.GainLife += AddLifeItem;
            this.content = content;
            this.asset = asset;
            for (int i = 0; i < (int)Player.Instance.Health; ++i)
            {
                sprite = new Sprite();
                sprite.LoadContent(content, asset);
                DrawController.AddLives(sprite);
            }
            DrawController.UpdateLifeLocation();
        }

        private void LostLifeItem()
        {
            DrawController.RemoveLife();
            Player.Instance.IsInvincible = true;
        }

        private void AddLifeItem()
        {
            sprite = new Sprite();
            sprite.LoadContent(content, asset);
            DrawController.AddLives(sprite);
        }

    }
}
