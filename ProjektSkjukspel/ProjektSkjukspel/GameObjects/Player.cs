using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjektSkjukspel.GameObjects;
using ProjektSkjukspel.GameObjects.TileObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class Player : MoveableObject
    {
        public const int SPEED = 4;    //Has to be even.
        private int damage = 5;

        private bool upgraded = false;

        private Vector2 oldVelocity = new Vector2(SPEED, 0);

        public Player(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            this.position = position;
            this.texture = texture;
        }

        public override void Update()
        {
            PixelMovement();

            Movement();

            base.Update();
        }

        private void PixelMovement()
        {
            //Tile-based movement

            if (pixelCounter != Game1.tileSize && velocity != Vector2.Zero)
            {
                pixelCounter += SPEED;

                if (velocity.X == -SPEED)
                {
                    GameplayManager.drawOffsetX += Player.SPEED;
                }
                else if (velocity.X == SPEED)
                {
                   GameplayManager.drawOffsetX -= Player.SPEED;
                }
                else if (velocity.Y == -SPEED)
                {
                    GameplayManager.drawOffsetY += Player.SPEED;
                }
                else if (velocity.Y == SPEED)
                {
                    GameplayManager.drawOffsetY -= Player.SPEED;
                }

            }
            if (pixelCounter == Game1.tileSize)
            {
                oldVelocity = velocity;
                velocity = Vector2.Zero;
                pixelCounter = 0;
            }

        }

        private void Movement()
        {

            if (pixelCounter == 0)
            {
                #region Gamepad input
                if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X < -0.5f)
                {
                    velocity = new Vector2(-SPEED, 0);
                }
                else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X > 0.5f)
                {
                    velocity = new Vector2(SPEED, 0);
                }
                else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y < -0.5f)
                {
                    velocity = new Vector2(0, SPEED);
                }
                else if (GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y > 0.5f)
                {
                    velocity = new Vector2(0, -SPEED);
                }
                #endregion

                #region Keyboard input
                if (Input.KeyPressed(Keys.W) && up == true)
                {
                    velocity = new Vector2(0, -SPEED);
                }
                else if (Input.KeyPressed(Keys.S) && down == true)
                {
                    velocity = new Vector2(0, SPEED);
                }
                else if (Input.KeyPressed(Keys.A) && left == true)
                {
                    velocity = new Vector2(-SPEED, 0);
                }
                else if (Input.KeyPressed(Keys.D) && right == true)
                {
                    velocity = new Vector2(SPEED, 0);
                }
                #endregion
            }

            position += velocity;
        }

        public void CollisionHandler()
        {
            dead = true;
        }

        public void CheckUpgrade(PickUp pickUp)
        {
            if (!hitbox.Intersects(pickUp.hitbox))
                return;
            else
            {
                upgraded = true;
                damage = 10;
                color = Color.Black;
            }
        }

        public bool ProjectileCollision(Projectile projectile)
        {
            return hitbox.Intersects(projectile.hitbox);
        }

        #region Get
        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetEmitterLocation()
        {
            return position;
        }

        public Vector2 GetVelocity()
        {
            if (velocity == Vector2.Zero)
                return oldVelocity;
            else
                return velocity;
        }

        public bool GetDead()
        {
            return dead;
        }

        public bool GetUpgraded()
        {
            return upgraded;
        }

        public int GetDamage()
        {
            return damage;
        }
        #endregion

    }
}
