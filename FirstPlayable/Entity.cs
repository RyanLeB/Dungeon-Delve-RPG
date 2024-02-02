﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    public abstract class Entity
    {
        public int entityMaxHealth { get; protected set; }
        public int entityHealth { get; set; }
        public int entityDamage { get; protected set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool IsAlive { get; set; }

        public Entity(int maxHealth, int health, int damage, int startX, int startY)
        {
            entityMaxHealth = maxHealth;
            entityHealth = health;
            entityDamage = damage;
            positionX = startX;
            positionY = startY;
            IsAlive = true;
        }

        public abstract void Move(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout);
        public abstract void Draw();
    }
}
