using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Map : Entity
    {
        public static int mapX;
        public static int mapY;
        public int maximumX;
        public int maximumY;

        public int enemyX, enemyY;

        public int playerX;
        public int playerY;

        public string mapPath;
        string Level1 = @"RPGMap.txt";

        public string[] floor;
        public char[,] layout;
        public bool levelComplete;

        public Map()
        {
            LoadMap();
        }

        public void LoadMap()
        {
            mapPath = Level1;
            floor = File.ReadAllLines(mapPath);
            layout = new char[floor.Length, floor[0].Length];
            MakeMap();
        }

        public void MakeMap()
        {
            for (int i = 0; i < floor.Length; i++)
            {
                for (int j = 0; j < floor[i].Length; j++)
                {
                    layout[i, j] = floor[i][j];
                }
            }
        }

        public void DrawMap()
        {
            Console.SetCursorPosition(0, 0);
            for (int k = 0; k < mapY; k++)
            {
                for (int l = 0; l < mapX; l++)
                {
                    char tile = layout[k, l];

                    if (tile == '=' && levelComplete == false)
                    {
                        playerX = l;
                        playerY = k - 1;
                        levelComplete = true;
                        layout[k, l] = '#';
                    }

                    if (tile == 'E' && levelComplete == false)
                    {
                        if (tile == 'E')
                        {
                            enemyX = l;
                            enemyY = k;
                        }
                    }
                    Console.Write(tile);
                }
                Console.WriteLine();
            }

            PlayerPosition();
            EnemyPosition();
            Console.SetCursorPosition(0, 0);
        }

        public void PlayerPosition()
        {
            Console.SetCursorPosition(playerX, playerY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("!");
            Console.ResetColor();
            layout[playerY, playerX] = '!'; // Update the layout array
        }

        public void EnemyPosition()
        {
            Console.SetCursorPosition(enemyX, enemyY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("E");
            Console.ResetColor();
            layout[enemyY, enemyX] = 'E'; // Update the layout array
        }
    }
}