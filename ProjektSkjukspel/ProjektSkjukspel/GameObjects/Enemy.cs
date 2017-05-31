using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel.GameObjects
{
    class Enemy : MoveableObject
    {
        public const int SPEED = 2;    //Has to be even.
        int health;

        Queue<Vector2> waypoints = new Queue<Vector2>();

        Vector2 direction = Vector2.Zero;

        Tile startTile = null, goalTile = null;

        public Enemy(Vector2 position, Texture2D texture, int health)
            : base(position, texture)
        {
            this.health = health;
        }

        public override void Update()
        {
            if (health == 0)
                dead = true;

            //Tile-based movement
            if (pixelCounter != Game1.tileSize && direction != Vector2.Zero)
            {
                pixelCounter += SPEED;
            }
            if (pixelCounter == Game1.tileSize)
            {
                direction = Vector2.Zero;
                pixelCounter = 0;
            }

            if (waypoints.Count > 0)
            {
                if (DistanceToTarget < SPEED)
                {
                    position = waypoints.Peek();
                    waypoints.Dequeue();
                }
                else
                {
                    direction = waypoints.Peek() - position;
                    direction.Normalize();
                }
            }

            position += direction * SPEED;

            base.Update();
        }

        public bool ProjectileCollision(Projectile projectile)
        {
            return hitbox.Intersects(projectile.hitbox);
        }

        public void FindPath(List<Tile> tileList, Vector2 targetPosition)
        {
            //if (waypoints.Count() > 0)
            //    return;

            waypoints.Clear();

            List<Tile> openList = new List<Tile>();
            List<Tile> closedList = new List<Tile>();

            List<Vector2> tempQueue = new List<Vector2>();

            targetPosition = (targetPosition / Game1.tileSize) * Game1.tileSize;

            foreach (Tile t in tileList)
            {
                if (t.position.ToVector2() == position)
                {
                    t.visited = true;
                    startTile = t;
                    openList.Add(t);
                }
                else if (t.position.ToVector2() == targetPosition)
                {
                    goalTile = t;
                }
            }

            while (openList.Count != 0)
            {
                if (openList[0].position.ToVector2() == targetPosition)
                    break;

                foreach (Tile t in openList[0].FindNeighbors(ref tileList))
                {
                    if (t.visited == true)
                        continue;

                    else
                    {
                        openList.Add(t);
                        t.visited = true;
                        t.SetTravelledFrom(openList[0]);
                    }
                }

                openList[0].visited = true;
                openList.Remove(openList[0]);
            }

            Tile current = goalTile;

            if (current == null)
            {
                current = startTile;
            }

            tempQueue.Add(current.position.ToVector2());

            while (current != startTile)
            {
                tempQueue.Add(current.position.ToVector2());

                current = current.GetTravelledFrom();
            }

            for (int i = tempQueue.Count - 1; i > 0; i--)
            {
                waypoints.Enqueue(tempQueue[i]);
            }

            foreach (Tile t in tileList)
                t.visited = false;
        }

        private float DistanceToTarget
        {
            get { return Vector2.Distance(position, waypoints.Peek()); }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public int GetPixelCounter()
        {
            return pixelCounter;
        }

        public Vector2 GetDirection()
        {
            return direction;
        }

        public Vector2 GetEmitterLocation()
        {
            return position;
        }
    }
}