using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Program : GameManager
    {
        static void Main(string[] args)
        {
            // very short Main due to GameManager class, just have to start here
            
            GameManager game = new GameManager();
            game.Start();

        }
       

    }
}
