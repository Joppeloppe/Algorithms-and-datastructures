using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel.Screens
{
    class GameOverScreen : ContentManager
    {

        public GameOverScreen(Game1 game1)
            : base(game1)
        {
            LoadContent();
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gameOverScreenTexture, Vector2.Zero, Color.White);
        }
    }
}
