using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        
    


    public GameManager()
    {
        map = new Map("RPGMap.txt");
        player = new Player(10,10, 1, map.initialPlayerPositionX, map.initialPlayerPositionY);
        boss = new Enemy(12, 2, 8, 8, true, "Boss");
        goblin1 = new Enemy(3, 0, map.initialEnemyPositionX, map.initialEnemyPositionY, "Goblin");
        runner = new Enemy(1, 2, map.initialEnemyPositionX, map.initialEnemyPositionY, "Runner");

        

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
                Console.ForegroundColor = ConsoleColor.Green;
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
        List<string> liveLog = player.GetLiveLog();
            
        DisplayLiveLog(liveLog);
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



    private void DisplayLiveLog(List<string> liveLog)
    {
        Console.SetCursorPosition(0, map.mapHeight + 7);

        Console.WriteLine("Live Log:");

        int logLimit = Math.Min(3, liveLog.Count); // Limits log to 3 most recent messages
        int startIndex = liveLog.Count - logLimit; 

        for (int i = liveLog.Count - 1; i >= startIndex; i--)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(liveLog[i]);
        }
        Console.ResetColor();
    }
}
}


