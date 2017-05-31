using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjektSkjukspel.GameObjects;
using ProjektSkjukspel.GameObjects.TileObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjektSkjukspel
{
    class PlayScreen : GameplayManager
    {
        private int elapsedTime;
        private int timerCount = 2;
        private int shootDuration = 50;
        private int enemySpawnDuration = 500;

        private Player player;

        private List<Projectile> projectileList = new List<Projectile>();
        private List<Projectile> enemyProjectileList = new List<Projectile>();

        public PlayScreen(Game1 game1)
            : base(game1)
        {
            LoadSpawn();

            foreach (Tile s in spawnPoints)
                if (s.texture == spawnPlayerTexture)
                    player = new Player(new Vector2(s.position.X, s.position.Y), playerTexture);

            LoadMap();

            foreach (Tile w in wallTiles)
                w.walkable = false;

            CenterCamera();
        }

        public override void Update(GameTime gameTime)
        {
            Timer(gameTime);

            InputUpdate();

            player.AvailablePath(wallTiles);

            player.Update();

            #region Projectile update
            foreach (Projectile p in projectileList)
            {
                p.Update();

                foreach (Tile w in wallTiles)
                {
                    if (p.WallCollision(w))
                    {
                        p.SetDead();
                    }
                }

                if (p.GetDead())
                {
                    projectileList.Remove(p);
                    break;
                }
            }

            foreach (Projectile p in enemyProjectileList)
            {
                p.Update();

                foreach (Tile w in wallTiles)
                {
                    if (p.WallCollision(w))
                    {
                        p.SetDead();
                    }
                }

                if (p.GetDead())
                {
                    enemyProjectileList.Remove(p);
                    break;
                }

                if (player.ProjectileCollision(p))
                {
                    player.CollisionHandler();
                }
            }
            #endregion

            #region Enemy update
            foreach (Enemy e in enemyList)
            {
                if (e.GetDirection() == Vector2.Zero)
                    e.FindPath(floorTiles, player.GetPosition());

                if (player.CollisionCheck(e))
                    player.CollisionHandler();

                foreach (Projectile p in projectileList)
                {
                    if (e.ProjectileCollision(p))
                    {
                        e.TakeDamage(p.damage);
                    }
                    if (p.CollisionCheck(e))
                    {
                        p.SetDead();
                    }
                }
                if (e.GetDead())
                {
                    enemyList.Remove(e);
                    break;
                }
            }
            #endregion

            foreach (PickUp p in pickUps)
            {
                player.CheckUpgrade(p);
                p.active = false;
            }
            //if (player.GetDead())
            //    game1.GameOver();

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tile w in wallTiles)
                w.Draw(spriteBatch);

            foreach (Tile t in floorTiles)
                t.Draw(spriteBatch);

            foreach (Projectile p in projectileList)
                p.Draw(spriteBatch);

            foreach (Projectile p in enemyProjectileList)
                p.Draw(spriteBatch);

            foreach (PickUp p in pickUps)
                p.Draw(spriteBatch);


            foreach (Enemy e in enemyList)
                e.Draw(spriteBatch);

            player.Draw(spriteBatch);
        }

        private void CenterCamera()
        {
            drawOffsetX = (Game1.windowWidth / 2) - ((int)player.GetPosition().X + (Game1.tileSize / 2));
            drawOffsetY = (Game1.windowHeight / 2) - ((int)player.GetPosition().Y + (Game1.tileSize / 2));
        }

        private void InputUpdate()
        {
            if (Input.KeyClick(Keys.Space))
            {
                Shoot(player.GetEmitterLocation(), projectileList, player.GetVelocity());
            }
        }

        private void Shoot(Vector2 emitterLocation, List<Projectile> paramProjectileList, Vector2 direction)
        {
            Projectile newProjectile = new Projectile(emitterLocation, projectileTexture, player.GetDamage());

            newProjectile.SetVelocity(direction);

            paramProjectileList.Add(newProjectile);
        }

        private void Timer(GameTime gameTime)
        {
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= timerCount)
            {
                --shootDuration;
                --enemySpawnDuration;
                elapsedTime -= shootDuration;
            }

            if (shootDuration <= 0)
            {
                foreach (Enemy e in enemyList)
                {
                    Shoot(e.GetEmitterLocation(), enemyProjectileList, e.GetDirection());
                }

                shootDuration = 50;
            }
            if (enemySpawnDuration <= 0)
            {
                LoadSpawn();

                enemySpawnDuration = 500;
            }
        }

    }
}
