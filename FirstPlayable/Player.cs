using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Player : Entity
    {
        public int playerDMG;
        public int playerMaxHealth;
        public int playerExp;
        public int playerSeeds;

        public Player()
        {
            healthSystem.SetHealth(playerMaxHealth);
            
                
            playerSeeds = 0;
            playerMaxHealth = 0;
            playerExp = 0;
            playerDMG = 20;

        }

        
    }
}
