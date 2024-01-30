using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    
    
    internal class TitleScreen
    {
        public void ShowTitle() 
        {
            Console.WriteLine("Welcome to the Underworld!");
            
            Console.WriteLine("--------------------------");
            Console.WriteLine("Your goal is to escape the Underworld by defeating monsters and walking through each level");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine("It's dangerous to go alone.... Good luck!");
            


            Console.ReadKey(true);
            Console.Clear();
        }
    
    }
}
