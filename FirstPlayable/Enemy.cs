using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    public class Enemy
    {
        public int enemyMaxHealth { get; private set; }
        public int enemyHealth { get; set; }
        public int enemyDamage { get; private set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool enemyAlive { get; set; }

        public Enemy(int maxHealth, int damage, int startX, int startY)
        {
            enemyMaxHealth = maxHealth;
            enemyHealth = maxHealth;
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            enemyAlive = true;
        }

        public void EnemyMovement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout)
        {
            int enemyMovementX = positionX;
            int enemyMovementY = positionY;
            int newEnemyPositionX = positionX;
            int newEnemyPositionY = positionY;

            // random roll to move
            Random randomRoll = new Random();

            // enemy will have 1 of 4 options to move
        if (enemyAlive == true) 
        { 
            int rollResult = randomRoll.Next(1, 5);
            while ((enemyMovementX == playerX && enemyMovementY == playerY) ||
                   (enemyMovementX == newEnemyPositionX && enemyMovementY == newEnemyPositionY) ||
                   mapLayout[enemyMovementY, enemyMovementX] == '#')
            {
                rollResult = randomRoll.Next(1, 5); // Retry if the position is the same as the player or a wall

                if (rollResult == 1)
                {
                    enemyMovementY = positionY + 1;
                    if (enemyMovementY >= mapHeight)
                    {
                        enemyMovementY = mapHeight - 1;
                    }
                }
                else if (rollResult == 2)
                {
                    enemyMovementY = positionY - 1;
                    if (enemyMovementY <= 0)
                    {
                        enemyMovementY = 0;
                    }
                }
                else if (rollResult == 3)
                {
                    enemyMovementX = positionX - 1;
                    if (enemyMovementX <= 0)
                    {
                        enemyMovementX = 0;
                    }
                }
                else // rollResult == 4
                {
                    enemyMovementX = positionX + 1;
                    if (enemyMovementX >= mapWidth)
                    {
                        enemyMovementX = mapWidth - 1;
                    }
                }
            }
            
        }
            
            
            // Update the enemy position
            positionY = enemyMovementY;
            positionX = enemyMovementX;
        }


        public void DrawEnemy()
        {
        if (enemyAlive == true) 
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("E");
            Console.ResetColor();
        }
        }
    }
}