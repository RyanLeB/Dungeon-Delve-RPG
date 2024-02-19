using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Enemy
    {
        // variables | encapsulation
        
        public int maximumHealth { get; set; }
        public int currentHealth { get; set; }
        public int enemyDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool enemyAlive { get; set; }

        public Enemy(int maxHealth, int damage, int startX, int startY)
        {
            maximumHealth = maxHealth;
            currentHealth = maxHealth;
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

            // checks if enemy is alive so it doesn't bug out when it is actually killed
        if (enemyAlive == true) 
        { 
            int rollResult = randomRoll.Next(1, 5);
            while ((enemyMovementX == playerX && enemyMovementY == playerY) ||
                   (enemyMovementX == newEnemyPositionX && enemyMovementY == newEnemyPositionY) ||
                   mapLayout[enemyMovementY, enemyMovementX] == '#')
            {
                // retries the role
                rollResult = randomRoll.Next(1, 5);

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
            
            
            // Updates the enemies position
            positionY = enemyMovementY;
            positionX = enemyMovementX;
        }





        // draws the enemy
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