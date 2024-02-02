using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    abstract class Entity
    {

        public Position EntityPosition;
        public HealthSystem HealthSystem;

        public Entity()
        {
            HealthSystem = new HealthSystem();
            EntityPosition = new Position();
            EntityPosition.X = 0;
            EntityPosition.Y = 0;
            EntityPosition.maxX = 0;
            EntityPosition.maxY = 0;
        }

        public struct Position 
        {
            public int maxX;
            public int maxY;
            public int X;
            public int Y;
        }
        
        



    }
}
