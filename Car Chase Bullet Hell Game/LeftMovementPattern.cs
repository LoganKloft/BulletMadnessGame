using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Chase_Bullet_Hell_Game
{
	internal class LeftMovementPattern : MovementPattern
	{
		//float speed = 100f;
		Queue<Point> waypoints = new Queue<Point>();
		Queue<Point> discarded = new Queue<Point>();
		public bool repeat = false;
		private int curRun = 0;

		private Vector2 position;       // Position of the sprite, where it is drawn
		private Vector2 direction = new Vector2(1, 0);  // Normalized vector pointing in the direction to move
		private float speed;            // Speed in units (pixels if not zooming) per second
		private bool moveLeft = true;

		public void AddPoint(Point p)
		{
			waypoints.Enqueue(p);
		}

		public override void Move(GameTime gameTime, Enemy enemy)
		{
			if (curRun == 0)
			{
				++curRun;
				enemy.DestinationRectangle.Y = 700;
				enemy.DestinationRectangle.X = 1750;
			}

			if (moveLeft)
				enemy.DestinationRectangle.X -= 5;
			else
				enemy.DestinationRectangle.X += 5;

			if (enemy.DestinationRectangle.X < 895 || enemy.DestinationRectangle.X > 1750)
				moveLeft = !moveLeft;
		}


	}

}