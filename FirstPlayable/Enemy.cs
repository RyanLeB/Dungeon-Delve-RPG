using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Enemy : Entity
    {
        private Player CurrentPlayer;
        
        public int enemyMaxHP;
        public int droppedEXP;
        public int enemyDamage;
        public int enemyMaxX;
        public int enemyMaxY;



        public Enemy()
        {

            healthSystem.SetHealth(enemyMaxHP);

            droppedEXP = 10;
            enemyDamage = 10;
            enemyMaxHP = 50;

        }

        public void enemyMaxPosition(Map map)
        {
            int mapX;
            int mapY;
            mapX = map.layout.GetLength(1);
            mapY = map.layout.GetLength(0);
            
            enemyMaxX = mapX - 1;
            enemyMaxY = mapY - 1;  
            
        }

        public void EnemyMove(Map map)
        {
            int movementX;
            int movementY;

            Random roll = new Random();
            int rollResult = roll.Next(1, 5);

            // enemy moving up
            if (rollResult == 1) 
            {
                movementY = entityPosition.y - 1;
                if(movementY <= 0) 
                {
                    movementY = 0;
                }
                if(movementY == map.playerY && entityPosition.x == map.playerX)
                {
                    CurrentPlayer.healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if (map.layout[movementY, entityPosition.x] == '#')
                {
                    movementY = map.enemy1Y;
                    entityPosition.y = movementY;
                    return;
                }
                else
                {
                    entityPosition.y = movementY;
                    if(entityPosition.y <= 0)
                    {
                        entityPosition.y = 0;
                    }
                }
            }

            // enemy moving down
            if(rollResult == 2)
            {
                movementY = entityPosition.y + 1;
                if(movementY >= enemyMaxY)
                {
                    movementY = enemyMaxY;
                }
                if(movementY == map.playerY && entityPosition.x == map.playerX)
                {
                    CurrentPlayer.healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if (map.layout[movementY, entityPosition.x] == '#')
                {
                    movementY = map.enemy1Y;
                    entityPosition.y = movementY;
                    return;
                }
                else
                {
                    entityPosition.y = movementY;
                    if(entityPosition.y >= enemyMaxY)
                    {
                        entityPosition.y = enemyMaxY;
                    }
                }

            }

            // enemy moving left 
            if(rollResult == 3)
            {
                movementX = entityPosition.x - 1;
                if(movementX >= enemyMaxX)
                {
                    movementX = enemyMaxX;
                }
                if(movementX <= 0)
                {
                    movementX = 0;
                }
                if(movementX == map.playerX && entityPosition.y == map.mapX)
                {
                    CurrentPlayer.healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                else
                {
                    entityPosition.x = movementX;
                    if(entityPosition.x <= 0)
                    {
                        entityPosition.x = 0;
                    }
                }
            }
            // enemy moving right
            if (rollResult == 4)
            {
                movementX = entityPosition.x + 1;
                if(movementX == map.playerX && entityPosition.y == map.playerY)
                {
                    CurrentPlayer.healthSystem.TakeDamage(enemyDamage);
                    return;
                }
                if (map.layout[entityPosition.y, movementX] == '#')
                {
                    movementX = entityPosition.x;
                    entityPosition.x = movementX;
                    return;
                }
                else
                {
                    entityPosition.x = movementX;
                    if(entityPosition.x >= enemyMaxX) 
                    {
                        entityPosition.x = enemyMaxX;
                    }
                }
            }

        }
        public void playerSet(Player player)
        {
            CurrentPlayer = player;
        }

    }
}
