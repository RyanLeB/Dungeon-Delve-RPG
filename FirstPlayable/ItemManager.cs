using System;
using System.Collections.Generic;

namespace FirstPlayable
{
    internal class ItemManager
    {
        private Player player;
        

        public ItemManager(Player player)
        {
            this.player = player;
            
        }

       

        public void UseItem(string item)
        {
            
            switch (item)
            {
                case "HealthPotion":
                    player.healthSystem.Heal(2);
                    player.UpdateLiveLog("Gained +2 health");
                break;
                
                case "DamageBoost":
                    player.playerDamage += 1;
                    player.UpdateLiveLog("Player Damage increased +1");
                break;
                
                case "Seed":
                    player.currentSeeds += 1;
                    player.UpdateLiveLog("Picked up a seed");
                break;
                    
            }
            
            
        }
    }
}
