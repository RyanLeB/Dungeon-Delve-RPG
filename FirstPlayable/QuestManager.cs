using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class QuestManager
    {
        private Player player;
        private HUD hud;
        public QuestManager(Player player,HUD hud)
        {
            this.player = player;
            this.hud = hud;
        }
        public virtual void Draw()
        {

        }
    }
}
