using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class Player : GameObject
    {
        Vector2 velocity;
        const int SPEED = 4;    //Has to be even.
        int pixelCounter = 0;

        public Player(Vector2 position, Texture2D texture)
            : base(position, texture)
        {
            this.position = position;
            this.texture = texture;
        }

        public override void Update(GameTime gameTime)
        {
            //Tile-based movement
            if (pixelCounter != Game1.tileSize && velocity != Vector2.Zero)
            {
                pixelCounter += SPEED;
            }
            if (pixelCounter == Game1.tileSize)
            {
                velocity = Vector2.Zero;
                pixelCounter = 0;
            }

            Movement();

            base.Update(gameTime);
        }

        public void Movement()
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
                if (Input.KeyPressed(Keys.W))
                {
                    velocity = new Vector2(0, -SPEED);
                }
                else if (Input.KeyPressed(Keys.S))
                {
                    velocity = new Vector2(0, SPEED);
                }
                else if (Input.KeyPressed(Keys.A))
                {
                    velocity = new Vector2(-SPEED, 0);
                }
                else if (Input.KeyPressed(Keys.D))
                {
                    velocity = new Vector2(SPEED, 0);
                }
                #endregion
            }

            position += velocity;
        }
    }
}
