using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Enemy : Entity
    {
        public int enemyMaxHP;
        public int droppedEXP;
        public int enemyDamage;


        public Enemy()
        {

            healthSystem.SetHealth(enemyMaxHP);

            droppedEXP = 10;
            enemyDamage = 10;
            enemyMaxHP = 50;

        }


    }
}
