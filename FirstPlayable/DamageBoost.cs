using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    public class DamageBoost : Item
    {
        internal override void Use(Player player)
        {
            player.playerDamage += 1;
            player.UpdateLiveLog("Player Damage increased +1");
        }
    }
}
