using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Program : Game
    {
        static void Main(string[] args)
        {
            // very short Main due to Game class, just have to start here
            
            Game game = new Game();
            game.Start();
        }
       

    }
}
