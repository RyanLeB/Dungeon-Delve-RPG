using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    abstract class Entity
    {

        public Position entityPosition;
        public HealthSystem healthSystem;

        public Entity()
        {
            healthSystem = new HealthSystem();
            entityPosition.x = 0;
            entityPosition.y = 0;
        }

        public struct Position 
        {
            public int maxX;
            public int maxY;
            public int x;
            public int y;
        }
        




    }
}
