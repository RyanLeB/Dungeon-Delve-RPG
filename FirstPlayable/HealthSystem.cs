using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class HealthSystem
    {
        public int Health;

        public HealthSystem()
        {
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
            }
        }

        public void Heal(int recoverHP, int maxHealth)
        {
            Health += recoverHP;
            if (Health > maxHealth)
            {
                Health = maxHealth;
            }
        }

        public void SetHealth(int maxHP)
        {
            Health = maxHP;
        }
    }
}
