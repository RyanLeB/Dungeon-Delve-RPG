using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Entity
    {
        public Position position;

        public HealthSystem healthSystem;

        public Entity()
        {
            healthSystem = new HealthSystem();
            position.x = 0;
            position.y = 0;
        }

        public struct Position
        {
            public int x, y;
        }




    }
}
