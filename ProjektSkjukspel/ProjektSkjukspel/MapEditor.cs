using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class MapEditor : GameplayManager
    {
        private HUD hud;

        private List<Tile> hudTiles = new List<Tile>();
        private List<Tile> mapTiles = new List<Tile>();

        public MapEditor(Game1 game1) 
            : base(game1)
        {
            hud = new HUD(hudFrameTexture);

            LoadHudTiles();
        }

        public override void Update(GameTime gameTime)
        {
            EditorInput();

            base.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            hud.Draw(spriteBatch);

            foreach (Tile t in hudTiles)
                t.Draw(spriteBatch);

            foreach (Tile t in mapTiles)
                t.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        private void EditorInput()
        {
            if (Input.LeftClick())
            {
                foreach (Tile t in hudTiles)
                {
                    if (t.IsSelected())
                    {
                        tile.texture = t.texture;
                    }
                }
            }

            if (Input.LeftPress())
            {
                foreach (Tile t in mapTiles)
                {
                    if (t.IsSelected())
                    {
                        mapTiles.Remove(t);
                        break;
                    }
                }

                mapTiles.Add(new Tile(new Point((int)screenPosition.X - drawOffsetX,
                    (int)screenPosition.Y - drawOffsetY),
                    tile.texture));
            }
            else if (Input.RightPress())
            {
                foreach (Tile t in mapTiles)
                {
                    if (t.IsSelected())
                    {
                        mapTiles.Remove(t);
                        break;
                    }
                }
            }

            if (Input.KeyClick(Keys.C))
            {
                mapTiles.Clear();
            }
            else if (Input.KeyPressed(Keys.LeftControl) && Input.KeyClick(Keys.S) ||
                Input.KeyPressed(Keys.RightControl) && Input.KeyClick(Keys.S))
            {
                SaveMap();
            }
        }

        private void SaveMap()
        {
            streamWriter = new StreamWriter(@"mapTilePosition.txt", false);

            foreach (Tile t in mapTiles)
            {
                streamWriter.WriteLine(t.ToString());
            }

            streamWriter.Close();
        }

        private void LoadHudTiles()
        {
            streamReader = new StreamReader(@"hudTilePosition.txt", false);

            while (!streamReader.EndOfStream)
            {
                string[] split = streamReader.ReadLine().Split(';');

                Texture2D tileTexture = GetTexture(split[2]);

                hudTiles.Add(new Tile(new Point(int.Parse(split[0]), int.Parse(split[1])),
                    tileTexture));
            }

            streamReader.Close();
        }

    }
}
