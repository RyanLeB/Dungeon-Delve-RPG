using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    
        public class Game
        {
            private Map map;
            private Player player;
            private Enemy enemy;

            public Game()
            {
                map = new Map("RPGMap.txt");
                player = new Player(6,6, 1, map.initialPlayerPositionX, map.initialPlayerPositionY);
                enemy = new Enemy(3, 1, map.initialEnemyPositionX, map.initialEnemyPositionY);
            }

            public void Start()
            {
                Console.WriteLine("Welcome to my playable text RPG");
                Console.WriteLine("\nYour goal is to collect seeds around a dungeon map while avoiding or defeating the enemies.");
                Console.WriteLine("You can attack by either running into the enemy or pressing the spacebar when an enemy is close.");
                Console.WriteLine("It's dangerous to go alone... good luck!");
                Console.WriteLine("Press any key to start...");
                Console.ReadKey(true);
                Console.Clear();

                while (!player.gameOver)
                {
                    map.DrawMap(player, enemy);
                    DisplayHUD();
                    DisplayLegend();
                    PlayerInput();
                    enemy.EnemyMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout);

            }

            Console.Clear();
                if (player.youWin)
                {
                    Console.WriteLine("You win!");
                    Console.WriteLine($"\nYou collected: {player.currentSeeds} Seeds!");
                    Console.WriteLine("Try to get more if you haven't got them all");    
                    Console.ReadKey(true);
                }
                else
                {
                    Console.WriteLine("You died...");
                    Console.ReadKey(true);
                }
            }

            private void DisplayHUD()
            {
                Console.SetCursorPosition(0, map.mapHeight + 1);
                Console.WriteLine($"Player Health: {player.playerHealth}/{player.playerMaxHealth} | Collected Seeds: {player.currentSeeds} | Enemy Health: {enemy.enemyHealth}/{enemy.enemyMaxHealth}");
            }

            private void DisplayLegend()
            {
                Console.SetCursorPosition(0, map.mapHeight + 2);
                Console.WriteLine("Player = !" + "\nEnemy = E" + "\nWalls = #" + "\nFloor = -" + "\nSeeds = &" + "\nSpikeTrap = ^  Door: %");
            }

            private void PlayerInput()
            {
                player.PlayerInput(map, enemy);
            }
        }
    }

