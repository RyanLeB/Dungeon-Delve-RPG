using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Map
    {
        public static string path;
        public static string RPGMap = @"RPGMap.txt";
        static string[] floor;
        static char[,] layout;

        public int mapX;
        static int mapY;
        static int maximumX;
        static int maximumY;
    }
}
