using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal abstract class Quest
    {
        public bool IsCurrent;
        public abstract void Complete(HUD hud);
        public abstract void Started(HUD hud);
    }
}
