using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Program
    {
        static Player Player1;
        static Enemy Enemy1;
        static Map levelMap;

        
        static void Main(string[] args)
        {
            TitleScreen title = new TitleScreen();
            OnStart();
            title.ShowTitle();

            levelMap.DrawMap(levelMap.layout);
            
            

        }


        static void OnStart()
        {
            levelMap = new Map();
            Player1 = new Player();
            Enemy1 = new Enemy();
        }


    }
}
