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

        public void getPlayerInput(Map map)
        {
            bool moved;
            moved = false;

            int movementX;
            int movementY;



            playerInput = Console.ReadKey(true);

            if (moved == false)
            {
                // moving player up
                if (playerInput.Key == ConsoleKey.W || playerInput.Key == ConsoleKey.UpArrow)
                {
                    movementY = (entityPosition.y - 1);
                    if (movementY <= 0)
                    {
                        movementY = 0;
                    }
                    if (map.layout[movementY, entityPosition.x] == map.layout[map.enemy1Y, map.enemy1X])
                    {
                        enemyList[0].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[movementY, entityPosition.x] == map.layout[map.enemy2Y, map.enemy2X])
                    {
                        enemyList[1].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[movementY, entityPosition.x] == map.layout[map.enemy3Y, map.enemy3X])
                    {
                        enemyList[2].healthSystem.TakeDamage(playerDMG);
                        return;
                    }

                    if (map.layout[movementY, entityPosition.x] == '#')
                    {
                        movementY = entityPosition.y;
                        entityPosition.y = movementY;
                        return;
                    }
                    else
                    {
                        moved = true;
                        entityPosition.y = movementY;
                        if (entityPosition.y <= 0)
                        {
                            entityPosition.y = 0;
                        }
                    }
                }
                // player moving down
                if (playerInput.Key == ConsoleKey.S || playerInput.Key == ConsoleKey.DownArrow)
                {

                    movementY = (entityPosition.y + 1);
                    if (movementY >= entityPosition.maxY)
                    {
                        movementY = entityPosition.maxY;
                    }
                    if (map.layout[movementY, entityPosition.x] == map.layout[map.enemy1Y, map.enemy1X])
                    {
                        enemyList[0].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[movementY, entityPosition.x] == map.layout[map.enemy2Y, map.enemy2X])
                    {
                        enemyList[1].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[movementY, entityPosition.x] == map.layout[map.enemy3Y, map.enemy3X])
                    {
                        enemyList[2].healthSystem.TakeDamage(playerDMG);
                        return;
                    }

                    if (map.layout[movementY, entityPosition.x] == '#')
                    {
                        movementY = entityPosition.y;
                        entityPosition.y = movementY;
                        return;
                    }
                    else
                    {
                        moved = true;
                        entityPosition.y = movementY;
                        if (entityPosition.y >= entityPosition.maxY)
                        {
                            entityPosition.y = entityPosition.maxY;
                        }
                    }
                }
                // player moving left
                if (playerInput.Key == ConsoleKey.A || playerInput.Key == ConsoleKey.LeftArrow)
                {

                    movementX = (entityPosition.x - 1);
                    if (movementX <= 0)
                    {
                        movementX = 0;
                    }
                    if (map.layout[entityPosition.y, movementX] == map.layout[map.enemy1Y, map.enemy1X])
                    {
                        enemyList[0].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[entityPosition.y, movementX] == map.layout[map.enemy2Y, map.enemy2X])
                    {
                        enemyList[1].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[entityPosition.y, movementX] == map.layout[map.enemy3Y, map.enemy3X])
                    {
                        enemyList[2].healthSystem.TakeDamage(playerDMG);
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
                        moved = true;
                        entityPosition.x = movementX;
                        if (entityPosition.x <= 0)
                        {
                            entityPosition.x = 0;
                        }
                    }
                }
                if (playerInput.Key == ConsoleKey.D || playerInput.Key == ConsoleKey.RightArrow)
                {
                    // moving player right
                    movementX = (entityPosition.x + 1);
                    if (movementX >= entityPosition.maxX)
                    {
                        movementX = entityPosition.maxX;
                    }
                    if (map.layout[entityPosition.y, movementX] == map.layout[map.enemy1Y, map.enemy1X])
                    {
                        enemyList[0].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[entityPosition.y, movementX] == map.layout[map.enemy2Y, map.enemy2X])
                    {
                        enemyList[1].healthSystem.TakeDamage(playerDMG);
                        return;
                    }
                    if (map.layout[entityPosition.y, movementX] == map.layout[map.enemy3Y, map.enemy3X])
                    {
                        enemyList[2].healthSystem.TakeDamage(playerDMG);
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
                        moved = true;
                        entityPosition.x = movementX;
                        if (entityPosition.x >= entityPosition.maxX)
                        {
                            entityPosition.x = entityPosition.maxX;
                        }
                    }
                }

                if (map.layout[entityPosition.y, entityPosition.x] == '$')
                {
                    gameWon = true;
                    gameOver = true;
                }

            }
            if (map.layout[entityPosition.y, entityPosition.x] == '@')
            {
                playerSeeds += 1;
                map.layout[entityPosition.y, entityPosition.x] = '-';
            }
            if (map.layout[entityPosition.y, entityPosition.x] == '"' && healthSystem.health < playerMaxHealth)
            {
                healthSystem.Heal(10, playerMaxHealth);
                map.layout[entityPosition.y, entityPosition.x] = '-';
            }
            if (map.layout[entityPosition.y, entityPosition.x] == '*')

                healthSystem.health -= 1;
            if (healthSystem.health <= 0)
            {
                gameOver = true;
                gameWon = false;
            }
        }


    }
}        
        

    
 


