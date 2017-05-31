using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class Tile
    {
        bool walkable = true;

        public Point position;
        public Texture2D texture;
        protected Rectangle hitbox;

        public Tile(Point position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;

            hitbox = new Rectangle(position.X, position.Y, texture.Width, texture.Height);
        }

        public virtual void Update(int drawOffsetX, int drawOffsetY)
        {
            hitbox.X = position.X+ drawOffsetX;
            hitbox.Y = position.Y + drawOffsetY;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, Color.White);
        }

        public override string ToString()
        {
            return position.X.ToString() + ';' + position.Y + ';' + texture.Name;
        }

        public bool IsSelected()
        {
            return hitbox.Contains(Input.mousePosition);
        }
    }
}
