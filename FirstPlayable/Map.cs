using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    public class Map
    {
        private string path;
        private string[] floor;
        public char[,] layout;

        public int mapWidth { get; private set; }
        public int mapHeight { get; private set; }
        public int initialPlayerPositionX { get; private set; }
        public int initialPlayerPositionY { get; private set; }
        public int initialEnemyPositionX { get; private set; }
        public int initialEnemyPositionY { get; private set; }

        public Map(string mapFileName)
        {
            path = mapFileName;
            floor = File.ReadAllLines(path);
            CreateMap();
        }

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

        public void DrawMap(Player player, Enemy enemy)
        {
            Console.Clear();

            for (int k = 0; k < mapHeight; k++)
            {
                for (int l = 0; l < mapWidth; l++)
                {
                    char tile = layout[k, l];

                    if (tile == '=' && !player.levelComplete)
                    {
                        player.positionX = l;
                        player.positionY = k - 1;
                        player.levelComplete = true;
                        layout[k, l] = '#';
                    }

                    if (tile == 'E' && !player.levelComplete)
                    {
                        enemy.positionX = l;
                        enemy.positionY = k;
                    }

                    Console.Write(tile);
                }
                Console.WriteLine();
            }

            player.DrawPlayer();
            enemy.DrawEnemy();
            Console.SetCursorPosition(0, 0);
        }

        
        }
    }
