﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstPlayable
{
    
    internal class GameManager
    {

    private Stopwatch levelTimer = new Stopwatch();
    private Map map;
    private Player player;
    private Enemy goblin1;
    private Enemy boss;
    private Enemy runner;
    private Settings settings = new Settings();
    private List<Enemy> enemies = new List<Enemy>();


        public GameManager()
    {
            map = new Map("RPGMap.txt");
            player = new Player(settings.PlayerInitialHealth, settings.PlayerInitialDamage, settings.PlayerInitialLevel, map.initialPlayerPositionX, map.initialPlayerPositionY, map.layout);
            boss = new Enemy(settings.BossInitialHealth, settings.BossInitialDamage, 8, 8, true, "Boss", map.layout);
            goblin1 = new Enemy(settings.GoblinInitialHealth, settings.GoblinInitialDamage, map.initialEnemyPositionX, map.initialEnemyPositionY, "Goblin", map.layout);
            runner = new Enemy(settings.RunnerInitialHealth, settings.RunnerInitialDamage, map.initialEnemyPositionX, map.initialEnemyPositionY, "Runner", map.layout);
        }







        // Start up
    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Welcome to Dungeon Delve");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("\nYour goal is to collect seeds '&' around a dungeon map while avoiding or defeating the enemies.");
        Console.WriteLine("\nThe world is known as The Underworld");
        Console.WriteLine("---------------------------------------------------------------------------------------------");
        Console.WriteLine("You can attack by running into the enemy");
        Console.WriteLine("---------------------------------------------------------------------------------------------");
        Console.WriteLine("There are health packs '+' and Damage boosters '?' available to pickup during your adventure");
                
        Console.WriteLine("---------------------------------------------------------------------------------------------");
        Console.WriteLine("Goblins don't like conflict and aren't very aggressive!");
        Console.WriteLine("---------------------------------------------------------------------------------------------");
        Console.WriteLine("Runners chase you and hit you when they move into you!");
        Console.WriteLine("---------------------------------------------------------------------------------------------");
        Console.WriteLine("The Boss hits you back whenever you hit him");
        Console.WriteLine("---------------------------------------------------------------------------------------------");
        Console.WriteLine("Enemies can drop valuables, so try not to skip them!");
        Console.WriteLine("---------------------------------------------------------------------------------------------");
                
            
        Console.WriteLine("It's dangerous to go alone... good luck!");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);
        Console.Clear();
        Console.ResetColor();

            


            // game loop keeps on as long as the game isn't over or you haven't won   
        while (!player.gameOver)    
        {
            Console.CursorVisible = false;
            StartLevel();
            map.DrawMap(player, goblin1, boss, runner);
            DisplayHUD();
            DisplayLegend();
            PlayerInput();
            goblin1.EnemyMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player);
            boss.EnemyMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player);
            runner.RunnerMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player);
                

        }

        Console.Clear();

            // player wins
            if (player.youWin)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("You win!");
                Console.WriteLine($"\nYou collected: {player.currentSeeds} / 14 Seeds!");
                Console.WriteLine("Try to get more if you haven't got them all");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");
                EndLevel();
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("-------------------------------------------");
                Console.ReadKey(true);
            }
            // players dead
            else
            {
            Console.ForegroundColor = ConsoleColor.Red;
            int centerX = (Console.WindowWidth - "You died...".Length) / 2;
            Console.SetCursorPosition(centerX, Console.CursorTop);
            Console.WriteLine("You died...");
            
            Console.ReadKey(true);
        }
    }
        // displays the HUD
    private void DisplayHUD()
    {
        string currentEnemyInfo = player.currentEnemy != null ? $"{player.currentEnemy.Name} | HP Remaining: ({player.currentEnemy.healthSystem.GetCurrentHealth()}/{player.currentEnemy.healthSystem.GetMaximumHealth()})" : "None";
        Console.SetCursorPosition(0, map.mapHeight + 1);
        Console.WriteLine($"Player Health: {player.healthSystem.GetCurrentHealth()}/{player.healthSystem.GetMaximumHealth()} | Collected Seeds: {player.currentSeeds} | Attacking: {currentEnemyInfo}");
        

        RedrawLiveLog();
    }

    // displays the legend
    private void DisplayLegend()
    {
        Console.SetCursorPosition(0, map.mapHeight + 2);
        Console.WriteLine($"\nPlayer Damage Level: {player.playerDamage}");
    }

    private void StartLevel()
    {
        levelTimer.Start(); // Timer will start at beginning of level
    }

    private void EndLevel()
    {
        levelTimer.Stop(); // Timer stops at the end
        TimeSpan elapsedTime = levelTimer.Elapsed;

        
        string elapsedTimeString = String.Format("{0:00}:{1:00}:{2:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
        Console.WriteLine($"Level completed in: {elapsedTimeString}");
    }


    private void PlayerInput()
    {
        player.PlayerInput(map, goblin1, boss, runner);
    }


    


        public void DisplayLiveLog(List<string> liveLog)
    {
        Console.SetCursorPosition(0, map.mapHeight + 7);

        Console.WriteLine("Live Log:");

        int logLimit = Math.Min(3, liveLog.Count); // Limits log to 3 most recent messages
        int startIndex = liveLog.Count - logLimit; 

        for (int i = liveLog.Count - 1; i >= startIndex; i--)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.WriteLine(liveLog[i]);
        }

            
            Console.ResetColor();
    }

        public void RedrawLiveLog()
        {
            
            int startLine = map.mapHeight + 7;

            List<string> liveLog = player.GetLiveLog();

            
            int startIndex = Math.Max(0, liveLog.Count - 3); 

            
            for (int i = 2; i >= 0; i--)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            
            for (int i = 2; i >= 0; i--)
            {
                int index = startIndex + (2 - i);
                if (index >= 0 && index < liveLog.Count)
                {
                    string message = liveLog[index];
                    Console.SetCursorPosition(0, startLine + i);
                    Console.WriteLine(message);
                }
            }
        }
    }
}


