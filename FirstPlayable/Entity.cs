using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Entity
    {
        public int x; 
        public int y;

        public HealthSystem healthSystem;

        public Entity()
        {
            healthSystem = new HealthSystem();
            x = 0;
            y = 0;
        }

        




    }
}
