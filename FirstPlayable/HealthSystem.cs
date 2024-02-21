using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

       
namespace FirstPlayable
{
    internal class HealthSystem
    {
        // variables | encapsulation
        private int maximumHealth;
        private int currentHealth;

        public HealthSystem(int maxHealth)
        {
        maximumHealth = maxHealth;
        currentHealth = maxHealth;
        }

        // Getters
        public int GetCurrentHealth()
            {
                return currentHealth;
            }

            public int GetMaximumHealth()
            {
                return maximumHealth;
            }

            // Modify Health
            public void Damage(int amount)
            {
                currentHealth -= amount;
                if (currentHealth < 0)
                    currentHealth = 0;
            }

            public void Heal(int amount)
            {
                currentHealth += amount;
                if (currentHealth > maximumHealth)
                    currentHealth = maximumHealth;
            }

            // Check Health
            public bool IsDead()
            {
                return currentHealth <= 0;
            }
        }
    }


