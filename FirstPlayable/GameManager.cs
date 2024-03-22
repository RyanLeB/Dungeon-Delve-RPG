using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
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
        private List<EnemyManager> enemies = new List<EnemyManager>();
        private HUD hud;

        public string currentLevel { get; set; }


        public void Init()
        {

            // Initialization
            if (currentLevel == null)
            {
                currentLevel = settings.MapFileName;
            }
            map = new Map(currentLevel, enemies);
            player = new Player(settings.PlayerInitialHealth, settings.PlayerInitialDamage, settings.PlayerInitialLevel, map.initialPlayerPositionX, map.initialPlayerPositionY, map.layout, this);
            hud = new HUD(player, map);
            

            
        }
            

            
            
            
            


        public void Update()
        {
            map.UpdateMap(player, goblin, boss, runner);
            hud.UpdateLegend();
            hud.UpdateHUD();
        }


        public void Draw()
        {
            Display();
            foreach (var enemy in enemies)
            {
                enemy.DrawMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player, enemies);
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

            

       

        // game loop uses Init/Update/Draw methods
        
        Init();
        while (!player.gameOver)    
        {
            Console.CursorVisible = false;
            StartLevel();
            
            Update();

            Draw();
            
            
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



        public void ChangeLevel()
        {
            enemies.Clear();  
            
            
            
            if (currentLevel == "RPGMap.txt")
            {
                currentLevel = "RPGMap2.txt";
            }
            else if (currentLevel == "RPGMap2.txt")
            {
                currentLevel = "RPGMap3.txt";
            }

            
            
            map = new Map(currentLevel, enemies);
            
            player.positionX = map.initialPlayerPositionX;
            player.positionY = map.initialPlayerPositionY;
            
            Console.Clear();
            
            map.UpdateMap(player, goblin, boss, runner);
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


        private void Display()
        {
            player.PlayerInput(map, enemies);
           
        }
            
            

            
    }

}



