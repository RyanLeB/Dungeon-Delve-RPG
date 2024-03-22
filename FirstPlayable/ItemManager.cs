using System;
using System.Collections.Generic;

namespace FirstPlayable
{
    internal class ItemManager
    {
        private Player player;


        
        public Dictionary<string, Item> Items { get; set; }

        public ItemManager(Player player)
        {
            this.player = player;
            Items = new Dictionary<string, Item>
        {
            { "HealthPotion", new HealthPotion() },
            { "DamageBoost", new DamageBoost() },
            { "Seed", new Seed() }
        };
        }



        public void UseItem(string itemName)
        {
            if (Items.ContainsKey(itemName))
            {
                Items[itemName].Use(player);
            }
        }
    }
}
