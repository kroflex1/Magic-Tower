using System.Collections.Generic;
using MagicTower.Model.EnemiesModels;

namespace MagicTower.Model
{
    public static class CollisionController
    {
        public static void CheckGameObjectsForCollisions(Room room)
        {
            foreach (var magic in room.MagicInRoom)
            {
                var magicRectangle = new Rectangle(magic.PosX, magic.PosY, magic.HitboxWidth, magic.HitboxHeight);
                foreach (var enemy in room.AliveEnemiesInRoom)
                {
                    var enemyRectangle = new Rectangle(enemy.PosX, enemy.PosY, enemy.HitboxWidth, enemy.HitboxHeight);
                    if (IsIntersection(magicRectangle, enemyRectangle))
                    {
                        magic.OnCollisionEnter(enemy);
                        enemy.OnCollisionEnter(magic);
                    }
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