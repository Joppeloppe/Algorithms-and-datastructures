using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjektSkjukspel.GameObjects;
using ProjektSkjukspel.GameObjects.TileObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class GameplayManager : ContentManager
    {
        public static int drawOffsetX, drawOffsetY;
        protected Point screenPosition;
        protected Vector2 cameraDirection;

        protected List<Tile> floorTiles = new List<Tile>();
        protected List<Tile> wallTiles = new List<Tile>();
        protected List<Tile> spawnPoints = new List<Tile>();

        protected List<PickUp> pickUps = new List<PickUp>();

        protected List<Enemy> enemyList = new List<Enemy>();

        public GameplayManager(Game1 game1) 
            : base (game1)
        {
            LoadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            Input.Update();

            screenPosition = new Point(((int)Input.mousePosition.X / Game1.tileSize) * Game1.tileSize,
                ((int)Input.mousePosition.Y / Game1.tileSize) * Game1.tileSize);

            foreach (Tile t in floorTiles)
                t.Update(drawOffsetX, drawOffsetY);

            foreach (Tile w in wallTiles)
                w.Update(drawOffsetX, drawOffsetY);

            foreach (Tile s in spawnPoints)
                s.Update(drawOffsetX, drawOffsetY);

            foreach (Enemy e in enemyList)
                e.Update();

            foreach (PickUp p in pickUps)
                p.Update(drawOffsetX, drawOffsetY);
        }

        public void LoadMap()
        {
            streamReader = new StreamReader(@"mapTilePosition.txt");

            while (!streamReader.EndOfStream)
            {
                string[] split = streamReader.ReadLine().Split(';');

                Texture2D texture = GetTexture(split[2]);

                if (texture.Name == "tileWalkable")
                    floorTiles.Add(new Tile(new Point(int.Parse(split[0]), int.Parse(split[1])), texture));
                else if (texture.Name == "tileNonwalkable")
                    wallTiles.Add(new Tile(new Point(int.Parse(split[0]), int.Parse(split[1])), texture));
                else if (texture.Name == "projectilePurplePickUp")
                    pickUps.Add(new PickUp(new Point(int.Parse(split[0]), int.Parse(split[1])), texture));
            }

            streamReader.Close();
        }

        public void LoadSpawn()
        {
            spawnPoints.Clear();

            streamReader = new StreamReader(@"spawnPointPosition.txt");

            while (!streamReader.EndOfStream)
            {
                string[] split = streamReader.ReadLine().Split(';');

                Texture2D tileTexture = GetTexture(split[2]);

                spawnPoints.Add(new Tile(new Point(int.Parse(split[0]), int.Parse(split[1])), tileTexture));

                if (tileTexture == spawnEnemyTexture)
                    enemyList.Add(new Enemy(new Vector2(int.Parse(split[0]), int.Parse(split[1])), tileTexture, 10));
            }

            streamReader.Close();
        }
    }
}
