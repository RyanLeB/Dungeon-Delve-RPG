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
        
        public HealthSystem healthSystem;
        public int playerDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int currentSeeds { get; set; }
        public bool youWin { get; set; }
        public bool gameOver { get; set; }
        public bool levelComplete { get; set; }

        private char currentTile;

        public Enemy currentEnemy { get; set; }

        // Log list
        private List<string> liveLog;

        public Player(int maxHealth, int health, int damage, int startX, int startY, char[,] mapLayout)
        {
            healthSystem = new HealthSystem(maxHealth);
            healthSystem.Heal(health);
            playerDamage = damage;
            positionX = startX;
            positionY = startY;
            currentTile = mapLayout[startY, startX];
            liveLog = new List<string>();
        }


        // recieves player input
        public void PlayerInput(Map map, Enemy goblin, Enemy boss, Enemy runner)
        {
            ConsoleKeyInfo playerController;
            bool moved = false;

            int movementX = positionX;
            int movementY = positionY;

            int newPlayerPositionX = positionX;
            int newPlayerPositionY = positionY;

            moved = false;

            playerController = Console.ReadKey(true);

            

            // moves up
            if (playerController.Key == ConsoleKey.UpArrow || playerController.Key == ConsoleKey.W)
            {
                movementY = Math.Max(positionY - 1, 0);
                HandleMovement(map, goblin, boss, runner, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves down
            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(positionY + 1, map.mapHeight - 1);
                HandleMovement(map, goblin, boss, runner, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves left
            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(positionX - 1, 0);
                HandleMovement(map, goblin, boss,runner, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves right
            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(positionX + 1, map.mapWidth - 1);
                HandleMovement(map, goblin, boss, runner, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            

            // exit game
            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }

        // handles things like collision checks and what the player is moving towards
        private void HandleMovement(Map map, Enemy goblin, Enemy boss, Enemy runner, ref bool moved, ref int newPlayerPositionX, ref int newPlayerPositionY, int movementX, int movementY)
        {
            if (moved == false && map.layout[movementY, movementX] != '#')
            {
                
                // Goblin

                if (movementY == goblin.positionY && movementX == goblin.positionX)
                {
                    currentEnemy = goblin;
                    goblin.healthSystem.Damage(playerDamage);
                    UpdateLiveLog($"Dealt {playerDamage} damage to Goblin");
                    if (healthSystem.IsDead())
                    {
                        gameOver = true;
                    }
                    if (goblin.healthSystem.IsDead())
                    {

                        Console.SetCursorPosition(goblin.positionX, goblin.positionY);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('-');
                        
                        goblin.positionX = 0;
                        goblin.positionY = 0;

                        goblin.enemyAlive = false;
                        UpdateLiveLog("You Killed The Goblin");
                        UpdateLiveLog("+1 Damage Gained");
                        playerDamage += 1;
                    }
                    return;
                }
                

                // Boss
                
                if (movementY == boss.positionY && movementX == boss.positionX)
                {
                    currentEnemy = boss;
                    boss.healthSystem.Damage(playerDamage);
                    healthSystem.Damage(2);
                    UpdateLiveLog($"Dealt {playerDamage} damage to the Boss");
                    UpdateLiveLog("Boss Deals -2 Damage");
                    if (healthSystem.IsDead())
                    {
                        gameOver = true;
                        
                    }

                    if (boss.healthSystem.IsDead())
                    {

                        Console.SetCursorPosition(boss.positionX, boss.positionY);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('-');


                        boss.positionX = 0;
                        boss.positionY = 0;
                        boss.enemyAlive = false;
                        UpdateLiveLog("You Killed The Boss!");
                        UpdateLiveLog("Received +2 Seeds");
                        currentSeeds += 2;
                    }
                    return;
                }
                
                
                // Runner
                
                if (movementY == runner.positionY && movementX == runner.positionX)
                {
                    currentEnemy = runner;
                    runner.healthSystem.Damage(playerDamage);
                    UpdateLiveLog($"Dealt {playerDamage} damage to Runner");
                    
                    if (healthSystem.IsDead())
                    {
                        gameOver = true;
                        
                    }

                    if (runner.healthSystem.IsDead())
                    {
                        Console.SetCursorPosition(runner.positionX, runner.positionY);
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('-');

                        runner.positionX = 0;
                        runner.positionY = 0;
                        runner.enemyAlive = false;
                        UpdateLiveLog("You Killed The Runner");
                        UpdateLiveLog("Healed +2 health");
                        healthSystem.Heal(2);
                    }
                    return;
                }

                // Spikes
                
                if (map.layout[movementY, movementX] == '^')
                {
                    healthSystem.Damage(1);
                    UpdateLiveLog("-1 Health");
                    if (healthSystem.IsDead())
                    {
                        gameOver = true;
                    }
                }

                // winning door
                if (map.layout[movementY, movementX] == '%')
                {
                    youWin = true;
                    gameOver = true;
                }

                // collectable seeds
                if (map.layout[movementY, movementX] == '&')
                {
                    currentSeeds += 1;
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    UpdateLiveLog("Picked up a seed!");
                    


                    // Replace the old position with the current tile
                    Console.SetCursorPosition(positionX, positionY);
                    Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color to dark gray
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;
                    
                }

                    






                if (map.layout[movementY, movementX] == '+')
                {
                    healthSystem.Heal(1);
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    UpdateLiveLog("Gained Health!");
                    
                    Console.SetCursorPosition(positionX, positionY);


                    
                    Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color to dark gray
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;



                }

                if (map.layout[movementY, movementX] == '?')
                {
                    playerDamage += 1;
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    UpdateLiveLog("Damage increased!");
                    Console.SetCursorPosition(positionX, positionY);


                    
                    Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color to dark gray
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;



                }



                if (map.layout[movementY, movementX] == 'E')
                {
                    
                    movementY = positionY;
                    movementX = positionX;
                    return;
                }

                else
                {
                    Console.SetCursorPosition(positionX, positionY);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(currentTile);


                    positionY = movementY;
                    positionX = movementX;

                    currentTile = map.layout[movementY, movementX];

                    moved = true;
                }
            }
        }

        // attack enemy method
        
        private void AttackEnemy(Enemy enemy)
        {
            if (Math.Abs(positionX - enemy.positionX) <= 1 && Math.Abs(positionY - enemy.positionY) <= 1)
            {
                enemy.healthSystem.Damage(playerDamage);
                if (enemy.healthSystem.IsDead())
                {
                    enemy.positionX = 0;
                    enemy.positionY = 0;
                    enemy.enemyAlive = false;
                }
            }
        }
        private void AttackBoss(Enemy boss)
        {
            if (Math.Abs(positionX - boss.positionX) <= 1 && Math.Abs(positionY - boss.positionY) <= 1)
            {
                boss.healthSystem.Damage(playerDamage);
                if (boss.healthSystem.IsDead())
                {
                    boss.positionX = 0;
                    boss.positionY = 0;
                    boss.enemyAlive = false;
                }
            }
        }

        // Picked up a seed (&)Health(?)
        // You Hit a Spike! -1 Health(?)

        // draws the player "!"
        public void Draw()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("!");
            Console.ResetColor();
        }

        public void UpdateLiveLog(string message) 
        {
            liveLog.Add(message);
            
        }

        public List<string> GetLiveLog()
        {
            return liveLog;
        }
    }
}