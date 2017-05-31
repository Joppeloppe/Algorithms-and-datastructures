using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjektSkjukspel.GameObjects.TileObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class MapEditor : GameplayManager
    {
        private int pixelCounter = 0;

        private HUD hud;
        private List<Tile> hudTiles = new List<Tile>();

        protected Tile tile;

        public MapEditor(Game1 game1) 
            : base(game1)
        {
            hud = new HUD(hudFrameTexture);
            tile = new Tile(Point.Zero, tileTargetTexture);

            LoadHudTiles();

            drawOffsetX = 0;
            drawOffsetY = 0;
        }

        public override void Update(GameTime gameTime)
        {
            EditorInput();
            CameraMovement();

            base.Update(gameTime);

            tile.position = new Point(screenPosition.X, screenPosition.Y);
            tile.Update(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile w in wallTiles)
                w.Draw(spriteBatch);

            foreach (Tile t in floorTiles)
                t.Draw(spriteBatch);

            foreach (Tile s in spawnPoints)
                s.Draw(spriteBatch);

            foreach (PickUp p in pickUps)
                p.Draw(spriteBatch);


            hud.Draw(spriteBatch);

            foreach (Tile t in hudTiles)
                t.Draw(spriteBatch);

            tile.Draw(spriteBatch);
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
                if (tile.texture == spawnEnemyTexture || tile.texture == spawnPlayerTexture)
                {
                    foreach (Tile s in spawnPoints)
                    {
                        if (s.IsSelected())
                        {
                            spawnPoints.Remove(s);
                            break;
                        }
                    }

                    if (!hud.hudTilesRectangle.Contains(Input.mousePosition))
                        spawnPoints.Add(new Tile(new Point((int)screenPosition.X - drawOffsetX,
                            (int)screenPosition.Y - drawOffsetY),
                            tile.texture));
                }
                else if (tile.texture == projectilePurplePickUpTexture)
                {
                    foreach (PickUp p in pickUps)
                    {
                        if (p.IsSelected())
                        {
                            pickUps.Remove(p);
                            break;
                        }
                    }

                    if (!hud.hudTilesRectangle.Contains(Input.mousePosition))
                    {
                        pickUps.Add(new PickUp(new Point((int)screenPosition.X - drawOffsetX,
                            (int)screenPosition.Y - drawOffsetY),
                            tile.texture));
                    }
                }
                else
                {
                    foreach (Tile t in floorTiles)
                    {
                        if (t.IsSelected())
                        {
                            floorTiles.Remove(t);
                            break;
                        }
                    }

                    foreach (Tile w in wallTiles)
                    {
                        if (w.IsSelected())
                        {
                            wallTiles.Remove(w);
                            break;
                        }
                    }


                    if (!hud.hudTilesRectangle.Contains(Input.mousePosition))
                    {
                        floorTiles.Add(new Tile(new Point((int)screenPosition.X - drawOffsetX,
                            (int)screenPosition.Y - drawOffsetY),
                            tile.texture));
                    }
                }
            }
            else if (Input.RightPress())
            {
                foreach (Tile t in floorTiles)
                {
                    if (t.IsSelected())
                    {
                        floorTiles.Remove(t);
                        break;
                    }
                }
                foreach (Tile s in spawnPoints)
                {
                    if (s.IsSelected())
                    {
                        spawnPoints.Remove(s);
                        break;
                    }
                }
            }

            if (Input.KeyClick(Keys.C))
            {
                floorTiles.Clear();
                wallTiles.Clear();
                spawnPoints.Clear();
            }
            else if (Input.KeyPressed(Keys.LeftControl) && Input.KeyClick(Keys.S) ||
                Input.KeyPressed(Keys.RightControl) && Input.KeyClick(Keys.S))
            {
                SaveMap();
                SaveSpawnPoints();
            }
            else if (Input.KeyPressed(Keys.LeftControl) && Input.KeyClick(Keys.L) ||
                Input.KeyPressed(Keys.RightControl) && Input.KeyClick(Keys.L))
            {
                LoadMap();
                LoadSpawn();
            }

        }

        public void CameraMovement()
        {
            if (pixelCounter == 0)
            {
                if (Input.KeyPressed(Keys.Space))
                {
                    drawOffsetX = 0;
                    drawOffsetY = 0;
                }

                if (Input.MiddlePress())
                {
                    Vector2 direction = new Vector2(Input.oldMousePosition.X - Input.mousePosition.X,
                        Input.oldMousePosition.Y - Input.mousePosition.Y);

                    direction.Normalize();

                    drawOffsetX -= (int)direction.X * (Game1.tileSize / 2);
                    drawOffsetY -= (int)direction.Y * (Game1.tileSize / 2);
                }

                if (Input.KeyPressed(Keys.Left))
                {
                    cameraDirection = new Vector2(-1, 0);
                }
                else if (Input.KeyPressed(Keys.Right))
                {
                    cameraDirection = new Vector2(1, 0);
                }
                else if (Input.KeyPressed(Keys.Up))
                {
                    cameraDirection = new Vector2(0, -1);
                }
                else if (Input.KeyPressed(Keys.Down))
                {
                    cameraDirection = new Vector2(0, 1);
                } 
            }

            //Tile-based movement
            if (pixelCounter != Game1.tileSize && cameraDirection != Vector2.Zero)
            {
                pixelCounter += Player.SPEED;

                if (cameraDirection.X == -1)
                {
                    drawOffsetX += Player.SPEED;
                }
                else if (cameraDirection.X == 1)
                {
                    drawOffsetX -= Player.SPEED;
                }
                else if (cameraDirection.Y == -1)
                {
                    drawOffsetY += Player.SPEED;
                }
                else if (cameraDirection.Y == 1)
                {
                    drawOffsetY -= Player.SPEED;
                }
            }
            if (pixelCounter == Game1.tileSize)
            {
                cameraDirection = Vector2.Zero;
                pixelCounter = 0;
            }
        }

        private void SaveMap()
        {
            streamWriter = new StreamWriter(@"mapTilePosition.txt", false);

            foreach (Tile t in floorTiles)
                streamWriter.WriteLine(t.ToString());
            
            foreach (Tile w in wallTiles)
                streamWriter.WriteLine(w.ToString());

            foreach (PickUp p in pickUps)
                streamWriter.WriteLine(p.ToString());

            streamWriter.Close();
        }

        private void SaveSpawnPoints()
        {
            streamWriter = new StreamWriter(@"spawnPointPosition.txt", false);

            foreach (Tile s in spawnPoints)
            {
                streamWriter.WriteLine(s.ToString());
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
