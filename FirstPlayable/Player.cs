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

        public static char Input()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.KeyChar == 'w')
            {
                return 'w';
            }
            else if (key.KeyChar == 'a')
            {
                return 'a';
            }
            else if (key.KeyChar == 's')
            {
                return 's';
            }
            else if (key.KeyChar == 'd')
            {
                return 'd';
            }
            else
            {
                return 'e';
            }

        }

        public void DisplayPlayer()
        {
            Console.SetCursorPosition(Position.x, Position.y);
        }
    }
}
