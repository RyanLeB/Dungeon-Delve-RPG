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
        

        // Health System
        public HealthSystem healthSystem;
        public int playerDamage { get; set; }
       
        // Player Position
        public int positionX { get; set; }
        public int positionY { get; set; }
        
        // Seeds
        public int currentSeeds { get; set; }
        
        // Game States
        public bool youWin { get; set; }
        public bool gameOver { get; set; }
        public bool levelComplete { get; set; }

        private char currentTile;
        
        // Settings
        public Settings settings = new Settings();

        // Enemy 
        public EnemyManager currentEnemy { get; set; }

        // Log list
        private List<string> liveLog;

        // Item Manager
        public ItemManager itemManager;

        public GameManager gameManager;

        public Map map;

        public List<EnemyManager> enemies;

        public Player(int maxHealth, int health, int damage, int startX, int startY, char[,] mapLayout, GameManager gameManager)
        {
            healthSystem = new HealthSystem(maxHealth);
            healthSystem.Heal(health);
            playerDamage = damage;
            positionX = startX;
            positionY = startY;
            currentTile = mapLayout[startY, startX];
            itemManager = new ItemManager(this);
            this.gameManager = gameManager;
            liveLog = new List<string>();
        }


        // Receives player input
        public void PlayerInput(Map map, List<EnemyManager> enemies)
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
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves down
            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(positionY + 1, map.mapHeight - 1);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves left
            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(positionX - 1, 0);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves right
            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(positionX + 1, map.mapWidth - 1);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            

            // exit game
            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }

        // handles things like collision checks and what the player is moving towards
        private void HandleMovement(Map map, List<EnemyManager> enemies, ref bool moved, ref int newPlayerPositionX, ref int newPlayerPositionY, int movementX, int movementY)
        {
            if (moved == false && map.layout[movementY, movementX] != '#')
            {

                foreach (var enemy in enemies)
                {
                    if (movementY == enemy.positionY && movementX == enemy.positionX)
                    {
                        currentEnemy = enemy;
                        enemy.healthSystem.Damage(playerDamage);
                        UpdateLiveLog($"Dealt {playerDamage} damage to {enemy.Name}");
                        if (healthSystem.IsDead())
                        {
                            gameOver = true;
                        }
                        
                        
                        
                        if (enemy.healthSystem.IsDead())
                        {
                           enemy.enemyAlive = false;
                            Console.SetCursorPosition(enemy.positionX, enemy.positionY);
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write('-');

                            enemy.positionX = 0;
                            enemy.positionY = 0;

                            enemy.enemyAlive = false;
                            UpdateLiveLog($"You Killed The {enemy.Name}");

                            if (enemy is Runner)
                            {
                                healthSystem.Heal(1);
                                UpdateLiveLog("Runner dropped health");
                                UpdateLiveLog("+1 Health Gained");
                            }
                            if (enemy is Boss)
                            {
                                itemManager.UseItem("Seed");
                            }

                        }
                        else if (enemy is Boss) // Check if the enemy is a Boss
                        {
                           
                            healthSystem.Damage(enemy.enemyDamage);
                            UpdateLiveLog($"Boss dealt {enemy.enemyDamage} damage to you!");
                            if (healthSystem.IsDead())
                            {
                                gameOver = true;
                            }
                            
                            
                            
                        }
                        
                        return;
                    }
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
                    else
                    {
                        
                        Console.SetCursorPosition(positionX, positionY);
                        Console.BackgroundColor = ConsoleColor.DarkGray; 
                        Console.Write(currentTile);

                        
                        currentTile = map.layout[movementY, movementX];

                        // Move the player
                        positionY = movementY;
                        positionX = movementX;
                        moved = true;

                        
                        Draw();
                    }
                    return;

                }

                if(map.layout[movementY, movementX] == '%')
                {
                    gameManager.ChangeLevel();
                }

                if (map.layout[movementY, movementX] == '>')
                {
                    youWin = true;
                    gameOver = true;
                }



                // collectable seeds
                if (map.layout[movementY, movementX] == '&')
                {
                   
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    itemManager.UseItem("Seed");
                    


                    
                    Console.SetCursorPosition(positionX, positionY);
                    Console.BackgroundColor = ConsoleColor.DarkGray; 
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    
                    // move the player
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;
                    
                }

                if (map.layout[movementY, movementX] == '+')
                {
                    
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    itemManager.UseItem("HealthPotion");
                    
                    Console.SetCursorPosition(positionX, positionY);


                    
                    Console.BackgroundColor = ConsoleColor.DarkGray; 
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;



                }

                if (map.layout[movementY, movementX] == '?')
                {
                    
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    itemManager.UseItem("DamageBoost");
                    Console.SetCursorPosition(positionX, positionY);


                    
                    Console.BackgroundColor = ConsoleColor.DarkGray; 
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
                    if (map.layout[movementY, movementX] == '-')
                    {
                        currentTile = map.layout[movementY, movementX];
                    }
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;
                }
            }
        }

        
        

        
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