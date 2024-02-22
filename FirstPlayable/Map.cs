using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Map
    {
        // variables | encapsulation
        
        private string path;
        private string[] floor;
        public char[,] layout;

        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public int initialPlayerPositionX { get; set; }
        public int initialPlayerPositionY { get; set; }
        public int initialEnemyPositionX { get; set; }
        public int initialEnemyPositionY { get; set; }

        public Map(string mapFileName)
        {
            path = mapFileName;
            floor = File.ReadAllLines(path);
            CreateMap();
        }

        
        // creates map
        private void CreateMap()
        {
            mapWidth = floor[0].Length;
            mapHeight = floor.Length;
            layout = new char[mapHeight, mapWidth];

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    layout[i, j] = floor[i][j];

                    if (layout[i, j] == '!')
                    {
                        initialPlayerPositionX = j;
                        initialPlayerPositionY = i;
                    }
                    else if (layout[i, j] == 'E')
                    {
                        initialEnemyPositionX = j;
                        initialEnemyPositionY = i;
                    }
                }
            }
        }

        // draws out map on screen
        public void DrawMap(Player player, Enemy enemy, Enemy boss, Enemy runner)
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.DarkGray;

            for (int k = 0; k < mapHeight; k++)
            {
                for (int l = 0; l < mapWidth; l++)
                {
                    char tile = layout[k, l];
                    switch (tile)
                    {
                        case '=':
                            Console.ForegroundColor = ConsoleColor.Yellow; 
                            break;
                        case '-':
                            Console.ForegroundColor = ConsoleColor.Gray; 
                            break;
                        case '&':
                            Console.ForegroundColor = ConsoleColor.Cyan; 
                            break;
                        case '^':
                            Console.ForegroundColor = ConsoleColor.Gray; 
                            break;
                        case '!':
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case 'G':
                            Console.ForegroundColor = ConsoleColor.Red; 
                            break;
                        case 'B':
                            Console.ForegroundColor = ConsoleColor.Magenta; 
                            break;
                        case '#':
                            Console.ForegroundColor = ConsoleColor.Gray; 
                            break;
                        case '%':
                            Console.ForegroundColor = ConsoleColor.Yellow; 
                            break;
                        case '+':
                            Console.ForegroundColor = ConsoleColor.DarkGreen; 
                            break;
                        case '?':
                            Console.ForegroundColor = ConsoleColor.DarkMagenta; 
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray; 
                            break;
                    }


                    if (tile == '=' && !player.levelComplete)
                    {
                        player.positionX = l;
                        player.positionY = k - 1;
                        player.levelComplete = true;
                        layout[k, l] = '#';
                    }

                    if (tile == '*' && !player.levelComplete)
                    {
                        enemy.positionX = l;
                        enemy.positionY = k;
                        layout[k, l] = '-';
                    }
                    if (tile == '@' && !player.levelComplete)
                    {
                        boss.positionX = l;
                        boss.positionY = k;
                        layout[k, l] = '-';
                    }
                    if (tile == '_' && !player.levelComplete)
                    {
                        runner.positionX = l;
                        runner.positionY = k;
                        layout[k, l] = '-';
                    }
                    Console.Write(tile);
                }
                Console.WriteLine();
            }

            player.DrawPlayer();
            enemy.DrawEnemy();
            boss.DrawBoss();
            runner.DrawRunner();
            Console.SetCursorPosition(0, 0);
            
        }

        
        }
    }
