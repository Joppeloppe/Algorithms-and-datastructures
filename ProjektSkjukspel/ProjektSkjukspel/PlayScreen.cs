using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class PlayScreen : GameplayManager
    {
        Player player;

        public PlayScreen(Game1 game1)
            : base(game1)
        {
            player = new Player(Vector2.Zero, playerTexture);
        }

        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

    }
}
