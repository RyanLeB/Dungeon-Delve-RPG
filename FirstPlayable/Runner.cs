using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Runner : EnemyManager
    {

        public Runner(int maxHealth, int damage, int startX, int startY, string name, char[,] mapLayout) : base(maxHealth, damage, startX, startY, name, mapLayout)
        {
            healthSystem = new HealthSystem(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            currentTile = mapLayout[startY, startX];
            enemyAlive = true;
            Name = name;
            icon = 'R';
        }

        public override void Draw()
        {
            if (enemyAlive == true)
            {

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(positionX, positionY);
                Console.Write(icon);
                Console.ResetColor();
            }
        }


        public override void Movement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout, Player player, List<EnemyManager> enemies)
        {
            int enemyMovementX = positionX;
            int enemyMovementY = positionY;
            int newEnemyPositionX = positionX;
            int newEnemyPositionY = positionY;
            

            if (mapLayout[newEnemyPositionY, newEnemyPositionX] != '#')
            {

                Console.SetCursorPosition(positionX, positionY);
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.Write(currentTile);


                positionX = newEnemyPositionX;
                positionY = newEnemyPositionY;


                currentTile = mapLayout[newEnemyPositionY, newEnemyPositionX];
            }


            int distanceX = Math.Abs(playerX - positionX);
            int distanceY = Math.Abs(playerY - positionY);

            if (distanceX <= 5 && distanceY <= 5)
            {

                if ((Math.Abs(positionX - playerX) == 1 && positionY == playerY) ||
                    (Math.Abs(positionY - playerY) == 1 && positionX == playerX))
                {

                    player.healthSystem.Damage(enemyDamage);
                    player.UpdateLiveLog($"Runner dealt {enemyDamage} damage to you!");
                    if (player.healthSystem.IsDead())
                    {
                        player.gameOver = true;
                    }
                    return;
                }


                if (playerX < positionX && mapLayout[positionY, positionX - 1] != '#' && positionX - 1 != playerX)
                {
                    enemyMovementX--;
                }
                else if (playerX > positionX && mapLayout[positionY, positionX + 1] != '#' && positionX + 1 != playerX)
                {
                    enemyMovementX++;
                }

                if (playerY < positionY && mapLayout[positionY - 1, positionX] != '#' && positionY - 1 != playerY)
                {
                    enemyMovementY--;
                }
                else if (playerY > positionY && mapLayout[positionY + 1, positionX] != '#' && positionY + 1 != playerY)
                {
                    enemyMovementY++;
                }
            }
            int oldPositionX = positionX;
            int oldPositionY = positionY;

            if (enemyAlive)
            {
                // Store the old position
                
                if (mapLayout[newEnemyPositionY, newEnemyPositionX] == '#')
                {
                    
                    return;
                }

                // Clear the old position of the enemy on the map layout
                mapLayout[oldPositionY, oldPositionX] = '-';

                // Redraw the old position
                Console.SetCursorPosition(oldPositionX, oldPositionY);
                Console.Write('-');

                // Update the enemy's position
                positionY = enemyMovementY;
                positionX = enemyMovementX;

                // Update the enemy's position on the map layout
                mapLayout[positionY, positionX] = icon;

                // Redraw the new position
                Console.SetCursorPosition(positionX, positionY);
                Console.Write(icon);
            }

            if (!enemyAlive)
            {
                // Update the map layout and the console when the enemy dies
                mapLayout[oldPositionY, oldPositionX] = '-';
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(positionX, positionY);
                Console.BackgroundColor = ConsoleColor.DarkGray; // Set the background color to dark gray
                Console.Write(currentTile);


            }
        }
    }
}
