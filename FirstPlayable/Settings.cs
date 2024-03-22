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
        public string MapFileName { get; set; } = "RPGMap.txt";
        


        // Goblin settings

        public int GoblinInitialHealth { get; set; } = 3;
        public int GoblinInitialDamage { get; set; } = 0;



        // Boss settings
        public int BossInitialHealth { get; set; } = 12;
        public int BossInitialDamage { get; set; } = 2;


        // Runner settings
        public int RunnerInitialHealth { get; set; } = 1;
        public int RunnerInitialDamage { get; set; } = 2;
    }
}
