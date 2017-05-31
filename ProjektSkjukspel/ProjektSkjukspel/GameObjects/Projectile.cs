using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel.GameObjects
{
    class Projectile : GameObject
    {
        Vector2 velocity;
        private const int SPEED = 4;
        public int damage;

        public Projectile(Vector2 position, Texture2D texture, int damage)
            : base(position, texture)
        {
            this.damage = damage;
        }

        public override void Update()
        {
            position += velocity * SPEED;

            base.Update();
        }

        public void SetVelocity(Vector2 newVelocity)
        {
            velocity = newVelocity;
        }

        public bool WallCollision(Tile tile)
        {
            return hitbox.Intersects(tile.hitbox);
        }

        public bool CollisionCheck(GameObject gameObject)
        {
            return hitbox.Intersects(gameObject.hitbox);
        }
    }
}
