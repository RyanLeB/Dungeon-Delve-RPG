using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static FirstPlayable.Entity;

namespace FirstPlayable
{
    internal class Player 
    {
        // variables | encapsulation
        
        public int maximumHealth { get; set; }
        public int currentHealth { get; set; }
        public int playerDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int currentSeeds { get; set; }
        public bool youWin { get; set; }
        public bool gameOver { get; set; }
        public bool levelComplete { get; set; }

        public Player(int maxHealth, int health, int damage, int startX, int startY)
        {
            maximumHealth = maxHealth;
            currentHealth = health;
            playerDamage = damage;
            positionX = startX;
            positionY = startY;
        }

        // recieves player input
        public void PlayerInput(Map map, Enemy enemy)
        {
            ConsoleKeyInfo playerController;
            bool moved = false;

            int movementX = positionX;
            int movementY = positionY;

            int newPlayerPositionX = positionX;
            int newPlayerPositionY = positionY;

            moved = false;

            playerController = Console.ReadKey(true);

            if (moved == false && playerController.Key == ConsoleKey.Spacebar)
            {
                AttackEnemy(enemy);
                moved = true;
                return;
            }

            // moves up
            if (playerController.Key == ConsoleKey.UpArrow || playerController.Key == ConsoleKey.W)
            {
                movementY = Math.Max(positionY - 1, 0);
                HandleMovement(map, enemy, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves down
            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(positionY + 1, map.mapHeight - 1);
                HandleMovement(map, enemy, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves left
            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(positionX - 1, 0);
                HandleMovement(map, enemy, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves right
            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(positionX + 1, map.mapWidth - 1);
                HandleMovement(map, enemy, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // winning door
            if (map.layout[positionY, positionX] == '%')
            {
                youWin = true;
                gameOver = true;
            }

            // collectable seeds
            if (map.layout[positionY, positionX] == '&')
            {
                currentSeeds += 1;
                map.layout[positionY, positionX] = '~';
            }

            // exit game
            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }

        // handles things like collision checks and what the player is moving towards
        private void HandleMovement(Map map, Enemy enemy, ref bool moved, ref int newPlayerPositionX, ref int newPlayerPositionY, int movementX, int movementY)
        {
            if (moved == false && map.layout[movementY, movementX] != '#')
            {
                if (movementY == enemy.positionY && movementX == enemy.positionX)
                {
                    enemy.currentHealth -= 1;
                    if (enemy.currentHealth <= 0)
                    {
                        enemy.positionX = 0;
                        enemy.positionY = 0;
                        enemy.enemyAlive = false;
                    }
                    return;
                }

                if (map.layout[movementY, movementX] == '^')
                {
                    currentHealth -= 1;
                    if (currentHealth <= 0)
                    {
                        gameOver = true;
                    }
                }

                if (map.layout[movementY, movementX] == 'E')
                {
                    movementY = positionY;
                    movementX = positionX;
                    return;
                }

                else
                {
                    moved = true;
                    positionY = movementY;
                    positionX = movementX;
                }
            }
        }

        // attack enemy method
        
        private void AttackEnemy(Enemy enemy)
        {
            if (Math.Abs(positionX - enemy.positionX) <= 1 && Math.Abs(positionY - enemy.positionY) <= 1)
            {
                enemy.currentHealth -= 1;
                if (enemy.currentHealth <= 0)
                {
                    enemy.positionX = 0;
                    enemy.positionY = 0;
                    enemy.enemyAlive = false;
                }
            }
        }

        // draws the player "!"
        public void DrawPlayer()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("!");
            Console.ResetColor();
        }
    }
}