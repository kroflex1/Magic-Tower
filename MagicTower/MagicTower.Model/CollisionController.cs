using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model
{
    public static class CollisionController
    {
        public static void CheckGameObjectsForCollisions(Room room)
        {
            FindCollisionBetweenMagicsAndEnemies(room);
            FindCollisionBetweenPlayerAndEnemies(room);
        }

        private static void FindCollisionBetweenMagicsAndEnemies(Room room)
        {
            foreach (var magic in room.MagicInRoom)
            {
                var magicRectangle = new Rectangle(magic.PosX, magic.PosY, magic.HitboxWidth, magic.HitboxHeight);
                foreach (var enemy in room.AliveEnemiesInRoom)
                {
                    var enemyRectangle = new Rectangle(enemy.PosX, enemy.PosY, enemy.HitboxWidth, enemy.HitboxHeight);
                    if (IsIntersection(enemyRectangle, magicRectangle))
                    {
                        magic.OnCollisionEnter(enemy);
                        enemy.OnCollisionEnter(magic);
                    }
                }
            }
        }

        private static void FindCollisionBetweenPlayerAndEnemies(Room room)
        {
            var playerRectangle = new Rectangle(room.Player.PosX, room.Player.PosY, room.Player.HitboxWidth,
                room.Player.HitboxHeight);
            foreach (var enemy in room.AliveEnemiesInRoom)
            {
                var enemyRectangle = new Rectangle(enemy.PosX, enemy.PosY, enemy.HitboxWidth, enemy.HitboxHeight);
                if (IsIntersection(playerRectangle, enemyRectangle))
                {
                    enemy.OnCollisionEnter(room.Player);
                    room.Player.OnCollisionEnter(enemy);
                }
            }
        }

        public static bool IsIntersection(Rectangle firstRectangle, Rectangle secondRectangle)
        {
            return !(firstRectangle.Bottom < secondRectangle.Top || firstRectangle.Right < secondRectangle.Left ||
                     secondRectangle.Bottom < firstRectangle.Top || secondRectangle.Right < firstRectangle.Left);
        }

        public class Rectangle
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            public Rectangle(int x, int y, int width, int height)
            {
                Left = x;
                Top = y;
                Right = x + width;
                Bottom = y + height;
            }
        }
    }
}