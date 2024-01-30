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
        int mapX;
        int mapY;
        
        public string mapPath;
        string level1 = @"RPGMap1.txt";
        string level2 = @"RPGMap2.txt";
        public string[] floor;
        public char[,] layout;

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
        public void DrawMap(char[,] levelMap) 
        { 
            Console.SetCursorPosition(0, 0);
            for(int y = 0; y < mapY; y++)
            {
                for (int x = 0; x < mapX; x++)
                {
                    char tile = levelMap[y, x];

                }
                Console.WriteLine("\n");
            }
            Console.SetCursorPosition(0, 0);
        }


    }
}
