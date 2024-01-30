using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Program
    {
        
        
        static void Main(string[] args)
        {
            Player player = new Player();
            Enemy enemy = new Enemy();
            
            TitleScreen title = new TitleScreen();
            
            title.ShowTitle();

        }
    }
}
