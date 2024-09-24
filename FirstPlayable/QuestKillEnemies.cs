using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class QuestKillEnemies :Quest
    {
        public override void Complete(HUD hud)
        {
            throw new NotImplementedException();
        }
        public override void Started(HUD hud)
        {
            hud.DrawQuestLog();
        }
    }
}
