using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    public class HealthPotion : Item
    {
        internal override void Use(Player player)
        {
            player.healthSystem.Heal(2);
            player.UpdateLiveLog("Gained +2 health");
        }
    }
}
