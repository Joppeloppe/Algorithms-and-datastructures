using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class ContentManager
    {
        protected Game1 game1;
        protected Texture2D playerTexture, 
            tileWalkTexture, tileNonwalkTexture, tileTargetTexture, tileTransparent,
            hudFrameTexture, gameOverScreenTexture, spawnEnemyTexture, spawnPlayerTexture,
            projectileTexture, projectilePurpleTexture, projectilePurplePickUpTexture;

        protected StreamWriter streamWriter;
        protected StreamReader streamReader;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public ContentManager(Game1 game1)
        {
            this.game1 = game1;
        }

        public virtual void LoadContent()
        {
            playerTexture = game1.Content.Load<Texture2D>("playerTexture");

            tileWalkTexture = game1.Content.Load<Texture2D>("tileWalkable");
            tileNonwalkTexture = game1.Content.Load<Texture2D>("tileNonwalkable");
            tileTargetTexture = game1.Content.Load<Texture2D>("tileTarget");
            tileTransparent = game1.Content.Load<Texture2D>("tileTransparent");

            hudFrameTexture = game1.Content.Load<Texture2D>("hud_1024x768");
            gameOverScreenTexture = game1.Content.Load<Texture2D>("gameOver_1024x768");

            spawnEnemyTexture = game1.Content.Load<Texture2D>("spawnEnemy");
            spawnPlayerTexture = game1.Content.Load<Texture2D>("spawnPlayer");

            projectileTexture = game1.Content.Load<Texture2D>("projectile");
            projectilePurpleTexture = game1.Content.Load<Texture2D>("projectilePurple");
            projectilePurplePickUpTexture = game1.Content.Load<Texture2D>("projectilePurplePickUp");

            AddToDictionary();
        }

        private void AddToDictionary()
        {
            playerTexture.Name = "playerTexture";

            tileWalkTexture.Name = "tileWalkable";
            tileNonwalkTexture.Name = "tileNonwalkable";
            tileTargetTexture.Name = "tileTarget";
            tileTransparent.Name = "tileTransparent";

            hudFrameTexture.Name = "hud_1024x768";

            spawnEnemyTexture.Name = "spawnEnemy";
            spawnPlayerTexture.Name = "spawnPlayer";

            projectilePurplePickUpTexture.Name = "projectilePurplePickUp";

            textures.Add(playerTexture.Name, playerTexture);
            textures.Add(tileWalkTexture.Name, tileWalkTexture);
            textures.Add(tileNonwalkTexture.Name, tileNonwalkTexture);
            textures.Add(tileTargetTexture.Name, tileTargetTexture);
            textures.Add(tileTransparent.Name, tileTransparent);
            textures.Add(hudFrameTexture.Name, hudFrameTexture);
            textures.Add(spawnEnemyTexture.Name, spawnEnemyTexture);
            textures.Add(spawnPlayerTexture.Name, spawnPlayerTexture);
            textures.Add(projectilePurplePickUpTexture.Name, projectilePurplePickUpTexture);
        }

        public Texture2D GetTexture(string key)
        {
            if (textures.ContainsKey(key))
            {
                return textures[key];
            }

            return null;
        }
    }
}
