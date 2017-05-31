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
        public bool walkable = true,
            visited = false;

        public Point position;
        public Texture2D texture;
        public Rectangle hitbox;

        private Tile travelledFrom;

        public Tile(Point position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;

            hitbox = new Rectangle(position.X, position.Y, texture.Width, texture.Height);
        }

        public virtual void Update(int drawOffsetX, int drawOffsetY)
        {
            hitbox.X = position.X + drawOffsetX;
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

        public List<Tile> FindNeighbors(ref List<Tile> tileList)
        {
            Tile left, right, up, down;
            List<Tile> outList = new List<Tile>();

            foreach (Tile t in tileList)
            {
                if (t.position == new Point(position.X + texture.Width, position.Y))
                {
                    //right
                    right = t;
                    outList.Add(right);
                }
                else if (t.position == new Point(position.X - texture.Width, position.Y))
                {
                    //left
                    left = t;
                    outList.Add(left);
                }
                else if (t.position == new Point(position.X, position.Y + texture.Width))
                {
                    //down
                    down = t;
                    outList.Add(down);
                }
                else if (t.position == new Point(position.X, position.Y - texture.Width))
                {
                    //up
                    up = t;
                    outList.Add(up);
                }

            }

            return outList;
        }

        public void SetTravelledFrom(Tile tile)
        {
            travelledFrom = tile;
        }

        public Tile GetTravelledFrom()
        {
            return travelledFrom;
        }

    }
}
