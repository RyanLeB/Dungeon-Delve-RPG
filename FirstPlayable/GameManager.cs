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
    Boss boss;
    Goblin goblin;
    Runner runner;
    private Settings settings = new Settings();
    private List<Enemy> enemies = new List<Enemy>();
    private HUD hud;


        public GameManager()
        {
            map = new Map("RPGMap.txt", enemies);
            
            player = new Player(settings.PlayerInitialHealth, settings.PlayerInitialDamage, settings.PlayerInitialLevel, map.initialPlayerPositionX, map.initialPlayerPositionY, map.layout);

            
            hud = new HUD(player, map);
            
            
            
            for (int i = 0; i < 10; i++)
            {
                goblin = new Goblin(settings.GoblinInitialHealth, settings.GoblinInitialDamage + i, map.initialEnemyPositionX + i, map.initialEnemyPositionY, "Goblin", map.layout);
                enemies.Add(goblin);
            }

            for (int i = 0; i < 1; i++)
            {
                boss = new Boss(settings.BossInitialHealth, settings.BossInitialDamage, map.initialEnemyPositionX, map.initialEnemyPositionY, "Boss", map.layout);
                enemies.Add(boss);
            }
            
            for (int i = 0; i < 1; i++)
            {
                runner = new Runner(settings.RunnerInitialHealth, settings.RunnerInitialDamage, map.initialEnemyPositionX, map.initialEnemyPositionY, "Runner", map.layout);
                enemies.Add(runner);
            }
            
            
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
            map.DrawMap(player, goblin, boss, runner);
            hud.DisplayHUD();
            
            hud.DisplayLegend();
            PlayerInput();

            foreach (var enemy in enemies)
            {
                enemy.Move(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player, enemies);
            }

        }



            Console.Clear();

            // player wins
            if (player.youWin)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("You win!");
                Console.WriteLine($"\nYou collected: {player.currentSeeds} / 11 Seeds!");
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
        try
            {
            player.PlayerInput(map, enemies);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}


