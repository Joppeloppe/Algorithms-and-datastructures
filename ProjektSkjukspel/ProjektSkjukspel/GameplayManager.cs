using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class GameplayManager : ContentManager
    {
        protected int drawOffsetX, drawOffsetY;
        protected Point screenPosition;

        protected Tile tile;

        public GameplayManager(Game1 game1) 
            : base (game1)
        {
            LoadContent();

            tile = new Tile(Point.Zero, tileTargetTexture);
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();

            screenPosition = new Point(((int)Input.mousePosition.X / Game1.tileSize) * Game1.tileSize,
                ((int)Input.mousePosition.Y / Game1.tileSize) * Game1.tileSize);

            tile.position = new Point(screenPosition.X, screenPosition.Y);
            tile.Update(0, 0);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            tile.Draw(spriteBatch);
        }

        public void CameraMovement()
        {

        }
    }
}
