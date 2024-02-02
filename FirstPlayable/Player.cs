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

        // Enemy list
        private List<Enemy> enemyList;

        // player stats
        public int playerDMG;
        public int playerMaxHealth;
        public int playerExp;
        public int playerSeeds;
        public ConsoleKeyInfo playerInput;

        // winning or ending game bool
        public bool gameOver;
        public bool gameWon;



        public Player()
        {
            enemyList = new List<Enemy>();

            healthSystem.SetHealth(playerMaxHealth);


            playerSeeds = 0;
            playerMaxHealth = 0;
            playerExp = 0;
            playerDMG = 20;

        }

        public void setPosition(int x, int y)
        {
            entityPosition.x = x;
            entityPosition.y = y;
        }


        public void maxPosition(Map map)
        {
            int mapX;
            int mapY;

            mapX = map.layout.GetLength(1);
            mapY = map.layout.GetLength(0);

            entityPosition.maxX = mapX - 1;
            entityPosition.maxY = mapY - 1;
        }

        public void PlayerInput(Map map)
        {
            bool moved = false;

            int movementX;
            int movementY;

            

            moved = false;

            playerInput = Console.ReadKey(true);

            if (moved == false)

               





            // Up

            if (playerInput.Key == ConsoleKey.UpArrow || playerInput.Key == ConsoleKey.W)
            {
                movementY = Math.Max(map.playerY - 1, 0);

                if (movementY <= 0)
                {
                    movementY = 0;
                }
                if (movementY == map.enemyY && map.playerX == map.enemyX)
                {
                    enemy.enemyMaxHP -= 1;
                    if (enemyHealth <= 0)
                    {
                        map.enemyX = 0;
                        enemyPositionY = 0;
                        enemyAlive = false;
                    }

                    return;
                }


                if (map.layout[movementY, playerPositionX] == '^')
                {
                    playerHealth -= 1;
                    if (playerHealth <= 0)
                    {
                        gameOver = true;
                    }
                }

                if (layout[movementY, playerPositionX] == '#')
                {
                    movementY = playerPositionY;
                    playerPositionY = movementY;
                    return;
                }
                if (layout[movementY, playerPositionX] == 'E')
                {
                    movementY = playerPositionY;
                    playerPositionY = movementY;
                    return;
                }

                else
                {
                    moved = true;
                    playerPositionY = movementY;
                    if (playerPositionY <= 0)
                    {
                        playerPositionY = 0;
                    }
                }
            }





            // Down

            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(playerPositionY + 1, maximumY);

                if (movementY >= maximumY)
                {
                    movementY = maximumY;
                }
                if (movementY == enemyPositionY && playerPositionX == enemyPositionX)
                {
                    enemyHealth -= 1;
                    if (enemyHealth <= 0)
                    {
                        enemyPositionX = 0;
                        enemyPositionY = 0;
                        enemyAlive = false;
                    }

                    return;
                }
                if (layout[movementY, playerPositionX] == '^')
                {
                    playerHealth -= 1;
                    if (playerHealth <= 0)
                    {
                        gameOver = true;
                    }
                }

                if (layout[movementY, playerPositionX] == '#')
                {
                    movementY = playerPositionY;
                    playerPositionY = movementY;
                    return;
                }

                if (layout[movementY, playerPositionX] == 'E')
                {
                    movementY = playerPositionY;
                    playerPositionY = movementY;
                    return;
                }
                else
                {
                    moved = true;
                    playerPositionY = movementY;
                    if (playerPositionY >= maximumY)
                    {
                        playerPositionY = maximumY;
                    }


                }
            }

            // Left

            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(playerPositionX - 1, 0);

                if (movementX <= 0)
                {
                    movementX = 0;
                }
                if (movementX == enemyPositionX && playerPositionY == enemyPositionY)
                {
                    enemyHealth -= 1;
                    if (enemyHealth <= 0)
                    {
                        enemyPositionX = 0;
                        enemyPositionY = 0;
                        enemyAlive = false;
                    }
                    return;
                }

                if (layout[playerPositionY, movementX] == '^')
                {
                    playerHealth -= 1;
                    if (playerHealth <= 0)
                    {
                        gameOver = true;
                    }
                }
                if (layout[playerPositionY, movementX] == '#')
                {
                    movementX = playerPositionX;
                    playerPositionX = movementX;

                    return;
                }
                if (layout[playerPositionY, movementX] == 'E')
                {
                    movementX = playerPositionX;
                    playerPositionX = movementX;

                    return;
                }
                else
                {
                    moved = true;
                    playerPositionX = movementX;
                    if (playerPositionX <= 0)
                    {
                        playerPositionX = 0;
                    }
                }
            }


            // Right

            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(playerPositionX + 1, maximumX);

                if (movementX >= maximumX)
                {
                    movementX = maximumX;
                }
                if (movementX == enemyPositionX && playerPositionY == enemyPositionY)
                {
                    enemyHealth -= 1;
                    if (enemyHealth <= 0)
                    {
                        enemyPositionX = 0;
                        enemyPositionY = 0;
                        enemyAlive = false;
                    }
                    return;
                }

                if (layout[playerPositionY, movementX] == '^')
                {
                    playerHealth -= 1;
                    if (playerHealth <= 0)
                    {
                        gameOver = true;
                    }

                }


                if (layout[playerPositionY, movementX] == '#')
                {
                    movementX = playerPositionX;
                    playerPositionX = movementX;
                    return;
                }
                if (layout[playerPositionY, movementX] == 'E')
                {
                    movementX = playerPositionX;
                    playerPositionX = movementX;
                    return;
                }

                else
                {
                    moved = true;
                    playerPositionX = movementX;
                    if (playerPositionX >= maximumX)
                    {
                        playerPositionX = maximumX;
                    }
                }

            }

            // Winning door

            if (layout[playerPositionY, playerPositionX] == '%')
            {
                youWin = true;
                gameOver = true;

            }

            // Collectable seeds
            if (layout[playerPositionY, playerPositionX] == '&')
            {
                currentSeeds += 1;
                layout[playerPositionY, playerPositionX] = '~';
            }

            // Exit game
            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }


        }

    }
}        
        

    
 


