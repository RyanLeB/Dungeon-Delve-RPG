using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FirstPlayable.Entity;

namespace FirstPlayable
{
    internal class Player : Entity
    {
        private List<Enemy> EnemyList;

        public int PlayerDMG;
        public int PlayerMaxHealth;
        public int PlayerExp;
        public int PlayerSeeds;
        public ConsoleKeyInfo PlayerInput;

        public bool GameOver;
        public bool GameWon;
        Player player;

        public Player()
        {
            EnemyList = new List<Enemy>();
            EntityPosition = new Position();
            HealthSystem.SetHealth(PlayerMaxHealth);

            PlayerSeeds = 0;
            PlayerMaxHealth = 0;
            PlayerExp = 0;
            PlayerDMG = 20;
        }

        public void SetPosition(int x, int y)
        {
            EntityPosition.X = x;
            EntityPosition.Y = y;
        }

        public void MaxPosition(Map map)
        {
            int mapX;
            int mapY;

            mapX = map.layout.GetLength(1);
            mapY = map.layout.GetLength(0);

            EntityPosition.maxX = mapX - 1;
            EntityPosition.maxY = mapY - 1;
        }

        public void HandlePlayerInput(Map map, Enemy enemy)
        {
            bool moved = false;

            int movementX;
            int movementY;

            moved = false;

            PlayerInput = Console.ReadKey(true);

            if (moved == false)

                // Up
                if (PlayerInput.Key == ConsoleKey.UpArrow || PlayerInput.Key == ConsoleKey.W)
                {
                    movementY = Math.Max(map.playerY - 1, 0);

                    if (movementY <= 0)
                    {
                        movementY = 0;
                    }

                    if (movementY == map.enemyY && EntityPosition.X == map.enemyX)
                    {
                        enemy.EnemyMaxHP -= 1;
                        if (enemy.HealthSystem.Health <= 0)
                        {
                            map.enemyX = 0;
                            enemy.EntityPosition.Y = 0;
                            enemy.Alive = false;
                        }

                        return;
                    }

                    if (map.layout[movementY, EntityPosition.X] == '^')
                    {
                        HealthSystem.Health -= 1;
                        if (HealthSystem.Health <= 0)
                        {
                            GameOver = true;
                        }
                    }

                    if (map.layout[movementY, EntityPosition.X] == '#')
                    {
                        movementY = EntityPosition.Y;
                        EntityPosition.Y = movementY;
                        return;
                    }

                    if (map.layout[movementY, EntityPosition.X] == 'E')
                    {
                        movementY = EntityPosition.Y;
                        EntityPosition.Y = movementY;
                        return;
                    }
                    else
                    {
                        moved = true;
                        EntityPosition.Y = movementY;
                        if (EntityPosition.Y <= 0)
                        {
                            EntityPosition.Y = 0;
                        }
                    }
                }

            // Down
            if (PlayerInput.Key == ConsoleKey.DownArrow || PlayerInput.Key == ConsoleKey.S)
            {
                movementY = Math.Min(EntityPosition.Y + 1, map.maximumY);

                if (movementY >= map.maximumY)
                {
                    movementY = map.maximumY;
                }

                if (movementY == enemy.EntityPosition.Y && EntityPosition.X == enemy.EntityPosition.X)
                {
                    enemy.HealthSystem.Health -= 1;
                    if (enemy.HealthSystem.Health <= 0)
                    {
                        enemy.EntityPosition.X = 0;
                        enemy.EntityPosition.Y = 0;
                        enemy.Alive = false;
                    }

                    return;
                }

                if (map.layout[movementY, EntityPosition.X] == '^')
                {
                    HealthSystem.Health -= 1;
                    if (HealthSystem.Health <= 0)
                    {
                        GameOver = true;
                    }
                }

                if (map.layout[movementY, EntityPosition.X] == '#')
                {
                    movementY = EntityPosition.Y;
                    EntityPosition.Y = movementY;
                    return;
                }

                if (map.layout[movementY, EntityPosition.X] == 'E')
                {
                    movementY = EntityPosition.Y;
                    EntityPosition.Y = movementY;
                    return;
                }
                else
                {
                    moved = true;
                    EntityPosition.Y = movementY;
                    if (EntityPosition.Y >= map.maximumY)
                    {
                        EntityPosition.Y = map.maximumY;
                    }
                }
            }

            // Left
            if (PlayerInput.Key == ConsoleKey.LeftArrow || PlayerInput.Key == ConsoleKey.A)
            {
                movementX = Math.Max(EntityPosition.X - 1, 0);

                if (movementX <= 0)
                {
                    movementX = 0;
                }

                if (movementX == enemy.EntityPosition.X && EntityPosition.Y == enemy.EntityPosition.Y)
                {
                    enemy.EntityPosition.Y -= 1;
                    if (enemy.HealthSystem.Health <= 0)
                    {
                        enemy.EntityPosition.X = 0;
                        enemy.EntityPosition.Y = 0;
                        enemy.Alive = false;
                    }
                    return;
                }

                if (map.layout[EntityPosition.Y, movementX] == '^')
                {
                    HealthSystem.Health -= 1;
                    if (HealthSystem.Health <= 0)
                    {
                        GameOver = true;
                    }
                }

                if (map.layout[EntityPosition.Y, movementX] == '#')
                {
                    movementX = EntityPosition.X;
                    EntityPosition.X = movementX;
                    return;
                }

                if (map.layout[EntityPosition.Y, movementX] == 'E')
                {
                    movementX = EntityPosition.X;
                    EntityPosition.X = movementX;
                    return;
                }
                else
                {
                    moved = true;
                    EntityPosition.X = movementX;
                    if (EntityPosition.X <= 0)
                    {
                        EntityPosition.X = 0;
                    }
                }
            }

            // Right
            if (PlayerInput.Key == ConsoleKey.RightArrow || PlayerInput.Key == ConsoleKey.D)
            {
                movementX = Math.Min(EntityPosition.X + 1, map.maximumX);

                if (movementX >= map.maximumX)
                {
                    movementX = map.maximumX;
                }

                if (movementX == enemy.EntityPosition.X && EntityPosition.Y == enemy.EntityPosition.Y)
                {
                    enemy.HealthSystem.Health -= 1;
                    if (enemy.HealthSystem.Health <= 0)
                    {
                        enemy.EntityPosition.X = 0;
                        enemy.EntityPosition.Y = 0;
                        enemy.Alive = false;
                    }
                    return;
                }

                if (map.layout[EntityPosition.Y, movementX] == '^')
                {
                    HealthSystem.Health -= 1;
                    if (HealthSystem.Health <= 0)
                    {
                        GameOver = true;
                    }
                }

                if (map.layout[EntityPosition.Y, movementX] == '#')
                {
                    movementX = EntityPosition.X;
                    EntityPosition.Y = movementX;
                    return;
                }

                if (map.layout[EntityPosition.Y, movementX] == 'E')
                {
                    movementX = EntityPosition.X;
                    EntityPosition.X = movementX;
                    return;
                }
                else
                {
                    moved = true;
                    EntityPosition.X = movementX;
                    if (EntityPosition.X >= map.maximumX)
                    {
                        EntityPosition.X = map.maximumX;
                    }
                }
            }

            // Winning door
            if (map.layout[EntityPosition.Y, EntityPosition.X] == '%')
            {
                GameWon = true;
                GameOver = true;
            }

            // Collectable seeds
            if (map.layout[EntityPosition.Y, EntityPosition.X] == '&')
            {
                PlayerSeeds += 1;
                map.layout[EntityPosition.Y, EntityPosition.X] = '~';
            }

            // Exit game
            if (PlayerInput.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }
    }
}