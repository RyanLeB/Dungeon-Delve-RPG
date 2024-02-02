using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    class Enemy : Entity
    {
        private Player CurrentPlayer;

        public int EnemyMaxHP;
        public int DroppedEXP;
        public int EnemyDamage;
        public int EnemyMaxX;
        public int EnemyMaxY;
        public bool Alive;

        public Enemy()
        {
            EnemyMaxHP = 50;
            HealthSystem.SetHealth(EnemyMaxHP);

            DroppedEXP = 10;
            EnemyDamage = 10;
            Alive = true;
        }

        public void EnemyMaxPosition(Map map)
        {
            int mapX;
            int mapY;
            mapX = map.layout.GetLength(1);
            mapY = map.layout.GetLength(0);

            EnemyMaxX = mapX - 1;
            EnemyMaxY = mapY - 1;
        }

        public void EnemyMove(Map map)
        {
            if (!Alive)
            {
                return;
            }

            int movementX;
            int movementY;

            Random roll = new Random();
            int rollResult = roll.Next(1, 5);

            // enemy moving up
            if (rollResult == 1)
            {
                movementY = EntityPosition.Y - 1;
                if (movementY <= 0)
                {
                    movementY = 0;
                }
                if (movementY == map.playerY && EntityPosition.X == map.playerX)
                {
                    CurrentPlayer.HealthSystem.TakeDamage(EnemyDamage);
                    return;
                }
                if (map.layout[movementY, EntityPosition.X] == '#')
                {
                    movementY = map.enemyY;
                    EntityPosition.Y = movementY;
                    return;
                }
                else
                {
                    EntityPosition.Y = movementY;
                    if (EntityPosition.Y <= 0)
                    {
                        EntityPosition.Y = 0;
                    }
                }
            }

            // enemy moving down
            if (rollResult == 2)
            {
                movementY = EntityPosition.Y + 1;
                if (movementY >= EnemyMaxY)
                {
                    movementY = EnemyMaxY;
                }
                if (movementY == map.playerY && EntityPosition.X == map.playerX)
                {
                    CurrentPlayer.HealthSystem.TakeDamage(EnemyDamage);
                    return;
                }
                if (map.layout[movementY, EntityPosition.X] == '#')
                {
                    movementY = map.enemyY;
                    EntityPosition.Y = movementY;
                    return;
                }
                else
                {
                    EntityPosition.Y = movementY;
                    if (EntityPosition.Y >= EnemyMaxY)
                    {
                        EntityPosition.Y = EnemyMaxY;
                    }
                }
            }

            // enemy moving left
            if (rollResult == 3)
            {
                movementX = EntityPosition.X - 1;
                if (movementX >= EnemyMaxX)
                {
                    movementX = EnemyMaxX;
                }
                if (movementX <= 0)
                {
                    movementX = 0;
                }
                if (movementX == map.playerY && EntityPosition.Y == map.playerX)
                {
                    CurrentPlayer.HealthSystem.TakeDamage(EnemyDamage);
                    return;
                }
                else
                {
                    EntityPosition.X = movementX;
                    if (EntityPosition.X <= 0)
                    {
                        EntityPosition.X = 0;
                    }
                }
            }
            // enemy moving right
            if (rollResult == 4)
            {
                movementX = EntityPosition.X + 1;
                if (movementX == map.playerX && EntityPosition.Y == map.playerY)
                {
                    CurrentPlayer.HealthSystem.TakeDamage(EnemyDamage);
                    return;
                }
                if (map.layout[EntityPosition.Y, movementX] == '#')
                {
                    movementX = EntityPosition.X;
                    EntityPosition.X = movementX;
                    return;
                }
                else
                {
                    EntityPosition.X = movementX;
                    if (EntityPosition.X >= EnemyMaxX)
                    {
                        EntityPosition.X = EnemyMaxX;
                    }
                }
            }
        }

        public void PlayerSet(Player player)
        {
            CurrentPlayer = player;
        }
    }
}