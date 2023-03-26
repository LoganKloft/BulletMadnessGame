using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Chase_Bullet_Hell_Game.Controller;

namespace Car_Chase_Bullet_Hell_Game.Model.Entities
{
    internal class Button
    {
        private Texture2D _staticTexture;
        private Texture2D _clickedTexture;
        public int AnimationTime { get; set; }
        public string Name { get; set; }
        public Point Dimensions { get; set; }
        public int ID { get; set; }
        public float LayerDepth { get; set; }
        public bool Visible { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int CellWidth { get; set; }
        public int CellHeight { get; set; }

        public Button(Texture2D staticImage, Texture2D clickedImage, Point dimensions, Vector2 position, string name, int id, bool visible, float layerDepth)
        {
            _staticTexture = staticImage;
            _clickedTexture = clickedImage;

            Texture = _staticTexture;
            CellWidth = dimensions.X; 
            CellHeight = dimensions.Y;
            LayerDepth = layerDepth;
            Visible = visible;
            AnimationTime = 0;
            Name = name;
            Position = position;
            Dimensions = new Point(dimensions.X, dimensions.Y);
            ID = id;
        }

        public void Clicked()
        {
            //AnimationTime = 30;
            Game1.gameState = Game1.GameState.Playing;
        }

        public void UpdateButton()
        {
            if (AnimationTime > 0)
            {
                AnimationTime--;
            }
            if (AnimationTime == 0)
            {
                Texture = _staticTexture;
            }
        }
    }
}
