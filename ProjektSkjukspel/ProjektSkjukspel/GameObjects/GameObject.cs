using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjektSkjukspel
{
    class GameObject
    {
        protected Vector2 position;
        protected Texture2D texture;
        public Rectangle hitbox;

        protected bool dead;

        protected Color color;

        public GameObject(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;

            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            color = Color.White;
        }

        public virtual void Update()
        {
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            hitbox.X += GameplayManager.drawOffsetX;
            hitbox.Y += GameplayManager.drawOffsetY;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, hitbox, color);
        }

        public void SetDead()
        {
            dead = true;
        }

        public bool GetDead()
        {
            return dead;
        }

    }
}
