using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class MoveableObject : GameObject
    {
        protected Rectangle leftHitbox, rightHitbox, upHitbox, downHitbox;
        protected Vector2 velocity;

        protected int pixelCounter = Game1.tileSize;

        protected bool left = true,
            right = true,
            up = true,
            down = true;

        public MoveableObject(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            leftHitbox = new Rectangle(hitbox.X - texture.Width, hitbox.Y, texture.Width, texture.Height);
            rightHitbox = new Rectangle(hitbox.X + texture.Width, hitbox.Y, texture.Width, texture.Height);
            upHitbox = new Rectangle(hitbox.X, hitbox.Y - texture.Width, texture.Width, texture.Height);
            downHitbox = new Rectangle(hitbox.X, hitbox.Y + texture.Width, texture.Width, texture.Height);
        }

        public override void Update()
        {
            base.Update();

            leftHitbox.X = hitbox.X - texture.Width;
            leftHitbox.Y = hitbox.Y;

            rightHitbox.X = hitbox.X + texture.Width;
            rightHitbox.Y = hitbox.Y;

            upHitbox.Y = hitbox.Y - texture.Height;
            upHitbox.X = hitbox.X;

            downHitbox.Y = hitbox.Y + texture.Height;
            downHitbox.X = hitbox.X;
        }

        public bool TIleCollisionCheck(Tile tile)
        {
            return hitbox.Intersects(tile.hitbox);
        }

        public bool CollisionCheck(GameObject gameObject)
        {
            return hitbox.Intersects(gameObject.hitbox);
        }

        public void AvailablePath(List<Tile> tileList)
        {
            left = true;
            right = true;
            up = true;
            down = true;

            for (int i = 0; i < tileList.Count; i++ )
            {
                if (leftHitbox.Intersects(tileList[i].hitbox))
                {
                    left = false;
                }

                else if (rightHitbox.Intersects(tileList[i].hitbox))
                {
                    right = false;
                }

                else if (upHitbox.Intersects(tileList[i].hitbox))
                {
                    up = false;
                }

                else if (downHitbox.Intersects(tileList[i].hitbox))
                {
                    down = false;
                }
            }
        }

    }
}
