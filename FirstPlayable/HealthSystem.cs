using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class HealthSystem
    {

        public int health;
        

        public HealthSystem()
        {

        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0) 
            {
                health = 0;
            }
        }

        public void Heal(int recoverHP, int maxHealth)
        {
            health += recoverHP;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
        }
        
        
        public void SetHealth(int maxHP) 
        {
            health = maxHP;
        }
    
    }
}
