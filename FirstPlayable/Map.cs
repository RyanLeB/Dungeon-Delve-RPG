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
        
        // X and Y coords
        public int mapX;
        public int mapY;
        int maximumX;
        int maximumY;

        public int enemyX, enemyY;
        
        
        
        public int playerX;
        public int playerY;
        
        // map path
        public string mapPath;
        string level1 = @"RPGMap1.txt";
        string level2 = @"RPGMap2.txt";
        public string[] floor;
        public char[,] layout;
        public bool levelComplete;
        
        
        

        // Constructor
        public Map()
        {
            LoadMap();
        }

       
        
        
        // Loads level map
        public void LoadMap()
        {
            mapPath = level1;
            floor = File.ReadAllLines(mapPath);
            layout = new char[floor.Length, floor[0].Length];
            MakeMap();
        }

        // Makes level map
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

        // Draws level map
        public void DrawMap()
        {

            Console.Clear();

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
        }

        public void EnemyPosition()
        {
            Console.SetCursorPosition(enemyX, enemyY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("E");
            Console.ResetColor();
        }
    }
}
