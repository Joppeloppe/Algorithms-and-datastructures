using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class HUD
    {
        Texture2D texture;
        public Rectangle hudTilesRectangle;

        public HUD(Texture2D texture)
        {
            this.texture = texture;

            hudTilesRectangle = new Rectangle(Point.Zero, new Point(1024, 128));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Zero, Color.White);
        }
    }
}
