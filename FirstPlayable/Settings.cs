using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Settings
    {
       
      
        // Player settings
        public int PlayerInitialHealth { get; set; } = 10;
        public int PlayerInitialDamage { get; set; } = 10;
        public int PlayerInitialLevel { get; set; } = 1;

        // world Settings
        public string MapFileName { get; set; } = "RPGMap.txt";
        public string Map2FileName { get; set; } = "RPGMap2.txt";
        public string Map3FileName { get; set; } = "RPGMap3.txt";
        public string Map4FileName { get; set; } = "RPGMap4.txt";
        public string MusicFileName { get; set; } = "DungeonMap.wav";

        public string FileLocation { get; set; } = @"Maps-Music";
        


        // Goblin settings

        public int GoblinInitialHealth { get; set; } = 3;
        public int GoblinInitialDamage { get; set; } = 0;



        // Boss settings
        public int BossInitialHealth { get; set; } = 28;
        public int BossInitialDamage { get; set; } = 2;


        // Runner settings
        public int RunnerInitialHealth { get; set; } = 1;
        public int RunnerInitialDamage { get; set; } = 2;
    }
}
